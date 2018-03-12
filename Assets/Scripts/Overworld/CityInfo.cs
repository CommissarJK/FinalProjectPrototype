using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityInfo : MonoBehaviour {
    private int difficulty = 0;
    private int exp = 0;
    private List<int> quests;
    private string cityName;
    

	// Use this for initialization
	void Start () {
        cityName = "DefaultName " + Random.Range(0,10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addExp(int _exp) {
        this.exp += _exp;
    }

    public string getName() {
        return cityName;
    }
}
