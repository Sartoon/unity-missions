using UnityEngine;
using System.Collections;
using DrForgetful;

public class CollectibleGetMission : Mission {

	public CollectibleGetMission() : base()
	{
		eventType = "collectibleObtained";
		descriptionVerb = "Get";
		descriptionNoun = "collectibles";
		minEvents = 40;
		score = 100;
		timeBased = false;
	}
}
