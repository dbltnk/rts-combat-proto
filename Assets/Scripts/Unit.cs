using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private GameObject PFX;
    private GameObject Target;
    public float Speed = 7f;
    private bool isDoneSpawning = false;

    void Start ()
    {
        PFX = this.gameObject.transform.GetChild(1).gameObject;
        Target = GameObject.Find("Target");
        StartCoroutine(StopPFX());
    }

    IEnumerator StopPFX()
    {
        yield return new WaitForSeconds(3f);
        PFX.SetActive(false);
        isDoneSpawning = true;
    }

    void Update()
    {
        if (isDoneSpawning) Move();
    }

    private void Move()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
    }
}