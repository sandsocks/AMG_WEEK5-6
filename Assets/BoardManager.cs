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
    public GameObject fishPrefab;
    public GameObject swordPrefab;
    public GameObject camelPrefab;
    public GameObject flowerPrefab;
    public GameObject bugPrefab;
    public Text score;                  //Reference for UI Text for displaying score
    GameObject[] tiles;                 //Internal array to hold the instantiated tiles
    public List<GameObject> spawnedTiles = new List<GameObject>();
    long dirtBB = 0;                    //tree
    long desertBB = 0;                  //camel
    long grainBB = 0;                   //
    long pastureBB = 0;                 //flower
    long waterBB = 0;                   //fish
    long woodsBB = 0;                   //bug
    long rockBB = 0;                    //sword

    long treeBB = 0;                    //dirt
    long camelBB = 0;                   //desert
    long swordBB = 0;                   //rock
    long flowerBB = 0;                  //pasture
    long fishBB = 0;                    //water
    long bugBB = 0;                     //woods
    long houseBB = 0;                   //grain

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
                else if (tile.tag == "Desert")
                {
                    desertBB = SetCellState(desertBB, r, c);
                    PrintBB("Desert", desertBB);
                }
                else if (tile.tag == "Water")
                {
                    waterBB = SetCellState(waterBB, r, c);
                    PrintBB("Water", waterBB);
                }
                else if (tile.tag == "Pasture")
                {
                    pastureBB = SetCellState(pastureBB, r, c);
                    PrintBB("Pasture", pastureBB);
                }
                else if (tile.tag == "Woods")
                {
                    woodsBB = SetCellState(woodsBB, r, c);
                    PrintBB("Woods", woodsBB);
                }
                else if (tile.tag == "Rock")
                {
                    rockBB = SetCellState(rockBB, r, c);
                    PrintBB("Rock", rockBB);
                }
                else if (tile.tag == "Grain")
                {
                    grainBB = SetCellState(grainBB, r, c);
                    PrintBB("Grain", grainBB);
                }

                spawnedTiles.Add(tile);
            }
        Debug.Log("Dirt Cells = ");         //print how many dirt we have
        InvokeRepeating("PlantTree", 0.25f, 0.25f);
        InvokeRepeating("PlantCamel", 0.25f, 0.25f);
        InvokeRepeating("PlantFish", 0.25f, 0.25f);
        InvokeRepeating("PlantFlower", 0.25f, 0.25f);
        InvokeRepeating("PlantBug", 0.25f, 0.25f);
        InvokeRepeating("PlantSword", 0.25f, 0.25f);
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

    void PlantCamel()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(desertBB, rr, rc))
        {
            GameObject camel = Instantiate(camelPrefab);
            camel.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            camel.transform.localPosition = Vector3.zero;
            camelBB = SetCellState(treeBB, rr, rc);
        }
    }

    void PlantSword()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(rockBB, rr, rc))
        {
            GameObject sword = Instantiate(swordPrefab);
            sword.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            sword.transform.localPosition = Vector3.zero;
            swordBB = SetCellState(swordBB, rr, rc);
        }
    }

    void PlantFlower()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(pastureBB, rr, rc))
        {
            GameObject flower = Instantiate(flowerPrefab);
            flower.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            flower.transform.localPosition = Vector3.zero;
            flowerBB = SetCellState(flowerBB, rr, rc);
        }
    }

    void PlantFish()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(waterBB, rr, rc))
        {
            GameObject fish = Instantiate(fishPrefab);
            fish.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            fish.transform.localPosition = Vector3.zero;
            fishBB = SetCellState(fishBB, rr, rc);
        }
    }

    void PlantBug()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(woodsBB, rr, rc))
        {
            GameObject bug = Instantiate(bugPrefab);
            bug.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            bug.transform.localPosition = Vector3.zero;
            bugBB = SetCellState(bugBB, rr, rc);
        }
    }
    
    void PlantHouse()
    {
        int rr = UnityEngine.Random.Range(0, 8);            //random row
        int rc = UnityEngine.Random.Range(0, 8);            //random row
        if (GetCellState(grainBB, rr, rc))
        {
            GameObject house = Instantiate(housePrefab);
            house.transform.parent = spawnedTiles[rr * 8 + rc].transform;
            house.transform.localPosition = Vector3.zero;
            houseBB = SetCellState(houseBB, rr, rc);
        }
    }
}
