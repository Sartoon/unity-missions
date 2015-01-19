using System;
using UnityEngine;
using System.Collections.Generic;

namespace DrForgetful
{
	public class MissionInitiator : MonoBehaviour
	{
		List<Mission> missionTypes;
		MissionManager missionManager;

		public bool initiallySpawnInOrder = true;
		public bool populateMissionManagerOnStart;

		void Awake()
		{
			missionTypes = new List<Mission>();

			missionTypes.Add(new PlayerClickMission());
			missionTypes.Add(new PlayerNoClickMission());
			missionTypes.Add(new PlayerClickUntimedMission());

			missionManager = GameObject.FindObjectOfType<MissionManager>();
		}

		void Start()
		{
			if(populateMissionManagerOnStart)
			{
				PopulateMissionManager();
			}
		}

		public void PopulateMissionManager()
		{
			foreach(Mission mission in missionTypes)
			{
				missionManager.AddMissionType(mission);
			}

			missionManager.CreateStartupMissions(3, initiallySpawnInOrder);
		}
	}
}

