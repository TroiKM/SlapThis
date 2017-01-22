using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gSpots : MonoBehaviour {

	public delegate void ReducePointsEvent(int amount);
	public static event ReducePointsEvent OnSlapDestroyed;

	[Header("Butt model")]
	[Tooltip("GameObject for the butt 3d-model")]
	public Transform butt;

	[Header("Slap points zone's related variables")]
	[Tooltip("Valid hit radius for the slaps")]
	public float slapRadius;
	[Tooltip("Amount of points to be reduce when a slap point disappears")]
	public int destroyPointAmount;
	[Tooltip("Seconds between each slap point creation")]
	public float creationTimeInterval;
	[Tooltip("Seconds between each slap point destruction")]
	public float destroyTimeInterval;

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
					this.InvokeAfterSeconds(destroyTimeInterval, () => {
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
					yield return new WaitForSeconds(creationTimeInterval);
				}
			}

		}
	}


}
