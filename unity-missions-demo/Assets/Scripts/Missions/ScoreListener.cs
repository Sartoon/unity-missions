using UnityEngine;
using System.Collections;

public class ScoreListener : MonoBehaviour, DrForgetful.IScoreListener {

	int score;
	UnityEngine.UI.Text text;
	DrForgetful.MissionManager missionManager;

	void Awake () {
		text = GetComponent<UnityEngine.UI.Text>();
		missionManager = FindObjectOfType<DrForgetful.MissionManager>();
		missionManager.scoreListener = this;
	}
	
	void Update () {
		text.text = "Score: " + score;
	}

	void DrForgetful.IScoreListener.AddScore(int score)
	{
		this.score += score;
	}
}
