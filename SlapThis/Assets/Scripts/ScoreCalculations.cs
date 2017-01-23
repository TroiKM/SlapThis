using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculations : MonoBehaviour {

	[Header("User input")]
	// User controls broadcaster
	[Tooltip("User controllers input manager")]
	public InputController inputController;

    [Header("Canvas")]
    public GameObject pnlResults;
    public GameObject sprWin;
    public GameObject sprLose;


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
	[Tooltip("Audio clips for different power meter's")]
	public AudioClip[] slapClips;
	public AudioSource audioSource;

	// For points calculation

	// Ranges for power meter
	private Range[] _powerRanges;
	// Ranges for slap points creation (score's ranges)
	private Range[] _scoreRanges;

	// For slap points creation/destruction time interval
	public delegate void SetScoreZoneEvent(int range);
	public static event SetScoreZoneEvent OnScoreZoneEnter;

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

		// Create power zones for point calculations
		_powerRanges = new Range[4];
		float powerInterval = powerSlider.maxValue / 4.0f;
		_powerRanges [0] = new Range (0, powerInterval);
		_powerRanges [1] = new Range (powerInterval, powerInterval * 2);
		_powerRanges [2] = new Range (powerInterval * 2, powerInterval * 3);
		_powerRanges [3] = new Range (powerInterval * 3, powerInterval * 4);

		// Create score zones for slap points creation
		_scoreRanges = new Range[3];
		float scoreInterval = pleasureSlider.maxValue / 3.0f;
		_scoreRanges [0] = new Range (0, scoreInterval);
		_scoreRanges [1] = new Range (scoreInterval, scoreInterval * 2);
		_scoreRanges [2] = new Range (scoreInterval * 2, scoreInterval * 3);
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
	void ReducePoints(int amount) {

		// Checks for losing state
		if (pleasureSlider.value - amount <= pleasureSlider.minValue) {
            //TODO: FRIENDZONE DUDE
            //Debug.Log ("FRIENDZONE DUDE");
            pnlResults.SetActive(true);
            sprLose.SetActive(true);
		} else {
			pleasureSlider.value -= amount;
		}

		// Calculates the score zone for the slap points creation/destuction
		CalculateScoreZone ();
	}

	// Method for points gain
	void IncresasePoints() {
		// Get the amount of the slap's power
		float power = powerSlider.value;

		// Points to gain
		int pointAmount = 0;

		// Sets the points according to the power meter
		int range = 0;
		for(range = 0; range < 4; range++) {
			if (_powerRanges [range].Contains (power)) {
				break;
			}
		}

		switch (range) {
		// Very low pleasure
		case 0:
			pointAmount = 2;
			audioSource.PlayOneShot(slapClips[0]);
			break;
		// Low pleasure
		case 1:
			pointAmount = 3;
			audioSource.PlayOneShot(slapClips[1]);
			break;
		// Normal pleasure
		case 2:
			pointAmount = 4;
			audioSource.PlayOneShot(slapClips[2]);
			break;
		// High pleasure
		case 3:
			pointAmount = 5;
			audioSource.PlayOneShot(slapClips[3]);
			break;
		default:
			break;
		}

		// Checks for winning state
		if (pleasureSlider.value + pointAmount >= pleasureSlider.maxValue) {
            pnlResults.SetActive(true);
            sprWin.SetActive(true);
			//Debug.Log ("You Win!");
            
		} else {
			pleasureSlider.value += pointAmount;
		}
			
		// Calculates the score zone for the slap points creation/destuction
		CalculateScoreZone ();
	}

	void CalculateScoreZone() {


		for(int range = 0; range < 3; range++) {
			if (_scoreRanges [range].Contains (pleasureSlider.value)) {
				if (OnScoreZoneEnter != null) OnScoreZoneEnter (range);
				break;
			}
		}
	}
}