using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetBackgr : MonoBehaviour
{
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap.CompressBounds();        
    }

    // Update is called once per frame
    void Update()
    {
        tilemap.CompressBounds();
    }
}
