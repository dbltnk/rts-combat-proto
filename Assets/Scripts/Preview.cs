using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {

    private Transform Capsule;

    void Start()
    {
        Capsule = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
		Capsule.transform.position = InputManager.MousePosToWorldPos();
    }
}
