using UnityEngine;
using System.Collections;

public class PlayerNoClickMission : DrForgetful.Mission
{

	public PlayerNoClickMission()
	{
		eventType = "leftClick";
		missionTime = 3;
	}
	
	public override string GetDescription()
	{
		return "Don't left click for " + missionTime.ToString() + " seconds!";
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
