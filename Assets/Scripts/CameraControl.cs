using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    Vector3 oldPosition;
    HexComponent[] hexes;
    // Use this for initialization
    void Start () {
        oldPosition = this.transform.position;
    }

     
   // Update is called once per frame
   void Update () {
        CheckIfCameraMoved();
	    //wasd
        //zoom
	}

    public void PanToHex(Hex Hex) {
        //move to hex
    }

    void CheckIfCameraMoved() {
        if (oldPosition != this.transform.position)
        {
            oldPosition = this.transform.position;
            if (hexes == null) { 
            hexes = GameObject.FindObjectsOfType<HexComponent>();
            }
            foreach (HexComponent hex in hexes) {
                hex.UpdatePosition();
            }
        }
    }
}
