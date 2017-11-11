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
        Move();
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
    
private void Move()
    {
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
           // gp.UnitSelected = 0;
           // gp.up.SetActive(false);
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }
}
