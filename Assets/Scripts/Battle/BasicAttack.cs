using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {
    protected int type = 0;
    protected int damage = 10;

    public override void Activate(Fighter user, Fighter target) {
        float modiDamage = damage;
        modiDamage *= ((float)user.getstr() / 10f);
        Random.Range((-0.25f) * modiDamage, 0.25f * modiDamage);
        target.TakeDamage((int)modiDamage);
        user.Attack();
        Debug.Log(user.getname()+" attacks " + target.getname() + " for "+ modiDamage + ", Health Remaining " + target.getHP());
        Debug.Log("----------------------");
    }
}
