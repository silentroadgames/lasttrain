using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MalagaJam.LastTrain
{
	public enum Feelings {Joy, Fear, Sadness, Contempt, Fury, Silence};

	/// <summary>
	/// A emotion of the table of emotions.
	/// </summary>
	public class Emotion
	{
		public int Joy;
		public int Fear;
		public int Sadness;
		public int Contempt;
		public int Fury;
		public int Silence;

		public Emotion(int joy, int fear, int sadness, int contempt, int fury)
		{
			Joy = joy;
			Fear = fear;
			Sadness = sadness;
			Contempt = contempt;
			Fury = fury;
		}
	}

	// Every human reacts in a predictive way to an emotion.
	public class BasicReactions : MonoBehaviour
	{
		public Emotion baseJoy 			= new Emotion(1, -1, -1, -1, -1);
		public Emotion baseFear 		= new Emotion(1, 1, -1, -1, -1);
		public Emotion baseSadness 		= new Emotion(1, -1, 1, -1, -1);
		public Emotion baseContempt 	= new Emotion(1, -1, -1, 1, -1);
		public Emotion baseFury 		= new Emotion(-1, 1, 1, 1, 1);
		public Emotion baseSilence 		= new Emotion(1, -1, 1, -1, -1);
	}

	/// <summary>
	/// Every instance is a CPU player we can interact with.
	/// </summary>
	public class Human : BasicReactions {

		#region FIELDS
		public int joy;
		public int fear;
		public int sadness;
		public int contempt;
		public int fury;
		public int turn;                // ronda actual.
		public int nDoomed;             // 100 significa totalmente gris.
		public Feelings currentFeeling; // Emoción que está sintiendo el humano.
		#endregion

		#region CONSTRUCTORS
		public Human(int jo, int fe, int sa, int co, int fu)
		{
			// 1) Randomizar la primera emocion del humano
			System.Random rnd = new System.Random();
			currentFeeling = (Feelings)rnd.Next(0, 4);

			// 2) Gestionar parametros
			joy = jo;
			fear = fe;
			sadness = sa;
			contempt = co;
			fury = fu;
		}
		#endregion

		#region METHODS
		/// <summary>
		/// Compara la emocion del jugador con la del humano instanciado
		/// e incrementa una emocion basandonos en la tabla.
		/// </summary>
		/// <returns>void.</returns>
		void checkReaction (Feelings playerEmotion) {

			switch (playerEmotion.ToString())
			{
			case "Joy":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseJoy.Joy;
					break;
				case "Fear":
					fear += baseJoy.Fear;
					break;
				case "Sadness":
					sadness += baseJoy.Sadness;
					break;
				case "Contempt":
					contempt += baseJoy.Contempt;
					break;
				case "Fury":
					fury += baseJoy.Fury;
					break;
				}
				break;
			case "Fear":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseFear.Joy;
					break;
				case "Fear":
					fear += baseFear.Fear;
					break;
				case "Sadness":
					sadness += baseFear.Sadness;
					break;
				case "Contempt":
					contempt += baseFear.Contempt;
					break;
				case "Fury":
					fury += baseFear.Fury;
					break;
				}
				break;
			case "Sadness":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseSadness.Joy;
					break;
				case "Fear":
					fear += baseSadness.Fear;
					break;
				case "Sadness":
					sadness += baseSadness.Sadness;
					break;
				case "Contempt":
					contempt += baseSadness.Contempt;
					break;
				case "Fury":
					fury += baseSadness.Fury;
					break;
				}
				break;
			case "Contempt":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseContempt.Joy;
					break;
				case "Fear":
					fear += baseContempt.Fear;
					break;
				case "Sadness":
					sadness += baseContempt.Sadness;
					break;
				case "Contempt":
					contempt += baseContempt.Contempt;
					break;
				case "Fury":
					fury += baseContempt.Fury;
					break;
				}
				break;
			case "Fury":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseJoy.Joy;
					break;
				case "Fear":
					fear += baseJoy.Fear;
					break;
				case "Sadness":
					sadness += baseJoy.Sadness;
					break;
				case "Contempt":
					contempt += baseJoy.Contempt;
					break;
				case "Fury":
					fury += baseJoy.Fury;
					break;
				}
				break;
			case "Silence":
				switch (currentFeeling.ToString ()) {
				case "Joy":
					joy += baseSilence.Joy;
					break;
				case "Fear":
					fear += baseSilence.Fear;
					break;
				case "Sadness":
					sadness += baseSilence.Sadness;
					break;
				case "Contempt":
					contempt += baseSilence.Contempt;
					break;
				case "Fury":
					fury += baseSilence.Fury;
					break;
				}
				break;
			}
		}
		#endregion

		#region EVENTS
		/// <summary>
		/// Se actualiza el valor  alpha de la textura en el game loop.
		/// </summary>
		void updateDoomedValue () {
			nDoomed = ((joy + fear + sadness + contempt + fury) / 5);
		}

		/// <summary>
		/// Muestra la GUI de reiniciar.
		/// </summary>
		void triggerTryAgainGUI () {
				Debug.Log ("TODO: Crear GUI de Reiniciar.");
		}
		#endregion

		#region GAME LOOP
		// Usa esto para inicializar
		void Start () {
			
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
