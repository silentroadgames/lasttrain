﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using MalagaJam.LastTrain;

public class GameManager : MonoBehaviour {

	public class NobodiesGroup {
		int min, max;
		float minX, maxX, minY, maxY;
		private string layerName;
		private Transform nobodiesHolder;
		private SpriteRenderer sprite;

		public AudioClip audioOK, audioKO;

		public void init(
			GameObject layer, 
			List<SpriteRenderer> nobodies, 
			GameObject[] nobodiesLib, 
			int min, int max, 
			float minX, float maxX, 
			float minY, float maxY, 
			string layerName
		) {
			this.min = min;
			this.max = max;
			this.minX = minX;
			this.maxX = maxX;
			this.minY = minY;
			this.maxY = maxY;
			this.layerName = layerName;

			for (int i=0; i<max; i++) {
				//addObj ("Hero", layer, nobodiesLib, minX, maxX, minY, maxY, 0, "Foreground", 1);
				float myX = Random.Range(minX, maxX);
				float myY = Random.Range(minY, maxY);
				float scale = -myY;

				//addObj ("Hero", layer, nobodiesLib, -1.56f, -1.56f, -0.68f ,-0.68f, scale, "Foreground", 1);

				GameObject nobody = nobodiesLib[Random.Range(0, nobodiesLib.Length)];
				GameObject instance = Instantiate(nobody, new Vector2(myX, myY), Quaternion.identity) as GameObject;
				SpriteRenderer sprite = instance.GetComponent<SpriteRenderer> ();
				sprite.sortingLayerName = this.layerName;

				nobodies.Add(sprite);

				instance.transform.localScale = new Vector2 (scale, scale);
				instance.transform.SetParent(nobodiesHolder);
			}
		}
	}
		
	const string version = "0.0.1";

	const int MAX_TURNS_PER_HUMAN = 5;
	const float MIN_YPOS_FOREGROUND = -0.7f;
	const float MAX_YPOS_FOREGROUND = -2f;

	public static GameManager instance = null;
	public 	GameObject[] nobodiesLib;
	public 	GameObject[] humansLib;
	public 	GameObject[] emotionsLib;
	public 	GameObject[] dialogsLib;
	public 	GameObject[] iconsLib;
	public GameObject bubble;
	public GameObject playerLayer;
	public GameObject reactionClicked;
	public GameObject selectedDialog;

	public GameObject hero, target, targetBubble, heroBubble;


	private List<SpriteRenderer> nobodies = new List<SpriteRenderer>();

	NobodiesGroup groupA = new NobodiesGroup();
	NobodiesGroup groupB = new NobodiesGroup();
	NobodiesGroup groupC = new NobodiesGroup();

	void Awake () {
		Debug.Log("Last Train - version: " + version);

		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(instance);
	}

