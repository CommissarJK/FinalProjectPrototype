using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter {

    protected string name;
    protected int maxHP;
    protected int HP;
    protected int maxMP;
    protected int MP;
    protected int strength;
    protected int dexterity;
    protected int vitality;
    protected int intellect;
    protected List<int> abilities;
    protected float timer = 0;
    protected int defencePre = 0;
    protected bool KO = false;
    protected bool active = false;
    protected int prefab;
    protected FighterAnimations anime;

    public void Tick() {
        if (!active && !KO)
        {
            timer += 1f * (((float)dexterity / 10f) + 1f);
            if (timer >= 600f)
            {
                active = true;
                timer = 600f;
            }

        }
    }

    public void ResetTimer() {
        active = false;
        timer = 0;
    }

    virtual public void TakeDamage(int damage, int element = -1) {
        HP -= damage;
        anime.Damaged();
        if (HP <= 0) {
            HP = 0;
            KO = true;
            timer = 0f;
        }
    }

    virtual public float GetBonusDamage() { return 1f; }

    public void TakeHealing(int healing) {
        HP += healing;
        if (HP > maxHP) {
            //HP = maxHP;
        }
        if (KO && HP > 0)
        {
            KO = false;
            timer = 0f;
        }
    }

    public void Attack() { anime.Attack(); }
    public void Cast() { anime.Cast(); }
    public void setAnime(FighterAnimations _anime) { this.anime = _anime; }
    //public FighterAnimations getAnime() { return anime; }
    public float getTimer() { return timer; }

    public string getname() { return name; }
    public int getstr() { return strength; }
    public int getHP() { return HP; }
    public int getmaxHP() { return maxHP; }

    public int getdex() { return dexterity; }
    public int getvit() { return vitality; }
    public int getint() { return intellect; }
    public bool getKO() { return KO; }
    public int getPrefab() { return prefab; }
    public bool getActive() { return active; }
    public int getAbility(int i) { return abilities[i]; }
    public void reset() {
        active = false;
        timer = 0f;
    }

}
