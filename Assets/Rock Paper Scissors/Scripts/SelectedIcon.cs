using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedIcon : MonoBehaviour {
	Image img;
	// Use this for initialization
	void Start () {
		img = transform.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetImage(int choice){
		if (choice == 0) {
			img.sprite = Resources.Load<Sprite> ("Sprites/Icons/Rock");
			transform.localScale = Vector3.one;
		} else if (choice == 1) {
			img.sprite = Resources.Load<Sprite> ("Sprites/Icons/Paper");
			transform.localScale = new Vector3 (0.75f, 1f);
		} else if (choice == 2) {
			img.sprite = Resources.Load<Sprite> ("Sprites/Icons/Scissors");
			transform.localScale = new Vector3 (1f, 0.75f);
		}
	}
	public void ClearImage(){
		img.sprite = Resources.Load<Sprite> ("Sprites/Empty");
		transform.localScale = Vector3.one;
	}
}
