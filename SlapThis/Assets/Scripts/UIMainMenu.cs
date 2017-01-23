using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {
	
	public void ChargeLevel(string level){
		SceneManager.LoadScene (level);
	}
}
