using System;
using System.Collections.Generic;
using UnityEngine;
using DrForgetful;

namespace DrForgetful
{

	public class EventManager : MonoBehaviour
	{

		Dictionary<string, CustomGameEvent> gameEvents;

		public EventManager()
		{
			gameEvents = new Dictionary<string, CustomGameEvent>();
		}

		public void LogEvent(string eventType)
		{
			CustomGameEvent gameEvent;
			if(gameEvents.TryGetValue(eventType, out gameEvent))
			{
				gameEvent.RegisterEvent();
			}
			else
			{
				AddEventType(eventType);
				LogEvent(eventType); // Call this again
			}
		}

		public void LogEvent(string[] eventType)
		{
			foreach(string ev in eventType)
			{
				LogEvent(ev);
			}
		}

		public void AddListenerToEvent(string eventType, IGameEventListener listener)
		{
			CustomGameEvent gameEvent;
			if(gameEvents.TryGetValue(eventType, out gameEvent))
			{
				gameEvent.AddListener(listener);
			}
			else
			{
				AddEventType(eventType);
				AddListenerToEvent(eventType, listener); // Call this again
			}
		}

		public void RemoveListenerFromEvent(string eventType, IGameEventListener listener)
		{
			CustomGameEvent gameEvent;
			if(gameEvents.TryGetValue(eventType, out gameEvent))
			{
				gameEvent.RemoveListener(listener);
			}
		}

		void AddEventType(string eventType)
		{
			CustomGameEvent gameEvent = new CustomGameEvent(eventType);
			gameEvents.Add(eventType, gameEvent);
		}

	}
}
