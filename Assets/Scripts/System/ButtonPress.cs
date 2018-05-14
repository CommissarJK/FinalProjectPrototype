using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonPress : MonoBehaviour {
    private float timer = 0;
    private RawImage buttonImg;
    public int buttonID;

	// Use this for initialization
	void Start () {
        buttonImg = GetComponent<RawImage>();
    }
	
	// Update is called once per frame

    void Update() {
        switch (buttonID)
        {
            case 0:
                if (InputManager.Abutton())
                {
                    timer = 0.5f;
                    buttonImg.color = new Color32(105, 105, 105, 255);
                }
                break;
            case 1:
                if (InputManager.Bbutton())
                {
                    timer = 0.5f;
                    buttonImg.color = new Color32(105, 105, 105, 255);
                }
                break;
            case 2:
                if (InputManager.Ybutton())
                {
                    timer = 0.5f;
                    buttonImg.color = new Color32(105, 105, 105, 255);
                }
                break;
            case 3:
                if (InputManager.Xbutton())
                {
                    timer = 0.5f;
                    buttonImg.color = new Color32(105, 105, 105, 255);
                }
                break;
        }

        if (timer > 0) {
            buttonImg.color = new Color(buttonImg.color.r + (1 * Time.deltaTime), buttonImg.color.g + (1 * Time.deltaTime), buttonImg.color.b + (1 * Time.deltaTime), 255);
            timer -= 1 * Time.deltaTime;
            if (timer <= 0) { buttonImg.color = new Color32(255, 255, 255, 255); }
        }
    }
}
