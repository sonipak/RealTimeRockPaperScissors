using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyUp : MonoBehaviour {
	Button button;
	GPGMethods GPGScripts;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		GPGScripts = GPGMethods.Instance;
		button.onClick.AddListener (ClickedButton);
	}
	
	// Update is called once per frame
	void Update () {
		if (GPGScripts.RoomCreated) {
			button.enabled = true;
		} else {
			button.enabled = false;
		}
	}

	void ClickedButton(){
		SceneManager.LoadSceneAsync (1, LoadSceneMode.Single);
	}
}
