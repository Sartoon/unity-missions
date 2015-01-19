using UnityEngine;
using System.Collections;

public class PlayerClickMission : DrForgetful.Mission
{
	public PlayerClickMission() : base()
	{
		eventType = "leftClick";
		missionTime = 10;
		descriptionVerb = "Left click";
		descriptionNoun = "times";
		minEvents = 10;
		score = 100;
	}
}
