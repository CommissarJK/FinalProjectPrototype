using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {
    public Ability ability;
    public Fighter user;
    public Fighter target;

    public Action(Ability _ability, Fighter _user, Fighter _target) {
        this.user = _user;
        this.target = _target;
        this.ability = _ability;
    }

    public void Activate() {
        ability.Activate(user, target);
    }
}
