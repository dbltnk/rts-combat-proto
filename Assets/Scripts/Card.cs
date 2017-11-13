using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	[System.Serializable]
	public struct Config {
		public string Name;
		public Color Color;
		public int Price;
		public int UseMax;
		public int Amount;
		public float TimeoutMax;
		public float Size;
		public float RunSpeed;
		public float Range;
		public float HealthMax;
		public float AttackSpeed;
		public float Damage;
		public float MaxTargets;
	}

	public int UseCurrent;
	public float TimeoutCurrent;
	public float HealthCurrent;

	public Config Conf;

    public Text nameText;
    public Text costText;
    public Text useText;
    public Text timerText;

    public Button button;

	GameplayCards gameplayCards;

	public void Setup(GameplayCards gp, Config c)
    {
		Conf = c;
		gameplayCards = gp;

		TimeoutCurrent = 0f;
		HealthCurrent = c.HealthMax;
		UseCurrent = 0;

		transform.SetParent(gp.Deck.transform);
	}
    
    void Start () {
        nameText = transform.Find("Name").GetComponent<Text>();
        costText = transform.Find("Cost").GetComponent<Text>();
        useText = transform.Find("Uses").GetComponent<Text>();
        timerText = transform.Find("Timer").GetComponent<Text>();
        button = GetComponent<Button>(); 
    }
	
	void Update () {
		if (gameplayCards == null) return;

        nameText.text = Conf.Name;
		costText.text = string.Concat("[", Conf.Price, "]");
		useText.text = string.Concat(UseCurrent, "/", Conf.UseMax);
		timerText.text = string.Concat(Mathf.Floor(TimeoutCurrent).ToString(), "s");
		if (TimeoutCurrent > 0f) TimeoutCurrent -= Time.deltaTime;
		if (TimeoutCurrent < 0f) TimeoutCurrent = 0f;
		if (gameplayCards.crystalsRaw < Conf.Price || UseCurrent >= Conf.UseMax || TimeoutCurrent > 0f)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    void Activate()
    {
		if (gameplayCards.crystalsRaw >= Conf.Price && UseCurrent < Conf.UseMax && TimeoutCurrent <= 0f)
        {
			gameplayCards.SpawnUnit(Conf.Price);
			UseCurrent += 1;
			TimeoutCurrent = Conf.TimeoutMax;
			gameplayCards.supply += Conf.Price;
        }
    }
}
