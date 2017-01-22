using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour {
	[Tooltip("Points per power increment/decrement")]
	public float powerInterval;

	// Slider color filler
	private Transform _sliderFill;

	// Max value for the power slider
	private float _maxPower;

	// Use this for initialization
	void Start () {
		_sliderFill = transform.Find ("Fill Area/Fill");
		_maxPower = (GetComponent<Slider> () != null) ? GetComponent<Slider> ().maxValue : 100.0f;
	}
	
	// Power meter up and down
	void Update () {
		GetComponent<Slider> ().value = Mathf.PingPong (Time.time * powerInterval, _maxPower);
		if (_sliderFill.GetComponent<Image> () != null) {
			_sliderFill.GetComponent<Image> ().color = Color.Lerp(Color.green, Color.red, 
				Mathf.PingPong(Time.time, 1));
		}
	}
}
