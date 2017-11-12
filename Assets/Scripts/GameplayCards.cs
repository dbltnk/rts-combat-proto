using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCards : MonoBehaviour {

    public UnityEngine.UI.Text crystalsText;
    public UnityEngine.UI.Text crystalsCapText;
    public UnityEngine.UI.Text supplyText;
    public UnityEngine.UI.Text popCostsText;

    public float crystalsRaw;
    public float crystalsCapRaw;
    public int crystalsCap;
    public float timePerCrystal;
    public float timePerCrystalCapIncrease;
    public int crystalsCapMax;

    public int supply;
    public int supplySoftCap;

    public float speedFactor;
    public float regenerationModifier;

    public float crystalsPerMinuteBase;
    public float crystalsPerMinute;

    public List<GameObject> Cards;
    public float randomKillDelay;
    public float chanceToDie;

    // Use this for initialization
    void Start () {
        crystalsPerMinuteBase = 1 / timePerCrystal * 60;
        InvokeRepeating("PickAndKillUnit", randomKillDelay, randomKillDelay);
    }
	
	// Update is called once per frame
	void Update () {
        
        crystalsCapRaw += Time.deltaTime / timePerCrystalCapIncrease * speedFactor;
        crystalsCap = Mathf.Min(Mathf.FloorToInt(crystalsCapRaw), crystalsCapMax);
        crystalsCapText.text = "Crystal Cap: "+ crystalsCap.ToString();

        regenerationModifier = 1f - ((float)supply / (float)supplySoftCap);
        crystalsRaw += Time.deltaTime / timePerCrystal * speedFactor * regenerationModifier;
        crystalsRaw = Mathf.Min(crystalsRaw, crystalsCap) ;
        crystalsText.text = string.Format("Crystals: {0:0.00}", crystalsRaw);

        supplyText.text = "Supply Used: " + supply.ToString();

        crystalsPerMinute = 1 / timePerCrystal * regenerationModifier * 60;
        popCostsText.text = string.Format("Production: +{0:0.00} ({1:0.00})", crystalsPerMinute, crystalsPerMinute - crystalsPerMinuteBase);
    }

    public void SpawnUnit(int cost)
    {
        if (crystalsRaw >= cost)
        {
            crystalsRaw -= cost;
        }
    }

    public void PickAndKillUnit()
    {
        if (Random.Range(0f, 1f) <= chanceToDie)
        {
            int cardPosition = Mathf.RoundToInt(Random.Range(0, 8));
            Card c = Cards[cardPosition].GetComponent<Card>();
            if (c.useCurrent > 0)
            {
                c.useCurrent -= 1;
                supply -= c.cost;
            }
        }
    }
}
