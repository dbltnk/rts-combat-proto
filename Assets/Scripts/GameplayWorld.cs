using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameplayCards))]
public class GameplayWorld : MonoBehaviour {

	public const int SLOT_NULL = -1;
    public int UnitSelected = SLOT_NULL;

    public float SpawnDistance = 15f;
    public bool ChampionWasSpawned = false;
    public GameObject ChampionPrefab;

    public GameObject ChampionPreviewObject;
    public GameObject UnitPreviewCapsule;
	public Champion Champion;

	private GameplayCards gpCards;

	void Awake() {
		gpCards = GetComponent<GameplayCards>();
	}

    // Use this for initialization
    void Start () {
        UnitPreviewCapsule.SetActive(false);
    }

    void SpawnUnit(int unitIndex)
    {
        Vector3 worldPos = InputManager.MousePosToWorldPos();

        if(Vector3.Distance(worldPos, Champion.transform.position) <= SpawnDistance)
        {
			var config = gpCards.CardConfigs[unitIndex];
			Instantiate(config.UnitPrefab, worldPos, Quaternion.identity);
			UnitSelected = SLOT_NULL;
			UnitPreviewCapsule.SetActive(false);
        }       
    }

	public void SpawnUnit(Vector3 pos) {
		if (!ChampionWasSpawned)
		{
			GameObject.Destroy(ChampionPreviewObject);
			var go = Instantiate(ChampionPrefab, pos, Quaternion.identity);
			Champion = go.GetComponent<Champion>();
			ChampionWasSpawned = true;
		}
		else if (UnitSelected != SLOT_NULL)
		{
			SpawnUnit(UnitSelected);
		}
	}

	public void TriggerSlot(int slot) {
		if (UnitSelected == slot || slot >= gpCards.CardConfigs.Count)
		{
			// deselect
			UnitSelected = SLOT_NULL;
			UnitPreviewCapsule.SetActive(false);
		}
		else
		{
			// select
			UnitSelected = slot;
			UnitPreviewCapsule.SetActive(true);
		}
	}

    // Update is called once per frame
    void Update () {

		if (Champion != null) Champion.ShowCircle = UnitSelected != SLOT_NULL;

        if (ChampionWasSpawned)
        {            
			var inRange = Vector3.Distance(InputManager.MousePosToWorldPos(), Champion.transform.position) <= SpawnDistance;

			if (UnitSelected != SLOT_NULL) {
				var config = gpCards.CardConfigs[UnitSelected];

				var c = config.Color;
				if (!inRange) {
					c = new Color(c.r - 0.25f, c.g - 0.25f, c.b - 0.25f, 0.25f);
				}

				UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
			} else {
				UnitPreviewCapsule.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
			}
        }
    }

}
