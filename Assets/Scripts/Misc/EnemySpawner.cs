using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }
    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform transform = spawnPoints[index];

        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
