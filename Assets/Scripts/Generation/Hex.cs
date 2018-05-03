using UnityEngine;
using System.Collections;



public class Hex {

    public readonly int Q; // Column
    public readonly int R; // Row
    public readonly int S; // 3D Coordinate

    public float elevation;
    public float moisture;
    public bool city = false;
    public float difficulty;

    static readonly float WIDTH_MULTIPIER = Mathf.Sqrt(3) / 2;

    float radius = 1f;
    HexMap hexMap;

    public Hex(HexMap hexMap, int q, int r)
    {
        this.hexMap = hexMap;
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    public float HexHeight() {
        return radius * 2;
    }
    public float HexWidth()
    {
        return WIDTH_MULTIPIER * HexHeight();
    }
    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }
    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }


    // returns world space position
    public Vector3 Position() {
        return new Vector3(HexHorizontalSpacing() * (this.Q + this.R/2f), 0, HexVerticalSpacing() * this.R);
    }



    public Vector3 PositionFromCamera(Vector3 CaperaPosition, float numColumns, float numRows) {
        float mapWidth = numColumns * HexHorizontalSpacing();
        float mapHeight = numRows * HexVerticalSpacing();
        Vector3 position = Position();

        if (hexMap.allowEastWest)
        {
            float widthsFromCamera = (position.x - CaperaPosition.x) / mapWidth;

            if (widthsFromCamera > 0)
            {
                widthsFromCamera += 0.5f;
            }
            else
            {
                widthsFromCamera -= 0.5f;
            }

            int widthsToFix = (int)widthsFromCamera;

            position.x -= widthsToFix * mapWidth;
        }
        if (hexMap.allowNorthSouth)
        {
            float heightsFromCamera = (position.z - CaperaPosition.z) / mapHeight;

            if (heightsFromCamera > 0)
            {
                heightsFromCamera += 0.5f;
            }
            else
            {
                heightsFromCamera -= 0.5f;
            }

            int heightsToFix = (int)heightsFromCamera;

            position.z -= heightsToFix * mapHeight;
        }
        return position;        
    }

    public static float Distance(Hex a, Hex B) {
        int dQ = Mathf.Abs(a.Q - B.Q);
        if (a.hexMap.allowEastWest)
        {
            if (dQ > a.hexMap.numColumns / 2) { dQ = a.hexMap.numColumns - dQ; }
        }

        int dR = Mathf.Abs(a.R - B.R);
        if (a.hexMap.allowNorthSouth)
        {
            if (dR > a.hexMap.numRows / 2) { dR = a.hexMap.numRows - dR; }
        }

        return Mathf.Max(dQ, dR, Mathf.Abs(a.S - B.S));
    }

}
