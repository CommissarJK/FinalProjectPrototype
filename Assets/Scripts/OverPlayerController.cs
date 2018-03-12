using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverPlayerController : MonoBehaviour {

    private float movementSpeed = 1;
    public BattleControl battler;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("J_MainVertical"));
        //Debug.Log(InputManager.MainVertical());
        if (!battler.activeBattle && !battler.startingBattle)
        {

            if (Mathf.Abs(InputManager.MainHorizontal()) > 0.2f || Mathf.Abs(InputManager.MainVertical()) > 0.2f)
            {
                transform.position += InputManager.MainJoystick() * Time.deltaTime * movementSpeed;
                transform.rotation = Quaternion.LookRotation(InputManager.MainJoystick());
            }
        }
    }
}
