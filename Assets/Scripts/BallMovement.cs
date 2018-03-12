using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    private float movementSpeed = 1;
    public BattleControl battler;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("J_MainVertical"));
        //Debug.Log(InputManager.MainVertical());
        if (!battler.activeBattle && !battler.startingBattle) {

            if (Mathf.Abs(InputManager.MainHorizontal()) > 0.2f || Mathf.Abs(InputManager.MainVertical()) > 0.2f)
            {
                transform.position += InputManager.MainJoystick() * Time.deltaTime * movementSpeed;
                transform.rotation = Quaternion.LookRotation(InputManager.MainJoystick());
            }
            /*if ((Input.GetKey("w") && !Input.GetKey("s")))
                {

                    transform.position += transform.forward * Time.deltaTime * movementSpeed;
                }
                else if (Input.GetKey("s") && !Input.GetKey("w"))
                {
                    transform.position -= transform.forward * Time.deltaTime * movementSpeed;
                }

                if (Input.GetKey("d"))
                {
                    transform.Rotate(0, Time.deltaTime * clockwise, 0);
                } else if (Input.GetKey("a"))
                {
                    transform.Rotate(0, Time.deltaTime * (-clockwise), 0);
                }*/
        }
    }
}
