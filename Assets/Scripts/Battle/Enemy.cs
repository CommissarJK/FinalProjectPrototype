using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    // Use this for initialization
    private int currentAbility = 0;
    private List<float> weakness;
    private int exp;
    public Enemy(string _name, int _strength, int _dexterity, int _vitality, int _intellect, int _prefab, List<int> _abilities, int _exp)
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
        this.abilities = _abilities;
        weakness = new List<float>();
        weakness.Add(0f);
        weakness.Add(0f);
        weakness.Add(0f);
        this.exp = _exp;
    }

    public void setWeakness(float fire, float ice, float earth) {
        weakness[0] = fire;
        weakness[1] = ice;
        weakness[2] = earth;
    }

    public int PickAbility()
    {
        int temp = abilities[currentAbility];
        currentAbility++;
        if (currentAbility >= abilities.Count) {
            currentAbility = 0;
        }
        return temp;
    }

    public void setID(int ID) {
        name = name + " " + ID;
    }

    override public void TakeDamage(int damage, int element)
    {
        if (element >= 0)
        {
            damage += (int)((float)damage * (weakness[element] * 0.25f));
        }
        HP -= damage;
        anime.Damaged();
        if (HP <= 0)
        {
            HP = 0;
            KO = true;
            timer = 0f;
        }
    }

    override public float GetBonusDamage() { return 1f; }
    public int getEXP() { return exp; }

    public Enemy Clone() {
        Enemy temp = new Enemy(name, strength, dexterity, vitality, intellect, prefab, abilities, exp);
        return temp;
    }

}
