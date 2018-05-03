using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexMap : MonoBehaviour {

    public GameObject HexPrefab;

    public Mesh MeshWater;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountain;

    public GameObject ForestPrefab;
    public GameObject JunglePrefab;
    public GameObject CityPrefab;

    public Material MatOcean;
    public Material MatCoast;
    public Material MatPlains;
    public Material MatGrasslands;
    public Material MatMountains;
    public Material MatDesert;
    

    [System.NonSerialized]public float HeightMountain = 0.8f;
    [System.NonSerialized]public float HeightHill = 0.6f;
    [System.NonSerialized]public float HeightFlat = 0f;
    [System.NonSerialized]public float HeightCoast = -0.3f;

    [System.NonSerialized]public float MoistureJungle = 66f;
    [System.NonSerialized]public float MoistureForest = 0.33f;
    [System.NonSerialized]public float MoistureGrasslands = 0f;
    [System.NonSerialized]public float MoisturePlains = -0.5f;

    [System.NonSerialized]public int numColumns = 60;
    [System.NonSerialized]public int numRows = 30;

    [System.NonSerialized]public bool allowEastWest = true;
    [System.NonSerialized]public bool allowNorthSouth = false;

    private Hex[,] hexes;
    protected Dictionary<Hex, GameObject> hexToGameObjectMap;

    // Use this for initialization
    void Start()
    {
        GenerateMap();
    }

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array not yet instantiated");
            return null;
        }
            //x = x % numColumns;
            if (x < 0)
            {
                x += numColumns;
            }
            else if (x >= numColumns) {
                x -= numColumns;
            }
        
        
            //y = y % numRows;
            if (y < 0)
            {
                y += numRows;
            }
            else if (y >= numRows)
            {
                y -= numRows;
            }
        
        try
        {
            return hexes[x, y];
        }
        catch {
            Debug.LogError("GetHexAt: " + x + ", " + y);
            return null;
        }

    }

    virtual public void GenerateMap()
    {
        hexes = new Hex[numColumns,numRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();

        //Generate a map filled with ocean
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Instantiate a hex
                Hex h = new Hex(this, column, row);
                h.elevation = -0.5f;
                hexes[column, row] = h;

                Vector3 pos = h.PositionFromCamera(Camera.main.transform.position, numColumns, numRows);

                GameObject hexGO = (GameObject)Instantiate(HexPrefab, pos, Quaternion.identity, this.transform);
                hexToGameObjectMap[h] = hexGO;

                hexGO.name = string.Format("Hex: {0},{1}", column, row);
                hexGO.GetComponent<HexComponent>().hex = h;
                hexGO.GetComponent<HexComponent>().hexMap = this;

                hexGO.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
            }
        }
    }

    public void UpdateHexVisuals()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = hexes[column, row];
                GameObject hexGO = hexToGameObjectMap[h];
                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();
                MeshCollider mc = hexGO.GetComponentInChildren<MeshCollider>();

                if (h.city)
                {
                    h.elevation = HeightFlat + 0.1f;
                    Vector3 p = hexGO.transform.position;
                    Instantiate(CityPrefab, p, Quaternion.identity, hexGO.transform);
                }

                if (h.elevation >= HeightFlat)
                {
                    if (h.moisture >= MoistureJungle)
                    {
                        mr.material = MatGrasslands;
                        if (h.elevation < HeightMountain)
                        {
                            Vector3 p = hexGO.transform.position;
                            if (h.elevation >= HeightHill) { p.y += 0.25f; }
                            Instantiate(ForestPrefab, p, Quaternion.identity, hexGO.transform);
                        }
                    }
                    else if (h.moisture >= MoistureForest)
                    {
                        mr.material = MatGrasslands;
                        if (h.elevation < HeightMountain)
                        {
                            Vector3 p = hexGO.transform.position;
                            if (h.elevation >= HeightHill) { p.y += 0.25f; }
                            Instantiate(ForestPrefab, p, Quaternion.identity, hexGO.transform);
                        }
                    }
                    else if (h.moisture >= MoistureGrasslands)
                    {
                        mr.material = MatGrasslands;
                    }
                    else if (h.moisture >= MoisturePlains)
                    {
                        mr.material = MatPlains;
                    }
                    else
                    {
                        mr.material = MatDesert;
                    }
                }

                //check height
                if (h.elevation >= HeightMountain)
                {
                    mf.mesh = MeshMountain;
                    mr.material = MatMountains;
                    mc.sharedMesh = MeshMountain;
                }
                else if (h.elevation >= HeightHill)
                {
                    mf.mesh = MeshHill;
                    mc.sharedMesh = MeshHill;
                }
                else if (h.elevation >= HeightFlat)
                {
                    mf.mesh = MeshFlat;
                }
                else if (h.elevation >= HeightCoast)
                {
                    mf.mesh = MeshWater;
                    mr.material = MatCoast;
                }
                if (h.elevation < HeightCoast)
                {
                    mf.mesh = MeshWater;
                    bool coast = false;
                    Hex[] hNeighbours = GetHexesInRange(h, 2);
                    foreach (Hex hn in hNeighbours) {
                        if (hn.elevation >= HeightFlat) {
                            coast = true;
                            break;
                        }
                    }
                    if (coast)
                    {
                        mr.material = MatCoast;
                    }
                    else {
                        mr.material = MatOcean;
                    }
                }
            }
        }
    }

    public Hex[] GetHexesInRange(Hex centerHex, int range) {
        List<Hex> results = new List<Hex>();
        
        for (int dx = -range; dx < range; dx++) {
            for (int dy = Mathf.Max(-range,-dx-range); dy < Mathf.Min(range, -dx + range); dy++)
            {
                //results.Add(hexes[centerHex.Q + dx, centerHex.R + dy]);
                results.Add(GetHexAt(centerHex.Q + dx, centerHex.R + dy));
            }
        }
        return results.ToArray();
    }
}
