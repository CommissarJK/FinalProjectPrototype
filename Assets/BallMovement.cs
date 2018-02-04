using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    private float movementSpeed = 1;
    private float clockwise = 100f;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") && !Input.GetKey("s"))
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
        }
    }

}
