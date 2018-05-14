using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityInfo : MonoBehaviour {
    private int difficulty = 1;
    private int exp = 0;
    private List<int> quests;
    private string cityName;
    

	// Use this for initialization
	void Start () {
        cityName = "DefaultName " + Random.Range(0,10);
        GameObject.Find("Player").GetComponent<OverPlayerController>().addCity(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addExp(int _exp) {
        this.exp += _exp;
        int neededToLevel = (difficulty * difficulty) * 100;
        if (exp >= neededToLevel) {
            this.exp = 0;
            _exp -= neededToLevel;
            difficulty++;
            addExp(_exp);
        }
    }

    public string getName() { return cityName; }
    public int getLevel() { return difficulty; }
}
