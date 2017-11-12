using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public int cost;
    public int useMax;
    public int useCurrent;
    public float recastTimer;
    public float currentTimer;

    public UnityEngine.UI.Text costText;
    public UnityEngine.UI.Text useText;
    public UnityEngine.UI.Text timerText;

    public GameObject gameplayRoot;
    GameplayCards gp;
    public UnityEngine.UI.Button button;
    
    // Use this for initialization
    void Start () {
        gp = gameplayRoot.GetComponent<GameplayCards>();
        button = GetComponent<UnityEngine.UI.Button>(); 
    }
	
	// Update is called once per frame
	void Update () {
        costText.text = string.Concat("[", cost, "]");
        useText.text = string.Concat(useCurrent, "/", useMax);
        timerText.text = string.Concat(Mathf.Floor(currentTimer).ToString(), "s");
        if (currentTimer > 0f) currentTimer -= Time.deltaTime;
        if (currentTimer < 0f) currentTimer = 0f;
        if (gp.crystalsRaw < cost || useCurrent >= useMax || currentTimer > 0f)
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
        if (gp.crystalsRaw >= cost && useCurrent < useMax && currentTimer <= 0f)
        {
            gp.SpawnUnit(cost);
            useCurrent += 1;
            currentTimer = recastTimer;
            gp.supply += cost;
        }
    }
}
