using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    // Use this for initialization
    public Enemy(string _name, int _strength, int _dexterity, int _vitality, int _intellect, int _prefab, List<int> _abilities)
    {
        this.name = _name;
        this.strength = _strength;
        this.dexterity = _dexterity;
        this.vitality = _vitality;
        this.intellect = _intellect;
        this.prefab = _prefab;
        this.abilities = _abilities;
        maxHP = this.vitality * 10;
        HP = maxHP;     

    }
    public int PickAbility()
    {
        return 0;
    }

    public void setID(int ID) {
        name = name + " " + ID;
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

    public Enemy Clone() {
        Enemy temp = new Enemy(name, strength, dexterity, vitality, intellect, prefab, abilities);
        return temp;
    }

}
