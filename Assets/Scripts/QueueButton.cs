using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;


public class QueueButton : MonoBehaviour {
	const int MIN_OPPONENTS = 1, MAX_OPPONENTS = 1;
	const int gameVariant = 0;
	GPGMethods GPGScripts;
	[SerializeField]
	Text text;
	Button button;
	// Use this for initialization
	void Start () {
		GPGScripts = GPGMethods.Instance;
		GPGScripts.SignIn ();
		button = transform.GetComponent<Button> ();
		button.onClick.AddListener (ButtonClicked);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GPGScripts.SignedIn) {
			text.text = "Not Signed In";
		} else if (GPGScripts.SignedIn && GPGScripts.RoomSetupProgress == 0f) {
			text.text = "Find Match";
		} else if (GPGScripts.RoomSetupProgress > 0f) {
			text.text = "Loading Room " + GPGScripts.RoomSetupProgress + "%...";
		}
	}

	void ButtonClicked(){
		button.enabled = false;
		text.text = "Finding Match...";
		GPGScripts.QueueRandomMatch (1, 1, 0);
	}
}
