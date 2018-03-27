using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerTimer : MonoBehaviour {
    public int playerNumber;
    public BattleControl battle;
    protected Slider slider;
    //public Image fill;


    // Use this for initialization
    void Start () {
        slider = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        slider.value = battle.getPlayerTimer(playerNumber);
        //slider.
    }
}
