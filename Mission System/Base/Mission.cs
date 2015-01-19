using System;
using System.Collections.Generic;
using UnityEngine;
using DrForgetful;

namespace DrForgetful
{
	public class Mission : IGameEventListener
	{
		protected int count;

		protected float endTime;
		protected float missionTime;

		protected float delayedStartTime;

		protected string eventType;

		protected string descriptionVerb;
		protected string descriptionNoun;

		protected int minEvents;

		protected int score;

		protected bool timeBased = true;

		protected string description;

		protected bool succeeded;

		public bool active;
		public bool dying;
		public EventManager eventManager {get;set;}
		public IScoreListener scoreListener {get;set;}

		public Mission()
		{
		}

		public Mission(EventManager eventManager)
		{
			this.eventManager = eventManager;
		}

		public void RegisterEvent()
		{
			count++;
		}

		public bool OverTime()
		{
			return (Time.time > endTime);
		}

		public string GetRemainingTime(float time)
		{
			return ((time - Time.time)).ToString("F1");
		}

		public virtual string GetDescription()
		{
			if(!Success() && !Fail())
				return descriptionVerb + " " + (minEvents - count).ToString() + " " + descriptionNoun;
			else
				return descriptionVerb + " " + minEvents.ToString() + " " + descriptionNoun;
		}

		public virtual string GetScreenText()
		{
			string text = GetDescription();

			if(Success())
			{
				text += " - Success!";
			}
			else if(Fail())
			{
				text += " - Failed.";
			}
			else
			{
				if (timeBased && active)
				{
					text += " " + GetRemainingTime(endTime);
				}

				if(!active)
				{
					text += " - Begins in " + GetRemainingTime(delayedStartTime);
				}
			}



			return text;
		}

		public virtual void SetDelayedStart(float delayStart)
		{
			delayedStartTime = Time.time + delayStart;
		}

		public virtual void StartMission()
		{
			if(!active)
			{
				endTime =  Time.time + missionTime;
				count = 0;

				eventManager.AddListenerToEvent(eventType, this);
				active = true;
			}
		}

		public void StopMission()
		{
			if(eventManager != null)
				eventManager.RemoveListenerFromEvent(eventType, this);

			active = false;
			endTime = 0;
		}

		public virtual void Setup(){}
		public virtual void Update(){}
		public virtual bool Success()
		{
			if(!active)
				return false;

			if(succeeded)
				return true;
			
			if(count >= minEvents && !Fail())
			{
				succeeded = true;
				AddScore();
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual bool Fail()
		{
			if(!active)
				return false;

			if(succeeded)
				return false;

			return OverTime() && timeBased;
		}

		public virtual bool HasFinished()
		{
			return (Success() || Fail());
		}

		public virtual void AddScore()
		{
			if(scoreListener != null)
			{
				scoreListener.AddScore(score);
			}
		}

	}
}