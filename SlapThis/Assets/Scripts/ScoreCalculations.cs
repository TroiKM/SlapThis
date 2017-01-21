using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculations : MonoBehaviour {

	// User controls broadcaster
	public InputController inputController;
	// Points for every action
	public int pleasurePoints;
	public int hitSuccessPointsAmount;
	public int hitFailReducePointsAmount;
	// Pleasure meter: Score feedback meter
	public Slider pleasureSlider;
	// Force meter: How much point does it earns when slap the butt
	public Slider powerSlider;

	// Use this for initialization
	void Start () {
		
		inputController.OnTouchDown += GetTouchDown;	
		gSpots.OnSlapDestroyed += ReducePoints;
	}

	void OnDestroy() {
		inputController.OnTouchDown -= GetTouchDown;
		gSpots.OnSlapDestroyed -= ReducePoints;
	}

	// Get slap from user
	void GetTouchDown(Vector3 touchPosition) {
		RaycastHit hit;
		if (Physics.Raycast(touchPosition,Vector3.forward, out hit)) {

			Debug.DrawRay (touchPosition, Vector3.forward);
			// When touched a slap point
			if (hit.collider.CompareTag (Tags.SLAP_POINT)) {
				Destroy (hit.transform.gameObject);
				ReducePoints (-hitSuccessPointsAmount);
			}
			// When missed a slap point, reduce excite points
			else {
				// TODO QUITAR PUNTOS

				ReducePoints(hitFailReducePointsAmount);
			}
		}
	}

	// Method for points loss
	void ReducePoints(int amount){
		pleasurePoints -= amount;

		pleasureSlider.value = pleasurePoints;

		if(pleasurePoints <= 0) {
			//TODO: FRIENDZONE DUDE
		}
	}

	// Method for points gain
	void IncresasePoints() {
		// Get the amount of the slap's power
		float power = powerSlider.value;

		// TODO CREAR LA CONVERSION DE FUERZA A PUNTOS

	}
}