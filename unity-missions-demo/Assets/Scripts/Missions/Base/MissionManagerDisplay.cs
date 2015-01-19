using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using DrForgetful;

namespace DrForgetful
{
	public class MissionManagerDisplay : MonoBehaviour
	{
		[SerializeField] string textHeader;

		MissionManager missionManager;
		Text uiText;

		void Start()
		{
			missionManager = FindObjectOfType<MissionManager>();
			uiText = GetComponent<Text>();
		}

		void Update()
		{
			string display = textHeader;

			foreach(Mission m in missionManager.GetMissions())
			{
				display += "\n " + m.GetScreenText();
			}

			uiText.text = display;
		}
	}
}