	// Use this for initialization
	void Start () {
		nextScene ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject addObjFromLib(
		string name, 
		GameObject parentObj, 
		GameObject[] lib, 
		float minX, float maxX, 
		float minY, float maxY, float scale, 
		string layerName, int layerOrder,
		bool flipX
	) {
		
		GameObject obj = lib[Random.Range(0, lib.Length)];

		float myX = Random.Range(minX, maxX);
		float myY = Random.Range(minY, maxY);
	
		GameObject instance = Instantiate(obj, new Vector2(myX, myY), Quaternion.identity) as GameObject;
		SpriteRenderer sprite = instance.GetComponent<SpriteRenderer> ();
		sprite.sortingLayerName = layerName;
		sprite.sortingOrder = layerOrder;
		sprite.flipX = flipX;
		if (scale == 0) scale = -myY;

		instance.transform.localScale = new Vector2 (scale, scale);
		instance.transform.SetParent(parentObj.transform);

		return instance;
	}

	public GameObject addObjFromPrefab(
		string name, 
		GameObject parentObj, 
		GameObject obj, 
		float minX, float maxX, 
		float minY, float maxY, float scale, 
		string layerName, int layerOrder,
		bool flipX
	) {
		float myX = Random.Range(minX, maxX);
		float myY = Random.Range(minY, maxY);

		GameObject instance = Instantiate(obj, new Vector2(myX, myY), Quaternion.identity) as GameObject;
		SpriteRenderer sprite = instance.GetComponent<SpriteRenderer> ();
		sprite.sortingLayerName = layerName;
		sprite.sortingOrder = layerOrder;
		sprite.flipX = flipX;
		if (scale == 0) scale = -myY;

		instance.transform.localScale = new Vector2 (scale, scale);
		instance.transform.SetParent(parentObj.transform);

		return instance;
	}

	void resetNobodies() {
		GameObject nobodiesLayer = new GameObject("Nobodies");
		//(min, max, minX, maxX, minY, maxY, sortingLayerName)

		foreach(SpriteRenderer nobody in nobodies) {
			Destroy(nobody);
		}

		groupA.init(nobodiesLayer, nobodies, nobodiesLib, 20, 25, -4.5f, -0.5f, -0.85f, -1.0f, "SurfaceForeground");
		groupB.init(nobodiesLayer, nobodies, nobodiesLib, 20, 25, -2.0f, -0.5f, -0.85f, -2f, "First");
		groupC.init(nobodiesLayer, nobodies, nobodiesLib, 0, 10, -4.5f, -0.5f, -1.5f, -2f, "First");
	}

	void resetPlayers() {
		GameObject playersLayer = new GameObject("Layers");

		if (target)
			Destroy (target);
		if (hero)
			Destroy (hero);
		if (heroBubble)
			Destroy (heroBubble);
		
		target = addObjFromLib ("Target", playersLayer, humansLib, -2.6f, -2.6f, -1.1f ,-1.1f, 0.85f, "SurfaceForeground", 1, true);
		//targetBubble = addObjFromPrefab ("TargetBubble", playersLayer, bubble, -2.8f, -2.8f, -0.85f ,-0.85f, 0.85f, "SurfaceForeground", 1, true);

		hero = addObjFromLib ("Hero", playersLayer, humansLib, -3.86f, -3.86f, -1.1f, -1.1f, 0.85f, "SurfaceForeground", 1, false);
		heroBubble = addObjFromPrefab ("HeroBubble", playersLayer, bubble, -3.91f, -3.91f, -0.65f, -0.65f, 0.85f, "SurfaceForeground", 1, false);
		//HumanBehaviour targetHB = target.GetComponent<HumanBehaviour> () as HumanBehaviour;

		resetEmotions ();
		//targetHB.checkReaction (Feelings.Joy);
	}

	void resetEmotions() {
		GameObject obj;
		GameObject playersLayer = new GameObject("Layers");
		string feeling;
		Debug.Log ("resetEmotions");
		/*
		for( int i = 0; i < targetBubble.transform.childCount; ++i ) {
			//obj = targetBubble.transform.GetChild (i).gameObject;
			//if (target.GetComponent<HumanBehaviour> ().currentFeeling != obj.name) {
			//	obj.SetActive (false);
			//} else {
			//	obj.transform.position = new Vector2 (-2.8f, -0.83f);
			//}
		}
		*/
		if (selectedDialog != null) {
			Destroy (selectedDialog);
		}
		feeling = target.GetComponent<HumanBehaviour> ().currentFeeling;
		Debug.Log ("nextFeeling to " + feeling);
		obj = getEmotionFromDialog(feeling);			
		selectedDialog = addObjFromPrefab ("Emo", playersLayer, obj, -2.39f, -2.39f, -0.65f ,-0.5f, 0.85f, "First", 1, false);

	}

	public GameObject getEmotionFromDialog(string dialog) {
		string pos = Random.Range (0, 4).ToString();

		foreach (GameObject d in dialogsLib) {
			if (d.name == dialog + pos) {
				return d;
			}
		}

		return null;
	}

	public void nextFeeling(string emotion) {
		Reaction reaction = reactionClicked.GetComponent<Reaction> ();
		reaction.resetPos ();
		resetEmotions();
	}

	public void nextScene() {
		resetNobodies ();
		resetPlayers ();
	}
}
