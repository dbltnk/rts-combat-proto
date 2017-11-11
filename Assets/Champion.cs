using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour {

    public float speed = 1f;
    public Vector3 TargetPos;

    // Use this for initialization
    void Start () {
        TargetPos = transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                TargetPos = hit.point;
            }
            else
            {
                TargetPos = Camera.main.ScreenToWorldPoint(mousePos);
            }
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }

}
