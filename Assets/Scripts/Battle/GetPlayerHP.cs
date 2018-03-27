using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerHP : MonoBehaviour {

    public int playerNumber;
    public BattleControl battle;

    void Update()
    {
        string output = "";
        output = battle.getPlayerHP(playerNumber).ToString() + " / " +(string)battle.getPlayerMaxHP(playerNumber).ToString()+" HP";
        GetComponent<Text>().text = output;
    }
}
