using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public string Name;
    public Color Color;
    public int Price;
    public int UseMax;
    public int UseCurrent;
    public int Amount;
    public float TimeoutMax;
    public float TimeoutCurrent;
    public float Size;
    public float RunSpeed;
    public float Range;
    public float HealthMax;
    public float HealthCurrent;
    public float AttackSpeed;
    public float Damage;
    public float MaxTargets;

    public UnityEngine.UI.Text nameText;
    public UnityEngine.UI.Text costText;
    public UnityEngine.UI.Text useText;
    public UnityEngine.UI.Text timerText;

    public GameObject gameplayRoot;
    GameplayCards gp;
    public UnityEngine.UI.Button button;

    public void Setup(string name, Color color, int price, int useMax, int amount, float timeoutMax, float size, float runSpeed, float range, float health, float attackSpeed, float damage, float maxTargets)
    {
         Name = name;
         Color = color;
         Price = price;
         UseMax = useMax;
         Amount = amount;
         TimeoutMax = timeoutMax;
         TimeoutCurrent = 0f;
         Size = size;
         RunSpeed = runSpeed;
         Range = range;
         HealthMax = health;
         HealthCurrent = health;
         AttackSpeed = attackSpeed;
         Damage = damage;
         MaxTargets = maxTargets;
}
    
    void Start () {
        nameText = transform.Find("Name").GetComponent<UnityEngine.UI.Text>();
        costText = transform.Find("Cost").GetComponent<UnityEngine.UI.Text>();
        useText = transform.Find("Uses").GetComponent<UnityEngine.UI.Text>();
        timerText = transform.Find("Timer").GetComponent<UnityEngine.UI.Text>();

        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<GameplayCards>();
        transform.SetParent(gp.Deck.transform);
        button = GetComponent<UnityEngine.UI.Button>(); 
    }
	
	void Update () {
        nameText.text = Name;
        costText.text = string.Concat("[", Price, "]");
        useText.text = string.Concat(UseCurrent, "/", UseMax);
        timerText.text = string.Concat(Mathf.Floor(TimeoutCurrent).ToString(), "s");
        if (TimeoutCurrent > 0f) TimeoutCurrent -= Time.deltaTime;
        if (TimeoutCurrent < 0f) TimeoutCurrent = 0f;
        if (gp.crystalsRaw < Price || UseCurrent >= UseMax || TimeoutCurrent > 0f)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void Activate()
    {
        if (gp.crystalsRaw >= Price && UseCurrent < UseMax && TimeoutCurrent <= 0f)
        {
            gp.SpawnUnit(Price);
            UseCurrent += 1;
            TimeoutCurrent = TimeoutMax;
            gp.supply += Price;
        }
    }
}
