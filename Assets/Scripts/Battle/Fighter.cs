using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter {

    protected int maxHP;
    protected int HP;
    protected int strength;
    protected int dexterity;
    protected int vitality;
    protected int intellect;
    protected List<int> abilities;
    protected float timer = 0;
    protected int defencePre = 0;
    protected bool KO = false;
    protected int prefab;
    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Tick() {

    }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0) { KO = true; }
    }

    public float getTimer() { return timer; }

    public int getstr() { return strength; }
    public int getdex() { return dexterity; }
    public int getvit() { return vitality; }
    public int getint() { return intellect; }
    public int getPrefab() { return prefab; }
}
