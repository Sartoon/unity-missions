using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using DrForgetful;
using System.Linq;

namespace DrForgetful
{
	public class MissionManager : MonoBehaviour
	{

		class MissionType
		{
			public Mission mission;
			public bool inUse;
			
			public MissionType(Mission mission)
			{
				this.mission = mission;
			}
		}

		[Header("Mission Delay Times")]
		public float startMissionDelay = 3f;
		public float removeFinishedMissionDelay = 3f;

		[Header("Mission Spawn")]
		public float spawnTime;
		public int maximumActiveMissions = 3;
		public int startupMissions = 3;
		public bool allowDuplicateMissions;
		public bool runMissionsOnStart;

		[HideInInspector]
		public bool runMissions;
		public IScoreListener scoreListener;

		List<Mission> missions;
		List<MissionType> missionTypes;
		EventManager eventManager;


		int activeMissions;

		Timer timer;

		void Awake()
		{
			timer = new Timer(spawnTime);
			
			eventManager = GameObject.FindObjectOfType<EventManager>();

			missions = new List<Mission>();
			missionTypes = new List<MissionType>();
		}
		
		void Start()
		{
			timer.NewInterval();

			if(runMissionsOnStart)
			{
				runMissions = true;
			}
		}

		public void Update()
		{
			if(runMissions)
			{
				foreach(Mission m in missions)
				{
					if(m.active && !m.dying)
					{
						if(m.HasFinished())
						{
							StartCoroutine(DeactivateMissionDelayed(m));
						}
					}
				}

				if(timer.overTime)
				{
					CreateNewMission();
					timer.NewInterval();
				}
			}
		}

		public void AddMission(Mission mission)
		{
			if(mission != null)
				missions.Add(mission);
		}

		public List<Mission> GetMissions()
		{
			return missions;
		}

		IEnumerator DeactivateMissionDelayed(Mission mission)
		{
			mission.dying = true;

			yield return new WaitForSeconds(removeFinishedMissionDelay);

			mission.StopMission();
			missions.Remove(mission);
			activeMissions--;

			if(!allowDuplicateMissions)
			{
				foreach(MissionType missionType in missionTypes)
				{
					if(missionType.mission.GetType() == mission.GetType())
					{
						missionType.inUse = false;
						break;
					}
				}
			}
		}

		IEnumerator ActivateMissionDelayed(Mission mission)
		{
			mission.SetDelayedStart(startMissionDelay);
			yield return new WaitForSeconds(startMissionDelay);
			mission.StartMission();
			activeMissions++;
		}

		public void AddMissionType(Mission mission)
		{
			missionTypes.Add(new MissionType(mission));
		}

		public void CreateStartupMissions(int amount, bool inOrder)
		{
			if((inOrder || !allowDuplicateMissions) && amount > missionTypes.Count)
			{
				amount = missionTypes.Count;
			}

			for(int i = 0; i < amount; i++)
			{
				Mission mission;

				if(!inOrder)
				{
					mission = SpawnRandomMission();
				}
				else
				{
					mission = GetNewMissionFromType(missionTypes[i].mission);
					missionTypes[i].inUse = true;
				}

				if(mission != null)
				{
					InitialiseMission(mission);
				}
			}
		}

		Mission SpawnRandomMission()
		{
			Mission mission = null;
			int index = -1;

			if(activeMissions >= maximumActiveMissions)
				return null;

			if(!allowDuplicateMissions)
			{
				int start = missionTypes.GetRandomListPosition();
				int max = missionTypes.Count();
				int current = start;

				do
				{
					if(!missionTypes[current].inUse)
					{
						index = current;
						break;
					}

					current++;

					if(current >= max)
						current = 0;
				}
				while(current != start);
			}
			else
			{
				index = missionTypes.GetRandomListPosition();
			}

			if(index > -1)
			{
				mission = GetNewMissionFromType(missionTypes[index].mission);
				missionTypes[index].inUse = true;
			}

			return mission;
		}

		Mission GetNewMissionFromType(Mission mission)
		{
			return (Mission)Activator.CreateInstance(mission.GetType());
		}

		void CreateNewMission()
		{
			Mission mission = SpawnRandomMission();

			if(mission != null)
			{
				InitialiseMission(mission);
			}
		}

		void InitialiseMission(Mission mission)
		{
			mission.eventManager = eventManager;
			mission.scoreListener = scoreListener;
			missions.Add(mission);

			StartCoroutine(ActivateMissionDelayed(mission));
		}

	}

}