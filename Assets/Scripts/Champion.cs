using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour {

    public float speed = 1f;
    public Vector3 TargetPos;
    public Gameplay gp;
    public GameObject Circle;

    // Use this for initialization
    void Start () {
        gp = (Gameplay)FindObjectOfType(typeof(Gameplay));
        TargetPos = transform.position;
        Circle = this.gameObject.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		UpdateMove();
        DoCircle();
    }

	private void DoCircle()
	{
	    if (gp.UnitSelected == 0)
	        {
	            Circle.gameObject.SetActive(false);
	        }
	    else {
	            Circle.gameObject.SetActive(true);
	        }
	}

	public void SetMoveTarget(Vector3 pos)	{
		TargetPos = pos;	
	}
    
	private void UpdateMove()
	{
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }
}
