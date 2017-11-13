using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour {

	void Start () {
		var gp = GameObject.FindObjectOfType<Gameplay>();
		InputManager.Instance.ControlledGameplay = gp;
	}
}
