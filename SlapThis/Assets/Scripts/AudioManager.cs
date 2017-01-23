using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    //Singleton
    public static AudioManager Instance {
        get {
            if( instance == null ) instance = FindObjectOfType<AudioManager>();
            return instance;
        }
        set {
            instance = value;
        }
    }

    public AudioSource[] slaps;
    public AudioSource backgroundMusic;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
