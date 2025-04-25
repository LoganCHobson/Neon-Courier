using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public Transform player;
    public List<GameObject> tilePrefabs; 
    public int initialTiles = 5;
    public float tileLength = 100f;
    public int maxActiveTiles = 7;
    private List<GameObject> activeTiles = new();
    private float spawnZ = 0;


    public GameObject startTile = null;

    void Start()
    {
        if (startTile != null)
        {
            activeTiles.Add(startTile);
            spawnZ = tileLength; 
        }

        for (int i = 1; i < initialTiles; i++) 
        {
            SpawnTile(i < 2 ? tilePrefabs[0] : GetNextTile());
        }
    }

    void Update()
    {
        if (player.position.z - 20 > spawnZ - initialTiles * tileLength)
        {
            SpawnTile(GetNextTile());
            DeleteTile();
        }
    }

    void SpawnTile(GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, new Vector3(0, 0, spawnZ), Quaternion.identity, transform);
        activeTiles.Add(tile);

        spawnZ += tileLength; 
    }

    void DeleteTile()
    {
        if (activeTiles.Count > 0 && player.position.z - activeTiles[0].transform.position.z > tileLength)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }

    private GameObject GetNextTile()
    {
        CleanUpActiveTiles();

        GameObject nextTile = tilePrefabs[Random.Range(0, tilePrefabs.Count)];
        return nextTile;
    }

    private void CleanUpActiveTiles()
    {
        activeTiles.RemoveAll(tile => tile == null);
    }
}
