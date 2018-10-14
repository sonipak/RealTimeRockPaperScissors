using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextHandler : MonoBehaviour {
	public Text pName, eName;
	GPGMethods GPGScripts;

	// Use this for initialization
	void Start () {
		GPGScripts = GPGMethods.Instance;
		pName.text = PlayerPrefs.GetString("Name");
		eName.text = GPGScripts.EnemyName;
	}
	
	// Update is called once per frame
	void Update () {
		if (eName.text == "...") {
			eName.text = GPGScripts.EnemyName;
		}
	}
}
