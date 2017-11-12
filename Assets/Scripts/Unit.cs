using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private GameObject PFX;

    void Start ()
    {
        PFX = this.gameObject.transform.GetChild(1).gameObject;
        StartCoroutine(StopPFX());
    }

    IEnumerator StopPFX()
    {
        yield return new WaitForSeconds(3f);
        PFX.SetActive(false);
    }
}
