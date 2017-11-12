using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {

    private Gameplay gp;
    private Transform Capsule;

    void Start()
    {
        gp = (Gameplay)FindObjectOfType(typeof(Gameplay));
        Capsule = this.gameObject.transform.GetChild(0);
    }

    void Update()
    {
        Capsule.transform.position = gp.MousePosToWorldPos();
    }
}
