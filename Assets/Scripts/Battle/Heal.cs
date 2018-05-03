using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    protected int type = 0;
    protected int healMod = 10;
    protected string name;

    public Heal(string _name, int _healMod)
    {
        this.name = _name;
        this.healMod = _healMod;
    }

    public override void Activate(Fighter user, Fighter target)
    {
        float modiHeal = healMod;
        modiHeal *= ((float)user.getint() / 10f);
        target.TakeHealing((int)modiHeal);
        user.Attack();
        Debug.Log(user.getname() + " uses " + name + " on " + target.getname() + " for " + modiHeal + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}
