using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;


public class QueueButton : MonoBehaviour {
	const int MIN_OPPONENTS = 1, MAX_OPPONENTS = 1;
	const int gameVariant = 0;

	[SerializeField]
	GameObject GPGObject;
	GPGMethods GPGScripts;
	[SerializeField]
	Text text;
	Button button;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (clickedButton);
		GPGScripts = GPGObject.GetComponent<GPGMethods> ();
		GPGScripts.SignIn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GPGScripts.SignedIn) {
			button.enabled = false;
			text.text = "Not Signed In";
		} else {
			button.enabled = true;
			text.text = "Find Match";
		}
	}

	void clickedButton(){
		button.enabled = false;
		GPGScripts.QueueRandomMatch (1, 1, 0);
		text.text = "Queueing for match... ";
	}
}
