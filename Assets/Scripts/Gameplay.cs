using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

    public int UnitSelected = 0;
    public float SpawnDistance = 15f;
    public bool ChampionWasSpawned = false;
    public GameObject ChampionPrefab;
    public GameObject U1pref;
    public GameObject U2pref;
    public GameObject U3pref;
    public GameObject ChampionPreviewObject;
    public GameObject UnitPreviewCapsule;
    public GameObject Champion;

    // Use this for initialization
    void Start () {
        UnitPreviewCapsule.SetActive(false);
    }

    void SpawnUnit(int unitID)
    {
        Vector3 worldPos = InputManager.MousePosToWorldPos();

        if(Vector3.Distance(worldPos, Champion.transform.position) <= SpawnDistance)
        {
            switch (unitID)
            {
                case 1:
                    Instantiate(U1pref, worldPos, Quaternion.identity);
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                    break;
                case 2:
                    Instantiate(U2pref, worldPos, Quaternion.identity);
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                    break;
                case 3:
                    Instantiate(U3pref, worldPos, Quaternion.identity);
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                    break;
                default:
                    break;
            }
        }       
    }

	public void SpawnUnit(Vector3 pos) {
		if (!ChampionWasSpawned)
		{
			GameObject.Destroy(ChampionPreviewObject);
			Champion = Instantiate(ChampionPrefab, pos, Quaternion.identity);
			ChampionWasSpawned = true;
		}
		else
		{
			SpawnUnit(UnitSelected);
		}
	}

    // Update is called once per frame
    void Update () {

        if (ChampionWasSpawned)
        {
            if (Input.GetKeyDown("1"))
            {
                if (UnitSelected == 1)
                {
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                }
                else
                {
                    UnitSelected = 1;
                    UnitPreviewCapsule.SetActive(true);
                }
            }
            else if (Input.GetKeyDown("2"))
            {
                if (UnitSelected == 2)
                {
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                }
                else
                {
                    UnitSelected = 2;
                    UnitPreviewCapsule.SetActive(true);
                }
            }
            else if (Input.GetKeyDown("3"))
            {
                if (UnitSelected == 3)
                {
                    UnitSelected = 0;
                    UnitPreviewCapsule.SetActive(false);
                }
                else
                {
                    UnitSelected = 3;
                    UnitPreviewCapsule.SetActive(true);
                }
            }

			if (Vector3.Distance(InputManager.MousePosToWorldPos(), Champion.transform.position) <= SpawnDistance)
            {
                switch (UnitSelected)
                {
                    case 1:
                        UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        break;
                    case 2:
                        UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        break;
                    case 3:
                        UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
        }
    }

}
