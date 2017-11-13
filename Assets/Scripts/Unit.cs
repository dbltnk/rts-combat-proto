using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public float Speed = 7f;
	public float ParticleDuration = 3f;
	public string TargetTag = "Enemy";

	private GameObject PFX;
    private bool isDoneSpawning = false;

	public float RetargetTimeout = 0.1f;

	private GameObject target = null;

    void Start ()
    {
        PFX = this.gameObject.transform.GetChild(1).gameObject;
        StartCoroutine(StopPFX());
		StartCoroutine(Retarget());
    }

	IEnumerator Retarget() {
		while(true) {
			target = FindClosestEnemy();
			yield return new WaitForSeconds(RetargetTimeout);
		}
	}

    IEnumerator StopPFX()
    {
		yield return new WaitForSeconds(ParticleDuration);
        PFX.SetActive(false);
        isDoneSpawning = true;
    }

    void Update()
    {
        if (isDoneSpawning) UpdateMove();
    }

    private void UpdateMove()
    {
		if (target != null) {
			float step = Speed * Time.smoothDeltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
		}
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(TargetTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
		if (gos.Length == 0) return null;
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