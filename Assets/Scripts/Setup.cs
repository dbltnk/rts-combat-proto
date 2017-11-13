using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour {

	void Start () {
		var gp = GameObject.FindObjectOfType<GameplayWorld>();
		InputManager.Instance.ControlledGameplay = gp;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
    }
}
