using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {
    public Ability ability;
    public Fighter user;
    public List<Fighter> targets;
    protected int timer;

    public Action(Ability _ability, Fighter _user, Fighter _target) {
        this.user = _user;
        this.targets = new List<Fighter>();
        this.targets.Add(_target);
        this.ability = _ability;
        timer = ability.getTimer();
    }

    public Action(Ability _ability, Fighter _user, List<Fighter> _target)
    {
        this.user = _user;
        this.targets = _target;
        this.ability = _ability;
        timer = ability.getTimer();
    }

    public void Animate() { }

    public void Activate() {
        for (int i = 0; i < targets.Count; i++)
        {
            ability.Activate(user, targets[i]);
        }
    }
}
