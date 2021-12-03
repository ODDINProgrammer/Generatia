using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldGenerationV2 : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private List<Tile> landPrefab;
    [SerializeField] private List<GameObject> generatedTiles;
    [Range(0.01f, 2f)] [SerializeField] private float generationSpeed;

    [Header("Accessors")]
    [SerializeField] private Button generateButton;

    private Vector3 tileOffset = new Vector3(3, 0, 3);
    private RaycastHit hit;

    public void GenerateRandom() { StartCoroutine(GenerateRandomMap()); }
    //public void GenerateWeighted() { StartCoroutine(GenerateWeightedMap()); }
    public void ClearMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            StopAllCoroutines();
        }

        generatedTiles.Clear();
    }

    //  GENERATES TILES RANDOMLY
    private IEnumerator GenerateRandomMap()
    {
        Vector2Int pos = new Vector2Int(0, 0);
        for (int y = 0; y < mapSize.y; y++)
        {
            pos.x = 0;
            pos.y -= 3;
            for (int x = 0; x < mapSize.x; x++)
            {
                pos.x += 3;
                yield return new WaitForSeconds(generationSpeed);
                PlaceRandomTile(pos.x, pos.y, Random.Range(0, landPrefab.Count));
            }
        }
        StartCoroutine(GenerateObjects());
    }

    private void PlaceRandomTile(int x, int y, int tileIndex)
    {
        Tile tile = Instantiate(landPrefab[tileIndex], new Vector3(x, 0, y), Quaternion.identity, transform);
        generatedTiles.Add(tile.gameObject);
        //  IF TILE IS NOT WATER CHANGE HEIGHT TO A RANDOM VALUE
        if (tile.Type != Tile.TileType.Water)
            tile.transform.position = new Vector3(x, Random.Range(0f, 1f), y);

    }

    private IEnumerator GenerateObjects()
    {
        foreach(GameObject go in generatedTiles)
        {
            int rand = Random.Range(0, 100 + 1);
            yield return new WaitForSeconds(generationSpeed);
            go.GetComponent<Tile>().TrySpawningObject(rand);
        }
        generateButton.gameObject.SetActive(true);
    }
}
