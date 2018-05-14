using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {
    protected int timer = 50;
    protected string name = "Attack";
    protected bool aoe = false;

    public void aoeTrue() { aoe = true; }
    public virtual void Activate(Fighter user, Fighter target) { }
    public int getTimer() { return timer; }
    public string getname() { return name; }
    public bool getAOE() { return aoe; }
}
