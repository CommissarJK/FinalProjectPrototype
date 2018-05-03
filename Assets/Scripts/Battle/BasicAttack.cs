using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {
    protected int type = 0;
    protected int damage = 10;
    protected string name = "Attack";

    public override void Activate(Fighter user, Fighter target) {
        float modiDamage = damage;
        modiDamage *= ((float)user.getstr() / 10f);
        target.TakeDamage((int)modiDamage);
        user.Attack();
        Debug.Log(user.getname()+" attacks " + target.getname() + " for "+ modiDamage + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}
