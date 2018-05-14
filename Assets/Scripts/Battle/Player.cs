using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter {

    protected int level = 1;
    protected int gearLevel = 0;
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
        this.abilities = _abilities;

    }

    override public void TakeDamage(int damage, int element = -1)
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

    override public float GetBonusDamage() { return 1f+((float)(gearLevel) * 0.1f); }

    public void levelUp(int _level) {
        this.level = _level;
        strength *= level;
        dexterity *= level;
        vitality *= level;
        intellect *= level;
        maxHP = this.vitality * 10;
        HP = maxHP;
    }

    public void upgrade(int i) { gearLevel = i; }

    public void fullHeal() { HP = maxHP; }
}
