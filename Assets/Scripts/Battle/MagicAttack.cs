using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : Ability
{
    protected int element = -1;
    protected int type = 1;
    protected int damage = 10;

    public MagicAttack(string _name, int _damage, int _element)
    {
        this.name = _name;
        this.damage = _damage;
        this.element = _element;
    }

    public override void Activate(Fighter user, Fighter target)
    {
        float modiDamage = damage;
        modiDamage *= ((float)user.getint() / 10f);
        Random.Range((-0.25f) * modiDamage, 0.25f * modiDamage);
        target.TakeDamage((int)modiDamage, element);
        user.Cast();
        Debug.Log(user.getname() + " uses " + name + " on " + target.getname() + " for " + modiDamage + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}