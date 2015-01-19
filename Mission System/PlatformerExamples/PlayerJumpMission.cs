using UnityEngine;
using System.Collections;
using DrForgetful;

public class PlayerJumpMission : Mission {
	
	public PlayerJumpMission() : base()
	{
		eventType = "playerJump";
		missionTime = 10;
		descriptionVerb = "Jump";
		descriptionNoun = "times";
		minEvents = 10;
		score = 100;
	}
}
