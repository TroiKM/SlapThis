using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public int creationTimeInterval;
	private int _creationTimeInterval;
	[Tooltip("Seconds between each slap point destruction")]
	public int destroyTimeInterval;
	private int _destroyTimeInterval;

	// Slap points prefab
	[Tooltip("Slap point to be hit by the user")]
	public GameObject slapPoint;

	// Pleasure meter: Score feedback meter
	[Tooltip("Score meter")]
	public Slider pleasureSlider;

	void Start () {
		StartCoroutine (CreateSlapPoints());
		ScoreCalculations.OnScoreZoneEnter += ChangeTimeIntervals;

		// Sets orginal time intervals
		_creationTimeInterval = creationTimeInterval;
		_destroyTimeInterval = destroyTimeInterval;
	}

	void OnDestroy() {
		ScoreCalculations.OnScoreZoneEnter -= ChangeTimeIntervals;
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
					// Destroys the slap point
					this.InvokeAfterSeconds(_destroyTimeInterval, () => {
						if(slapObj == null) return;
						Destroy(slapObj);
						// Decreases player score
						if(OnSlapDestroyed != null) OnSlapDestroyed(destroyPointAmount);
					});

					// Return to frame renderer
					yield return new WaitForSeconds(_creationTimeInterval);
				}
			}

		}
	}

	void ChangeTimeIntervals (int range) {
		Debug.Log ("ChangeTimeIntervals");
		switch (range) {
		case 0:
			_creationTimeInterval = creationTimeInterval;
			_destroyTimeInterval = destroyTimeInterval * 2;
			break;
		case 1:
			_creationTimeInterval = creationTimeInterval;
			_destroyTimeInterval = destroyTimeInterval;
			break;
		case 2:
			_creationTimeInterval = creationTimeInterval / 2;
			_destroyTimeInterval = destroyTimeInterval / 2;
			break;
		default:
			break;
		}
	}
}
