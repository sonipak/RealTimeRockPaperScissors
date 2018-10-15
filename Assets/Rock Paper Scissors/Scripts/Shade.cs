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
	public void FadeOut(){
		Destroy (gameObject);
	}
}
