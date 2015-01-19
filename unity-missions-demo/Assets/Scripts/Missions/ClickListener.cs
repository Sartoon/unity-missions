using UnityEngine;
using System.Collections;

public class ClickListener : MonoBehaviour {

	DrForgetful.EventManager eventManager;

	void Start ()
	{
		eventManager = FindObjectOfType<DrForgetful.EventManager>();
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			eventManager.LogEvent("leftClick");
		}

		if(Input.GetMouseButtonDown(1))
		{
			eventManager.LogEvent("rightClick");
		}
	}
}
