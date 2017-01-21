using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gSpots : MonoBehaviour {

	public delegate void ReducePointsEvent(int amount);
	public static event ReducePointsEvent OnSlapDestroyed;

	// Butt gameobject
	public Transform butt;
	public float slapRadius;
	public int destroyPointAmount;

	// Slap points prefab
	[Tooltip("Slap point to be hit by the user")]
	public GameObject slapPoint;

	void Start () {
		StartCoroutine (CreateSlapPoints());
	}

	IEnumerator CreateSlapPoints() {
		while (true) {
			// direction vector
			Vector3 directionVector = (butt.transform.position + Random.insideUnitSphere * slapRadius) - transform.position;

			RaycastHit hit;
			if (Physics.Raycast(transform.position,directionVector * 100, out hit)) {
				if (hit.collider.CompareTag (Tags.CULITO)) {
					Debug.DrawRay (transform.position, directionVector);
					GameObject slapObj = Instantiate (slapPoint, hit.point - directionVector * 0.1f, Quaternion.identity) as GameObject;
//
					this.InvokeAfterSeconds(3, () => {
						if(slapObj == null) return;

						Destroy(slapObj);
						// TODO QUITAR PUNTOS
						if(OnSlapDestroyed != null) OnSlapDestroyed(destroyPointAmount);
					});
//					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//					Vector3 spherePosition = hit.point - directionVector * 0.1f;
//					sphere.transform.position = spherePosition;
//					sphere.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//					sphere.tag = Tags.SLAP_POINT;

					// Return to frame renderer
					yield return new WaitForSeconds(2);
				}
			}

		}
	}


}
