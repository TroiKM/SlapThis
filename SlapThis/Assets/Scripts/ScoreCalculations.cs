using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculations : MonoBehaviour {

	[Header("User input")]
	// User controls broadcaster
	[Tooltip("User controllers input manager")]
	public InputController inputController;

	[Header("Score calculation")]
	// Points for every action
	[Tooltip("Default score slider value")]
	public int pleasurePoints;
	[Tooltip("Point penalty for every hit failed")]
	public int hitFailReducePointsAmount;
	// Pleasure meter: Score feedback meter
	[Tooltip("Score meter")]
	public Slider pleasureSlider;
	// Force meter: How much point does it earns when slap the butt
	[Tooltip("Hit power meter")]
	public Slider powerSlider;
	// For points calculation
	private Range[] _pleasureRanges;

	struct Range {
		private float _minVal;
		private float _maxVal;

		public Range(float min, float max) {
			this._minVal = min;
			this._maxVal = max;
		}

		public float Min {
			get {
				return _minVal;
			}
			set {
				_minVal = value; 
			}
		}

		public float Max {
			get {
				return _maxVal;
			}
			set {
				_maxVal = value; 
			}
		}

		public bool Contains(float x) {
			return (x == _minVal || x == _maxVal || (x > _minVal && x < _maxVal));
		}
	}

	// Use this for initialization
	void Start () {
		inputController.OnTouchDown += GetTouchDown;	
		gSpots.OnSlapDestroyed += ReducePoints;

		// Create pleasure zones for point calculations
		_pleasureRanges = new Range[4];
		float pleasureInterval = powerSlider.maxValue / 4.0f;
		_pleasureRanges [0] = new Range (0, pleasureInterval);
		_pleasureRanges [1] = new Range (0, pleasureInterval * 2);
		_pleasureRanges [2] = new Range (0, pleasureInterval * 3);
		_pleasureRanges [3] = new Range (0, pleasureInterval * 4);
	}

	void OnDestroy() {
		inputController.OnTouchDown -= GetTouchDown;
		gSpots.OnSlapDestroyed -= ReducePoints;
	}

	// Get slap from user
	void GetTouchDown(Vector3 touchPosition) {
		RaycastHit hit;
		if (Physics.Raycast(touchPosition,Vector3.forward, out hit)) {
			// When touched a slap point
			if (hit.collider.CompareTag (Tags.SLAP_POINT)) {
				Destroy (hit.transform.gameObject);
				IncresasePoints();
			}
			// When missed a slap point, reduce excite points
			else {
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

		// Sets the points according to the power meter
		int range = 0;
		for(range = 0; range < 4; range++) {
			if (_pleasureRanges [range].Contains (power)) {
				break;
			}
		}

		switch (range) {
		// Very low pleasure
		case 0:
			pleasureSlider.value += 2;
			break;
		// Low pleasure
		case 1:
			pleasureSlider.value += 3;
			break;
		// Normal pleasure
		case 2:
			pleasureSlider.value += 4;
			break;
		// High pleasure
		case 3:
			pleasureSlider.value += 5;
			break;
		default:
			break;
		}
	}
}