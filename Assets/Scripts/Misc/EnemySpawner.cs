using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private EventBus eventBus;
    [field: SerializeField] public int EnemyKillToWin { get; private set; } = 7;
    [field: SerializeField] public int KillCounter { get; private set; }
    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();
    }

    private void OnEnable()
    {
        eventBus.OnEnemyDeath += EnemyDeath;
        //eventBus.OnGameOver += EndGame;
    }

    private void OnDisable()
    {
        eventBus.OnEnemyDeath -= EnemyDeath;
        //eventBus.OnGameOver -= EndGame;
    }
    private void Start()
    {
        KillCounter = 0;

        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }
    private void SpawnEnemy()
    {
        Vector3 spawnPos;
        spawnPos.x = Random.Range(minPos.position.x, maxPos.position.x);
        spawnPos.y = Random.Range(minPos.position.y, maxPos.position.y);
        spawnPos.z = Random.Range(minPos.position.z, maxPos.position.z);

        Instantiate(enemyPrefab, transform.position + spawnPos, transform.rotation);
    }

    private void EnemyDeath(Enemy enemy)
    {
        KillCounter++;
        if (KillCounter >= EnemyKillToWin)
        {
            eventBus.RaiseGameOver();
        }
    }

    private void EndGame()
    {
        Invoke("GotoNextScene", 1f);
    }

    private void GotoNextScene()
    {
        LevelManager levelManager = FindAnyObjectByType<LevelManager>();
        levelManager.LoadScene();
    }
}
