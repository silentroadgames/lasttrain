using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

using System;

namespace MalagaJam.LastTrain
{
	public enum Feelings {Fear, Sadness, Contempt, Fury};

	/// <summary>
	/// A emotion of the table of emotions.
	/// </summary>
	public class Emotion
	{
		public float Joy;
		public float Fear;
		public float Sadness;
		public float Contempt;
		public float Fury;
		public float Silence;

		public Emotion(float fear, float sadness, float contempt, float fury, float silence)
		{
			Fear = fear;
			Sadness = sadness;
			Contempt = contempt;
			Fury = fury;
			Silence = silence;
		}
	}

	// Every human reacts in a predictive way to an emotion.
	public class BasicReactions
	{
		public Emotion baseFear 		= new Emotion(1, -1, -1, -1, -1);
		public Emotion baseSadness 		= new Emotion(-1, 1, -1, -1, 1);
		public Emotion baseContempt 	= new Emotion(-1, -1, 1, -1, -1);
		public Emotion baseFury 		= new Emotion(-1, -1, -1, -1, 1);
		public Emotion baseSilence 		= new Emotion(-1, 1, -1, 1, 1);
	}

	/// <summary>
	/// Every instance is a CPU player we can interact with.
	/// </summary>
	public class HumanBehaviour : MonoBehaviour {

		#region FIELDS
		public const int MAX_EMOTION_VALUE = 2;
		public const float EMOTION_DELAY = 1.0f;
		public Vector2 initialPosition;

		public int turn;                // ronda actual.
		public float health;

		public float fear, sadness, contempt, fury;
		[Range(1, 100)]public int nDoomed;             // 100 significa totalmente gris.
		public BasicReactions br;       // An human has basic reactions

		string[] feelings = new string[] {"Fear", "Sadness", "Contempt", "Fury"};
		public string currentFeeling;

		#endregion

		#region CONSTRUCTORS
		public HumanBehaviour()
		{
			// 2) Gestionar parametros
			br = new BasicReactions();
		}
		#endregion

		#region METHODS
		/// <summary>
		/// Compara la emocion del jugador con la del humano instanciado
		/// e incrementa una emocion basandonos en la tabla.
		/// </summary>
		/// <returns>void.</returns>
		/// 
		/// StartCoroutine(
		/// 
		/// 
	
		public void checkReaction (string emotion) {
			switch (emotion) {
			case "Fear":
				switch (currentFeeling) {
				case "Fear":
					fear += br.baseFear.Fear;
					break;
				case "Sadness":
					sadness += br.baseFear.Sadness;
					break;
				case "Contempt":
					contempt += br.baseFear.Contempt;
					break;
				case "Fury":
					fury += br.baseFear.Fury;
					break;
				}
				break;
			case "Sadness":
				switch (currentFeeling) {
				case "Fear":
					fear += br.baseSadness.Fear;
					break;
				case "Sadness":
					sadness += br.baseSadness.Sadness;
					break;
				case "Contempt":
					contempt += br.baseSadness.Contempt;
					break;
				case "Fury":
					fury += br.baseSadness.Fury;
					break;
				}
				break;
			case "Contempt":
				switch (currentFeeling) {
				case "Fear":
					fear += br.baseContempt.Fear;
					break;
				case "Sadness":
					sadness += br.baseContempt.Sadness;
					break;
				case "Contempt":
					contempt += br.baseContempt.Contempt;
					break;
				case "Fury":
					fury += br.baseContempt.Fury;
					break;
				}
				break;
			case "Fury":
				switch (currentFeeling) {
				case "Fear":
					fear += br.baseFury.Fear;
					break;
				case "Sadness":
					sadness += br.baseFury.Sadness;
					break;
				case "Contempt":
					contempt += br.baseFury.Contempt;
					break;
				case "Fury":
					fury += br.baseFury.Fury;
					break;
				}
				break;
			case "Silence":
				switch (currentFeeling) {
				case "Fear":
					fear += br.baseSilence.Fear;
					break;
				case "Sadness":
					sadness += br.baseSilence.Sadness;
					break;
				case "Contempt":
					contempt += br.baseSilence.Contempt;
					break;
				case "Fury":
					fury += br.baseSilence.Fury;
					break;
				}
				break;
			}

			if (fear > MAX_EMOTION_VALUE) {
				fear = MAX_EMOTION_VALUE;
			}
			if (contempt > MAX_EMOTION_VALUE) {
				contempt = MAX_EMOTION_VALUE;
			}
			if (fury > MAX_EMOTION_VALUE) {
				fury = MAX_EMOTION_VALUE;
			}
			if (sadness > MAX_EMOTION_VALUE) {
				sadness = MAX_EMOTION_VALUE;
			}

			/*
			if (fear < 0) {
				fear = 0;
			}
			if (contempt < 0) {
				contempt = 0;
			}
			if (fury < 0) {
				fury = 0;
			}
			if (sadness < 0) {
				sadness = 0;
			}
			*/

			updateHealth (false);
			Debug.Log ("checkReaction " + emotion +" currentFeeling="+currentFeeling+" - " + fear+","+sadness+","+contempt+","+fury + " HEALTH: "+ health.ToString() );

			StartCoroutine (delayCheckReaction(emotion));
		}

