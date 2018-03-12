using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter {

    // Use this for initialization


    public Player(int _strength = 10, int _dexterity = 10, int _vitality = 10, int _intellect = 10, int _prefab = 0) {
        this.strength = _strength;
        this.dexterity = _dexterity;
        this.vitality = _vitality;
        this.intellect = _intellect;
        this.prefab = _prefab;
        maxHP = this.vitality * 10;
        List<int> abilities = new List<int>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
