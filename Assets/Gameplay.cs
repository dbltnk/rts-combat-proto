using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

    public bool HasStarted = false;
    public GameObject ChampionPrefab;
    public GameObject U1pref;
    public GameObject U2pref;
    public GameObject U3pref;
    public GameObject ChampionPreviewObject;
    public int UnitSelected = 0;
    public float SpawnDistance = 15f;
    public GameObject Champion;

    // Use this for initialization
    void Start () {
		
	}

    void SpawnUnit(int unitID)
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 worldPos;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            worldPos = hit.point;
        }
        else
        {
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if(Vector3.Distance(worldPos, Champion.transform.position) <= SpawnDistance)
        {
            switch (unitID)
            {
                case 1:
                    Instantiate(U1pref, worldPos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(U2pref, worldPos, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(U3pref, worldPos, Quaternion.identity);
                    break;
                default:
                    print("No unit selected.");
                    break;
            }
        }

        
    }

    // Update is called once per frame
    void Update () {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

        if (Input.GetMouseButtonDown(0)) {

            if (!HasStarted)
            {
                Vector3 worldPos;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    worldPos = hit.point;
                }
                else
                {
                    worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                }
                GameObject.DestroyImmediate(ChampionPreviewObject);
                Champion = Instantiate(ChampionPrefab, worldPos, Quaternion.identity);
                HasStarted = true;
            }
            else
            {
                SpawnUnit(UnitSelected);
            }
        }

        if (Input.GetKeyDown("1"))
        {
            if (UnitSelected == 1)
            {
                UnitSelected = 0;
            }
            else {
                UnitSelected = 1;
            }     
        }
        else if (Input.GetKeyDown("2"))
        {
            if (UnitSelected == 2)
            {
                UnitSelected = 0;
            }
            else
            {
                UnitSelected = 2;
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            if (UnitSelected == 3)
            {
                UnitSelected = 0;
            }
            else
            {
                UnitSelected = 3;
            }
        }


    }
}