		private void updateHealth(Boolean hero) {
			health = (float)(fear + sadness + contempt + fury) / (float)MAX_EMOTION_VALUE / 4;
			GameManager gameB = GameManager.instance.GetComponent<GameManager> ();

			SpriteRenderer sprite = this.GetComponent<SpriteRenderer> ();
			SpriteRenderer dialogSprite = gameB.selectedDialog.GetComponent<SpriteRenderer> ();

			sprite.color = new Color (health, health, health, 1.0f);

			if (!hero) {
				sprite.transform.position = new Vector2 (0.5f + initialPosition.x - (1.0f * health), initialPosition.y);
				dialogSprite.transform.position = new Vector2 (0.65f + initialPosition.x - (1.0f * health), initialPosition.y + 0.4f);

			}
		}

		public IEnumerator delayCheckReaction (string emotion) {

			yield return new WaitForSeconds(EMOTION_DELAY);

			int pos = Random.Range (0, feelings.Length-1);
			currentFeeling = feelings[pos];

			Debug.Log ("feelings[" + pos + "] = " + currentFeeling);

			GameManager gameB = GameManager.instance.GetComponent<GameManager> ();
			gameB.nextFeeling(currentFeeling);

			yield return null;

		}
		#endregion

		#region EVENTS
		/// <summary>
		/// Se actualiza el valor  alpha de la textura en el game loop.
		/// </summary>
		void updateDoomedValue () {
			//nDoomed = ((fear + sadness + contempt + fury) / 5);
		}

		/// <summary>
		/// Muestra la GUI de reiniciar.
		/// </summary>
		void triggerTryAgainGUI () {
			//Debug.Log ("TODO: Crear GUI de Reiniciar.");
		}
		#endregion

		#region GAME LOOP
		void Awake () {
			SpriteRenderer sprite = this.GetComponent<SpriteRenderer> ();
			int pos = Random.Range (0, feelings.Length-1);
			currentFeeling = feelings[pos];
			Debug.Log("Current: " + currentFeeling);
		}

		void Start () {
			Boolean hero = false;
			GameManager gameB = GameManager.instance.GetComponent<GameManager>();

			if (this.transform.position.x == gameB.hero.transform.position.x) {
				hero = true;
				fear = sadness = contempt = fury = MAX_EMOTION_VALUE;
			} else {
				fear = Random.Range (1, MAX_EMOTION_VALUE);
				sadness = Random.Range (1, MAX_EMOTION_VALUE);
				contempt = Random.Range (1, MAX_EMOTION_VALUE);
				fury = Random.Range (1, MAX_EMOTION_VALUE);
			}

			initialPosition = this.transform.position;
			updateHealth (hero);
		}

		// Se llama Update una vez por frame.
		void Update () {
			this.updateDoomedValue ();
			if (this.nDoomed != 100) {
				triggerTryAgainGUI();
			};
		}
		#endregion
	}
}