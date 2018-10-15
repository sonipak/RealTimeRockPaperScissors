using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	[SerializeField]
	SelectedIcon xd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			xd.SetImage (0);
		} else if (Input.GetKeyDown(KeyCode.W)){
			xd.SetImage (1);
		} else if(Input.GetKeyDown(KeyCode.E)){
			xd.SetImage (2);
		}
	}
}
