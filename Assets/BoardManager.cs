using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;    //Array of tile prefabs (e.g. Dire, Desert)
    public GameObject housePrefab;      //Prefab for the houses the player places
    public GameObject treePrefab;      //Prefab for trees to be planted
    public Text score;                  //Reference for UI Text for displaying score
    GameObject[] tiles;                 //Internal array to hold the instantiated tiles
    public List<GameObject> spawnedTiles = new List<GameObject>();
    long dirtBB = 0;                    //Bitboard for dirt
    long treeBB = 0;                    //Bitboard for our tree

    void Start()
    {

    }

    // Update is called once per frame
    public void CreateBoard()
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(c, 0, r);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = tile.tag + "_" + r + "_" + c;
                if (tile.tag == "Dirt")     //if the tag is dirt
                {
                    dirtBB = SetCellState(dirtBB, r, c);
                    PrintBB("Dirt", dirtBB);
                }
                spawnedTiles.Add(tile);
            }
        Debug.Log("Dirt Cells = ");         //print how many dirt we have
        InvokeRepeating("PlantTree", 0.25f, 0.25f);
    }

    void PrintBB(string name, long BB)
    {
        Debug.Log(name + ": " + Convert.ToString(BB, 2).PadLeft(64, '0'));
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

    long SetCellState(long Bitboard, int row, int col)
    {
        long newBit = 1L << (row * 8 + col);
        return (Bitboard |= newBit);
    }

    bool GetCellState(long Bitboard, int row, int col)
    {
        long mask = 1L << (row * 8 + col);
        return ((Bitboard & mask) != 0);
    }

    int CellCount(long bitboard)
    {
        int count = 1;
        long bb = bitboard;
        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }
        return count;
    }

    void PlantTree()    //plants a tree on dirt
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(dirtBB, rr, rc))
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            tree.transform.localPosition = Vector3.zero;
            treeBB = SetCellState(treeBB, rr, rc);
        }
    }
}
