using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour {
	[Tooltip("Points per power increment/decrement")]
	public float powerInterval;

	// Use this for initialization
	void Start () {
		
	}
	
	// Power meter up and down
	void Update () {
		GetComponent<Slider> ().value = Mathf.PingPong (Time.time * powerInterval, 100);
	}
}
