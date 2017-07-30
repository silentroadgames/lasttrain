using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MalagaJam.LastTrain;
using UnityEngine.SceneManagement;

public class Reaction : MonoBehaviour {

	Vector2 originalPos;
	AudioSource audioSource;
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
		HumanBehaviour targetB = GameManager.instance.target.GetComponent<HumanBehaviour> ();
		targetB.checkReaction(this.name);

		//this.transform.position = GameManager.instance.heroBubble.transform.position;
		this.transform.position = new Vector2(-3.91f, -0.63f);

		//audioSource = GameManager.instance.audioOK.GetComponent<AudioSource> ();
		//audioSource.PlayOneShot(GameManager.instance.audioOK, 0.7F);
	}

	public void resetPos() {
		this.transform.position = originalPos;
	}

}
