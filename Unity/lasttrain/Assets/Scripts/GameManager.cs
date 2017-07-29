using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

	const string version = "0.0.1";
	const int MAX_NOBODIES_BACKGROUND = 10;
	const int MAX_NOBODIES_FIRSTPLANE = 10;

	const int MAX_TURNS_PER_HUMAN = 5;

	const float MIN_XPOS = -4.5f;
	const float MAX_XPOS = -0.5f;

	const float MIN_YPOS_BACKGROUND = -0.62f;
	const float MAX_YPOS_BACKGROUND = -0.75f;

	const float MIN_YPOS_FOREGROUND = -0.7f;
	const float MAX_YPOS_FOREGROUND = -2f;

	public static GameManager instance = null;
	public 	GameObject[] nobodiesLib;

	private List<GameObject> nobodies = new List<GameObject>();
	private GameObject nobodiesGO;
	private Transform nobodiesHolder;
	private SpriteRenderer nobodiesSprite;

	void Awake () {
		Debug.Log("Last Train - version: " + version);
	}

	// Use this for initialization
	void Start () {
		resetNobodies ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void resetNobodies() {
		
		nobodiesGO = new GameObject ("Nobodies");
		nobodiesHolder = nobodiesGO.transform;

		nobodiesSprite = nobodiesGO.renderer;
		nobodiesSprite.sortingLayerName = "foreground";

		nobodies.Clear ();

		for (int i=0; i<MAX_NOBODIES_BACKGROUND; i++) {
			GameObject nobody = nobodiesLib[Random.Range(0, nobodiesLib.Length)];

			float myX = Random.Range(MIN_XPOS, MAX_XPOS);
			float myY = Random.Range(MIN_YPOS_BACKGROUND, MAX_YPOS_BACKGROUND);

			GameObject instance = Instantiate(nobody, new Vector2(myX, myY), Quaternion.identity) as GameObject;
			instance.transform.SetParent(nobodiesHolder);
		}

	}
}
