using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MalagaJam.LastTrain;

public class Bubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start bubble");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButton (0)) {
			Debug.Log("Mouse down");

			this.transform.
		}
		*/
	}

	void OnMouseDown()
	{
		HumanBehaviour heroB = GameManager.instance.hero.GetComponent<HumanBehaviour> ();
		heroB.checkReaction("Joy");
	}

}
