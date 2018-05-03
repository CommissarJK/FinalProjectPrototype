using UnityEngine;
using System.Collections;

public class HexMap_cintinent : HexMap {
    override public void GenerateMap() {
        // First call the base version to load all the hexes as ocean
        base.GenerateMap();
        int numContinents = 1;
        int continentSpacing = numColumns/numContinents;
        int difficuly = 0;
        bool playerPosSet = false; 

        //Random.InitState(0);
        for (int c = 0; c < numContinents; c++)
        {           
            int numSplats = Random.Range(8, 12);
            for (int i = 0; i < numSplats; i++)
            {
                difficuly++;
                int range = Random.Range(5, 8);
                int y = Random.Range(range, numRows - range);
                int x = Random.Range(0, 20) - y / 2 + (c * continentSpacing);
                ElevateArea(x, y, difficuly, range);
                GetHexAt(x, y).city = true;
                if (!playerPosSet)
                {
                    Vector3 pos = GetHexAt(x, y).PositionFromCamera(Camera.main.transform.position, numColumns, numRows);
                    playerPosSet = true;
                }
            }
        }

        float noiseResolution = 0.01f;
        Vector2 noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        float noiseScale = 2f;
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = GetHexAt(column, row);
                float n = Mathf.PerlinNoise(((float)column / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.x, ((float)row / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.y) - 0.5f;
                h.elevation += n * noiseScale;
            }
        }

        noiseResolution = 0.1f;
        noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        noiseScale = 2f;
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = GetHexAt(column, row);
                float n = Mathf.PerlinNoise(((float)column / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.x, ((float)row / Mathf.Max(numColumns, numRows) / noiseResolution) + noiseOffset.y) - 0.5f;
                h.moisture = n * noiseScale;
            }
        }

        UpdateHexVisuals();
    }

    void ElevateArea(int q, int r, int range, int difficulty, float centerHeight = 0.8f) {
        Hex centerHex = GetHexAt(q, r);

        Hex[] areaHexes = GetHexesInRange(centerHex, range);

        foreach (Hex h in areaHexes) {
            h.elevation = centerHeight * Mathf.Lerp(1f,0.25f,Mathf.Pow( Hex.Distance(centerHex, h)/range,2f));
        }
    }
}
