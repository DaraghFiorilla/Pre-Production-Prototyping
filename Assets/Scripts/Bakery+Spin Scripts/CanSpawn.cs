using UnityEngine;

public class CanSpawn : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    
    [SerializeField] public float repeatTime = 2f;

    public GameObject spawnController;

    public void SpawnAll()
    {
        if (spawnController != null && !spawnController.activeInHierarchy)
        return;

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

    void Awake()
    {
        SpawnAll();
        InvokeRepeating(nameof(SpawnAll), repeatTime, repeatTime);
    }
}