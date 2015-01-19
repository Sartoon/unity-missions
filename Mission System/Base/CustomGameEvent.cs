using System;
using System.Collections.Generic;

namespace DrForgetful
{
	public class CustomGameEvent
	{
		List<IGameEventListener> listeners;
		public string eventType;

		public CustomGameEvent(string type)
		{
			eventType = type;
			listeners = new List<IGameEventListener>();
		}

		public void RegisterEvent()
		{
			foreach(IGameEventListener listener in listeners)
			{
				listener.RegisterEvent();
			}
		}

		public void AddListener(IGameEventListener listener)
		{
			listeners.Add(listener);
		}

		public void RemoveListener(IGameEventListener listener)
		{
			listeners.Remove(listener);
		}
	}
}


