using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerName : MonoBehaviour {

    public int playerNumber;
    public BattleControl battle;
    protected string name = null;

    void Update() {
        if (name == null)
        {
            name = battle.getPlayerName(playerNumber);
            GetComponent<Text>().text = name;
        }

    }
}
