using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gSpots : MonoBehaviour {

	// Butt gameobject
	public Transform butt;
	public float slapRadius;

	void Start () {
		StartCoroutine (CreateSlapPoints());
	}

	void FixedUpdate () {
		
	}

	IEnumerator CreateSlapPoints() {
		while (true) {
			// direction vector
			Vector3 directionVector = (butt.transform.position + Random.insideUnitSphere * slapRadius) - transform.position;

			RaycastHit hit;
			if (Physics.Raycast(transform.position,directionVector * 100, out hit)) {
				if (hit.collider.CompareTag ("Culito")) {
					Debug.DrawRay (transform.position, directionVector);

					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
					Vector3 spherePosition = hit.point - directionVector * 0.1f;
					sphere.transform.position = spherePosition;
					sphere.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

					// Return to frame renderer
					yield return new WaitForSeconds(2);
				}
			}
		}
	}
}
