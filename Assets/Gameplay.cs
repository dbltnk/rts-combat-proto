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
    public GameObject up;

    // Use this for initialization
    void Start () {
        up.SetActive(false);
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
                    UnitSelected = 0;
                    up.SetActive(false);
                    break;
                case 2:
                    Instantiate(U2pref, worldPos, Quaternion.identity);
                    UnitSelected = 0;
                    up.SetActive(false);
                    break;
                case 3:
                    Instantiate(U3pref, worldPos, Quaternion.identity);
                    UnitSelected = 0;
                    up.SetActive(false);
                    break;
                default:
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
                up.SetActive(false);
            }
            else {
                UnitSelected = 1;
                up.SetActive(true);
            }     
        }
        else if (Input.GetKeyDown("2"))
        {
            if (UnitSelected == 2)
            {
                UnitSelected = 0;
                up.SetActive(false);
            }
            else
            {
                UnitSelected = 2;
                up.SetActive(true);
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            if (UnitSelected == 3)
            {
                UnitSelected = 0;
                up.SetActive(false);
            }
            else
            {
                UnitSelected = 3;
                up.SetActive(true);
            }
        }

        Vector3 thisPos;
        Ray thisray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit thishit;
        if (Physics.Raycast(thisray, out thishit, 1000f))
        {
            thisPos = thishit.point;
        }
        else
        {
            thisPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        if (Vector3.Distance(thisPos, Champion.transform.position) <= SpawnDistance)
        {
            up.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        else
        {
            up.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        }
    }
}
