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
    public GameObject Deck;

    public float randomKillDelay;
    public float chanceToDie;

    public GameObject PrefUICard;

    // Use this for initialization
    void Start () {
        crystalsPerMinuteBase = 1 / timePerCrystal * 60;
        InvokeRepeating("PickAndKillUnit", randomKillDelay, randomKillDelay);

        // create cards
        GameObject ObjectCleaver = Instantiate(PrefUICard);
        Card CardCleaver = ObjectCleaver.GetComponent<Card>();
        CardCleaver.Setup(
            "Cleaver", // name
            Color.red, // color
            4, // price
            3, // useMax
            1, // amount
            30f, // timeoutMax
            3f, // size
            1f, // runspeed
            1f, // range
            100f, // health
            4f, // attackSpeed
            160f, // damage
            1f // maxTargets
            );

        GameObject ObjectSwarmers = Instantiate(PrefUICard);
        Card CardCSwarmers = ObjectSwarmers.GetComponent<Card>();
        CardCSwarmers.Setup(
            "Swarmers", // name
            Color.blue, // color
            2, // price
            3, // useMax
            4, // amount
            30f, // timeoutMax
            1f, // size
            1f, // runspeed
            1f, // range
            40f, // health
            1f, // attackSpeed
            5f, // damage
            1f // maxTargets
            );

        GameObject ObjectSpinner = Instantiate(PrefUICard);
        Card CardSpinner = ObjectSpinner.GetComponent<Card>();
        CardSpinner.Setup(
            "Spinner", // name
            Color.green, // color
            2, // price
            3, // useMax
            1, // amount
            30f, // timeoutMax
            2f, // size
            1f, // runspeed
            1f, // range
            160f, // health
            2f, // attackSpeed
            20f, // damage
            4f // maxTargets
            );
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
            if (c.UseCurrent > 0)
            {
                c.UseCurrent -= 1;
                supply -= c.Price;
            }
        }
    }
}
