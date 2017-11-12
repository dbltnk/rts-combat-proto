using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private GameObject PFX;
    public float Speed = 7f;
    private bool isDoneSpawning = false;

    void Start ()
    {
        PFX = this.gameObject.transform.GetChild(1).gameObject;
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
        transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, step);
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}