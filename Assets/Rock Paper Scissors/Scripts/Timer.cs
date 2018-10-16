using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public int timer { get; set; }
	public bool active { get; set; }
	Text text;
	// Use this for initialization
	void Start () {
		timer = 10;
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = timer.ToString ();
	}

	public IEnumerator StartTimer(){ 
		timer = 10;
		active = true;
		for (int i = timer; i > 0; i--) {
			if (active) {
				timer = i;
				yield return new WaitForSecondsRealtime (1);
			} else {
				timer = 10;
				yield break;
			}

		}
	}
}
