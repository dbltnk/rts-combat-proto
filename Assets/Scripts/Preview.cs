using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {

    void Update()
    {
		transform.position = InputManager.MousePosToWorldPos();
    }
}
