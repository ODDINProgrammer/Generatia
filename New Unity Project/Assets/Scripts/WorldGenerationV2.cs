using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationV2 : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private List<GameObject> tiles;
    [Range(0.01f, 0.5f)][SerializeField] private float generationSpeed;

    private GameObject[,] spawnedTiles;

    private Vector3 tileOffset = new Vector3(3, 0, 3);

    public void GenerateRandom() { StartCoroutine(GenerateRandomMap()); }

    public void ClearMap()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
            StopAllCoroutines();
        }
    }    
    //  GENERATES TILES RANDOMLY
    private IEnumerator GenerateRandomMap()
    {
        Debug.Log("Starting new random map");
        Vector2Int pos = new Vector2Int(0,0);
        for(int y = 0; y < mapSize.y; y++)
        {
            pos.x = 0;
            pos.y -= 3;
            for(int x = 0; x < mapSize.x; x++)
            {
                pos.x += 3;
                yield return new WaitForSeconds(generationSpeed);
                PlaceTile(pos.x, pos.y);
            }
        }

        Debug.Log("Finished generating");
    }

    private void PlaceTile(int x, int y)
    {
        Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(x, 0, y), Quaternion.identity, transform);
    }
}
