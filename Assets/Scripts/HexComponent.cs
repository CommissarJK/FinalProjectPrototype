using UnityEngine;
using System.Collections;

public class HexComponent : MonoBehaviour {
    public Hex hex;
    public HexMap hexMap;

    public void UpdatePosition() {
        this.transform.position = hex.PositionFromCamera(Camera.main.transform.position, hexMap.numColumns, hexMap.numRows);
    }
}
