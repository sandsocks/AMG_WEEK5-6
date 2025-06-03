using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] tilePrefabs;
    public List<GameObject> spawnedTiles = new List<GameObject>();

    void Start()
    {

    }

    // Update is called once per frame
    public void CreateBoard()
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
            {
                int randomTile = Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(c, 0, r);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = tile.tag + "_" + r + "_" + c;
                spawnedTiles.Add(tile);
            }
    }

    public void DeleteBoard()
    {
        for (int i = 0; i < spawnedTiles.Count; i++)
        {
            if (spawnedTiles[i] != null)
                DestroyImmediate(spawnedTiles[i].gameObject);
        }
        spawnedTiles.Clear();
    }
}
