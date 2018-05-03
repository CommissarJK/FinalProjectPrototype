using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttack : Ability
{
    protected int element = -1;
    protected int type = 0;
    protected int damage = 10;
    protected string name = "";

    public StrongAttack(string _name, int _damage, int _element)
    {
        this.name = _name;
        this.damage = _damage;
        this.element = _element;
    }

    public override void Activate(Fighter user, Fighter target)
    {
        float modiDamage = damage;
        modiDamage *= ((float)user.getstr() / 10f);
        target.TakeDamage((int)modiDamage);
        user.Attack();
        Debug.Log(user.getname() + " uses " + name + " on " + target.getname() + " for " + modiDamage + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}