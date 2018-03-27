using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {
    protected int element = -1;
    protected int type = 0;
    protected int damage = 10;

    public override void Activate(Fighter user, Fighter target) {
        float modiDamage = damage;
        modiDamage *= ((float)user.getstr() / 10f);
        target.TakeDamage((int)modiDamage);
        Debug.Log(user.getname()+" attacks " + target.getname() + " for "+ modiDamage + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}
