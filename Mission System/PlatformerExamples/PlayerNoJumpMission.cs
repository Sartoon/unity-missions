using UnityEngine;
using System.Collections;
using DrForgetful;

public class PlayerNoJumpMission : Mission {

	public PlayerNoJumpMission()
	{
		eventType = "playerJump";
		missionTime = 3;
	}

	public override string GetDescription()
	{
		return "Don't jump for " + missionTime.ToString() + " seconds!";
	}

	public override bool Success ()
	{
		if(!active)
			return false;

		if(succeeded)
			return true;
		
		if(count < 1 && OverTime())
		{
			succeeded = true;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public override bool Fail()
	{
		if(!active)
			return false;

		if(succeeded)
			return false;
		else
			return (count > 0);
	}
}
