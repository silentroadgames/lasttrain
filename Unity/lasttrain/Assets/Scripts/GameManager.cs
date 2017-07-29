using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

	public class NobodiesGroup {
		int min, max;
		float minX, maxX, minY, maxY;
		private string layerName;
		private Transform nobodiesHolder;
		private SpriteRenderer sprite;

		public void init(
			GameObject layer, 
			List<GameObject> nobodies, 
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

			nobodiesHolder = layer.transform;
			nobodies.Clear();

			for (int i=0; i<max; i++) {
				GameObject nobody = nobodiesLib[Random.Range(0, nobodiesLib.Length)];

				float myX = Random.Range(minX, maxX);
				float myY = Random.Range(minY, maxY);
				float scale = -myY;

				GameObject instance = Instantiate(nobody, new Vector2(myX, myY), Quaternion.identity) as GameObject;
				SpriteRenderer sprite = instance.GetComponent<SpriteRenderer> ();
				sprite.sortingLayerName = this.layerName;
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

	private List<GameObject> nobodies = new List<GameObject>();

	NobodiesGroup groupA = new NobodiesGroup();
	NobodiesGroup groupB = new NobodiesGroup();
	NobodiesGroup groupC = new NobodiesGroup();

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
		GameObject nobodiesLayer = new GameObject("Nobodies");
		//(min, max, minX, maxX, minY, maxY, sortingLayerName)
		groupA.init(nobodiesLayer, nobodies, nobodiesLib, 20, 25, -4.5f, -0.5f, -0.72f, -0.85f, "Background");
		groupB.init(nobodiesLayer, nobodies, nobodiesLib, 20, 25, -2.0f, -0.5f, -0.85f, -2f, "First");
		//groupC.init(nobodiesLayer, nobodies, nobodiesLib, 0, 10, -4.5f, -0.5f, -1.5f, -2f, "First");
	}
}
