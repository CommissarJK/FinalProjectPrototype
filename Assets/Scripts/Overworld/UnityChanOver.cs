using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanOver : MonoBehaviour {

    private Animator anime;
    private float inputX = 0.0f;
    private float inputY = 0.0f;
    private List<GameObject> cities;

    // Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
        cities = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        anime.SetFloat("speed", Mathf.Clamp(InputManager.MainJoystick().magnitude,-1.0f,1.0f));
    }

    public void getCity(GameObject city) {
        cities.Add(city);
    }
}
