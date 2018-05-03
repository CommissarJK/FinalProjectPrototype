using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {
    protected int timer = 50;

    public virtual void Activate(Fighter user, Fighter target) { }
    public int getTimer() { return timer; }
}
