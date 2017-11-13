using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour {

    public float speed = 1f;
    public Vector3 TargetPos;
    public GameObject Circle;

	public bool ShowCircle;

	// Use this for initialization
    void Start () {
        TargetPos = transform.position;
        Circle = this.gameObject.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		UpdateMove();
        UpdateCircle();
    }

	private void UpdateCircle()
	{
		Circle.gameObject.SetActive(ShowCircle);
	}

	public void SetMoveTarget(Vector3 pos)	{
		TargetPos = pos;	
	}
    
	private void UpdateMove()
	{
        float step = speed * Time.smoothDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }
}
