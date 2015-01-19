using UnityEngine;
using System.Collections;

public class PlayerClickUntimedMission : DrForgetful.Mission
{
	public PlayerClickUntimedMission() : base()
	{
		eventType = "rightClick";
		descriptionVerb = "Right click";
		descriptionNoun = "times";
		minEvents = 10;
		score = 100;
		timeBased = false;
	}
}

