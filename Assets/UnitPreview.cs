using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPreview : MonoBehaviour {

    public Transform Capsule;

    // Use this for initialization
    void Start()
    {
        Capsule = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 worldPos;
        LayerMask mask = 1 << 8;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, mask))
        {
            worldPos = hit.point;
        }
        else
        {
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        Capsule.transform.position = worldPos;
    }
}
