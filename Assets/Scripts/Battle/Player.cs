using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter {

    // Use this for initialization
    public Player(string _name, int _strength, int _dexterity, int _vitality, int _intellect, int _prefab, List<int> _abilities) {
        this.name = _name;
        this.strength = _strength;
        this.dexterity = _dexterity;
        this.vitality = _vitality;
        this.intellect = _intellect;
        this.prefab = _prefab;
        maxHP = this.vitality * 10;
        HP = maxHP;
        List<int> abilities = new List<int>();
    }

    override public void TakeDamage(int damage)
    {
        HP -= damage;
        anime.Damaged();
        if (HP <= 0)
        {
            HP = 0;
            KO = true;
            timer = 0f;
        }
    }
}
