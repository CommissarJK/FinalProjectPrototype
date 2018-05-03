using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAnimations : MonoBehaviour {
    private Animator anime;

	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack() {
        anime.Play("Attack");
    }

    public void Cast()
    {
        anime.Play("Cast");
    }

    public void Damaged() {
        anime.Play("Damaged");
    }
}
