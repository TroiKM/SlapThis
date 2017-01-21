using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculations : MonoBehaviour {

	public InputController inputController;
	public int pleasurePoints;
	public int hitSuccessPointsAmount;
	public int hitFailReducePointsAmount;

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
				Debug.Log ("Acabo de nalguear");
				ReducePoints (-hitSuccessPointsAmount);
			}
			// When missed a slap point, reduce excite points
			else {
				// TODO QUITAR PUNTOS

				ReducePoints(hitFailReducePointsAmount);
			}
		}
	}

	void ReducePoints(int amount){
		pleasurePoints -= amount;

		if(pleasurePoints <= 0) {
			//TODO: FRIENDZONE DUDE
		}
	}
}
