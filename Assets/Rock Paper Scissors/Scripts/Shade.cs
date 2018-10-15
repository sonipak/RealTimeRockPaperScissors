using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour {
	public SpriteRenderer sr;


	// Use this for initialization
	void Start () {
		sr.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator FadeOut(){
		for (float i = 1f; i > 0f; i -= Time.deltaTime*2){
			sr.color = new Color (1f, 1f, 1f, i);
			yield return null;
		}
	}
}
