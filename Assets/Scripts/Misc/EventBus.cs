using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action<Enemy> OnEnemySpawn;
    public event Action<Enemy> OnEnemyDeath;
    public event Action<GunData> OnGunUpdate;
    public void RaiseGameStart()
    {
        OnGameStart?.Invoke();
    }

    public void RaiseGameOver()
    {
        OnGameOver?.Invoke();
    }

    public void RaiseEnemySpawn(Enemy enemy)
    {
        OnEnemySpawn?.Invoke(enemy);
    }

    public void RaiseEnemyDeath(Enemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }

    public void RaiseGunUpdate(GunData gunData)
    {
        OnGunUpdate?.Invoke(gunData);
    }
}
