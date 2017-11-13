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

		CreateCard(new Card.Config() {
			Name = "Spinner", // name
			Color = Color.green, // color
			Price = 2, // price
			UseMax = 5, // useMax
			Amount = 1, // amount
			TimeoutMax = 7f, // timeoutMax
			Size = 2f, // size
			RunSpeed = 1f, // runspeed
			Range = 1f, // range
			HealthMax = 160f, // health
			AttackSpeed = 2f, // attackSpeed
			Damage = 10f, // damage
			MaxTargets = 4f // maxTargets				
		});
		CreateCard(new Card.Config(){
			Name = "Swarmers", // name
			Color = Color.blue, // color
			Price = 3, // price
			UseMax = 3, // useMax
			Amount = 4, // amount
			TimeoutMax = 12f, // timeoutMax
			Size = 1f, // size
			RunSpeed = 1f, // runspeed
			Range = 1f, // range
			HealthMax = 40f, // health
			AttackSpeed = 1f, // attackSpeed
			Damage = 5f, // damage
			MaxTargets = 1f // maxTargets
		});
		CreateCard(new Card.Config(){
			Name = "Cleaver",
			Color = Color.red,
			Price = 4,
			UseMax = 2,
			Amount = 1,
			TimeoutMax = 15f,
			Size = 3f,
			Range = 1f,
			HealthMax = 100f,
			AttackSpeed = 4f,
			Damage = 160f,
			MaxTargets = 1f,
		});
    }

	void CreateCard(Card.Config conf){
		GameObject obj = Instantiate(PrefUICard);
		Card card = obj.GetComponent<Card>();
		card.Setup(this, conf);
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
                supply -= c.Conf.Price;
            }
        }
    }
}
