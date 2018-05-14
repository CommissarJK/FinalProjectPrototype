using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {

    public Text aAbility;
    public Text bAbility;
    public Text yAbility;
    public Text xAbility;
    private Vector3 pos;

    void Start() {
        pos = gameObject.transform.position;
    }

    public void SetAbilities(string a, string b, string y, string x) {
        aAbility.text = a;
        bAbility.text = b;
        yAbility.text = y;
        xAbility.text = x;
    }

    public void SetPos(int i) {
        switch(i)
        {
            case 0:
                gameObject.transform.position =  pos + new Vector3(-160, 0, 0);
                break;
            case 1:
                gameObject.transform.position = pos;
                break;
            case 2:
                gameObject.transform.position = pos + new Vector3(160, 0, 0);
                break;
        }

    }


}
