using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public bool battleStarting;
    public Transform PlayerPos;
    Vector3 oldPosition;
    HexComponent[] hexes;
    // Use this for initialization
    void Start()
    {
        oldPosition = this.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!battleStarting)
        {
            this.transform.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y + 2.5f, PlayerPos.position.z - 1.5f);
            CheckIfCameraMoved();
            oldPosition = this.transform.position;
        }
        //wasd
        //zoom
    }

    public void PanToHex(Hex Hex)
    {
        //move to hex
    }

    void CheckIfCameraMoved()
    {
        if (!battleStarting)
        {
            if (oldPosition != this.transform.position)
            {
                oldPosition = this.transform.position;
                if (hexes == null)
                {
                    hexes = GameObject.FindObjectsOfType<HexComponent>();
                }
                foreach (HexComponent hex in hexes)
                {
                    hex.UpdatePosition();
                }
            }
        }
    }

    public void BattleCameraMovement()
    {
        battleStarting = true;
        transform.position += transform.forward * Time.deltaTime * 3.5f;
        transform.Rotate(0, 0, Time.deltaTime * 40f);
    }
    public void BattleCameraSet() {
        transform.rotation = Quaternion.Euler(10, 10, 0);
        transform.position = new Vector3(-0.5f, 2f, 598f);
    }
    public void ResetCamera() {
        transform.rotation = Quaternion.Euler(60, 0, 0);
        transform.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y + 2.5f, PlayerPos.position.z - 1.5f);
        battleStarting = false;
    }
}
