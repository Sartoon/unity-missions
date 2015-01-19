using System;
using UnityEngine;

namespace DrForgetful
{
	public class Timer
	{
		public bool frameBased;

		public float interval { get; set; }
		public float nextTime { get; set; }
		public bool overTime
		{ 
			get
			{
				if(frameBased)
					return nextTime >= Time.frameCount;
				else
					return Time.time >= nextTime;
			} 
		}

		public Timer(float interval)
		{
			this.interval = interval;
		}

		public void NewInterval()
		{
			if(frameBased)
				nextTime = Time.frameCount + interval;
			else
				nextTime = Time.time + interval;
		}
	}
}

