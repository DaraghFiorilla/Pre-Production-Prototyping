using UnityEngine;

public class CanSpawn : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    public void SpawnAll()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int prefabIndex = Random.Range(0, itemPrefabs.Length);

            Instantiate(
                itemPrefabs[prefabIndex],
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
    }

    void Start()
    {
        SpawnAll();
        InvokeRepeating(nameof(SpawnAll), 1f, 2f);
    }
}