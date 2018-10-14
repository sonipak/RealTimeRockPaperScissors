using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;


public class PlayButton : MonoBehaviour {
	const int MIN_OPPONENTS = 1, MAX_OPPONENTS = 1;
	const int gameVariant = 0;
	GPGMethods GPGScripts;
	public Text text;
	public Button button;
	public Image image;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(ButtonClicked);
		PlayerPrefs.SetString ("Name", "Despacito"); //temp
		GPGScripts = GPGMethods.Instance;
		GPGScripts.SignIn ();
		if(Application.isEditor) {
			text.text = "Test Mode";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!Application.isEditor) {
			if (!GPGScripts.SignedIn) {
				text.text = "You must be signed in!";
				button.interactable = false;
			} else if (GPGScripts.SignedIn && !GPGScripts.CreatingRoom) {
				text.text = "";
				button.interactable = true;
			} else if (GPGScripts.CreatingRoom) {
				text.text = "Searching for a match...";
				button.interactable = false;
			} else if (GPGScripts.RoomCreated) {
				text.text = "Match Found!";
			}
		} 

		if (GPGScripts.RoomCreated) {
			SceneManager.LoadSceneAsync ("Game Play", LoadSceneMode.Single);
		}
	}

	void ButtonClicked(){
		if (Application.isEditor) {
			text.text = "Click";
		} else {
			GPGScripts.QueueRandomMatch (1, 1, 0);
		}
	}
}
