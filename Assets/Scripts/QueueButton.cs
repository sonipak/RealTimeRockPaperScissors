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
		button = GetComponent<Button> ();
		button.onClick.AddListener (clickedButton);
		GPGScripts = GPGMethods.Instance;
		GPGScripts.SignIn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GPGScripts.SignedIn) {
			button.enabled = false;
			text.text = "Not Signed In";
		} else if(GPGScripts.SignedIn){
			button.enabled = true;
			text.text = "Find Match";
		}
	}

	async void clickedButton(){
		button.enabled = false;
		await Task.Run(() => GPGScripts.QueueRandomMatch (1, 1, 0));
		text.text = "Queueing for match... ";
	}
}
