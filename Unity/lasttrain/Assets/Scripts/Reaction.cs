using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalagaJam.LastTrain;

public class Reaction : MonoBehaviour {

	Vector2 originalPos;
	// Use this for initialization
	void Start () {
		originalPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		GameManager.instance.reactionClicked = GameObject.Find(this.name);
		HumanBehaviour heroB = GameManager.instance.hero.GetComponent<HumanBehaviour> ();
		heroB.delayCheckReaction(this.name);

		//this.transform.position = GameManager.instance.heroBubble.transform.position;
		this.transform.position = new Vector2(-3.86f, -0.83f);
	}

	public void resetPos() {
		this.transform.position = originalPos;
	}

}
