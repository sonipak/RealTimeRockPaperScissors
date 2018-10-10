using System.Collections;
using System.Collections.Generic;
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void clickedButton(){
		button.enabled = false;
		text.text = "Signing in...";
		if(GPGScripts.SignInAsync())
			text.text = "Signed in!";
	}
}
