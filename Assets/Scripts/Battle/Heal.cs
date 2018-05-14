using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    protected int type = 0;
    protected int healMod = 10;

    public Heal(string _name, int _healMod)
    {
        this.name = _name;
        this.healMod = _healMod;
        aoe = true;
    }

    public override void Activate(Fighter user, Fighter target)
    {
        float modiHeal = healMod;
        modiHeal *= ((float)user.getint() / 10f);
        float randomMod = Random.Range((-0.25f) * modiHeal, 0.25f * modiHeal);
        modiHeal += randomMod;
        target.TakeHealing((int)modiHeal);
        user.Cast();
        Debug.Log(user.getname() + " uses " + name + " on " + target.getname() + " for " + (int)modiHeal + ", Health Remaining " + target.getHP());
        Debug.Log(randomMod);
        Debug.Log("----------------------");

    }
}
