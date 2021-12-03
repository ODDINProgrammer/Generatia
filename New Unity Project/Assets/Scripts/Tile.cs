using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType { Sand, Water, Land }
    public TileType Type;

    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private List<int> chanceToSpawn;
    [SerializeField] private Transform spawnPoint;

    public void TrySpawningObject(int generatedNumber)
    {
        for (int i = 0; i < chanceToSpawn.Count; i++)
        {
            if (generatedNumber <= chanceToSpawn[i])
            {
                Instantiate(objectPrefabs[i], spawnPoint);
                return;
            }
            else
            {
                generatedNumber -= chanceToSpawn[i];
            }
        }
    }

}

