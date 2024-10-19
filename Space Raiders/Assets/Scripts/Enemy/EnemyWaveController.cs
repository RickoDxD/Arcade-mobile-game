using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class EnemyWaveController : MonoBehaviour
{
    
    [field: SerializeField]
    public int EnemyShipsRemaining { get; private set; } = 0;
    [field: SerializeField]
    public int Wave { get; private set; } = 1;

    [field: SerializeField]
    private List<EnemyShip> PossibleEnemies { get; set; }

    [field: SerializeField]
    private EnemySpawner Spawner { get; set; }
    [field: SerializeField]
    public UnityEvent<int> OnWaveCompleted { get; private set; }
    [field: SerializeField]
    public UnityEvent<int> OnWaveStart { get; private set; }

    public void Start()
    {
        Spawner.OnSpawn.AddListener(RegisterEnemy);
    }

    private void RegisterEnemy(EnemyShipController enemy)
    {
        enemy.GetComponent<DestructableController>().OnDestroyed.AddListener(HandleEnemyDestroyed);
    }

    private void HandleEnemyDestroyed(DestructableController destructable)
    {
        EnemyShipsRemaining--;
        if (EnemyShipsRemaining <= 0)
        {
            OnWaveCompleted.Invoke(Wave);
            Wave++;
            Invoke(nameof(StartWave), 3);
        }
    }

    public void Reset()
    {
        Wave = 1;
        Spawner.SpawnRate = 3;
        Invoke(nameof(StartWave), 3);
    }

    private void StartWave()
    {
        List<EnemyShip> enemies = FilterEnemies(Wave);
        
        EnemyShipsRemaining = 3 * Wave;
        for (int i = 0; i < EnemyShipsRemaining; i++)
        {
            int randomIx = Random.Range(0, enemies.Count);
            EnemyShip e = enemies[randomIx];
            Spawner.EnqueEnemy(e);
        }
        OnWaveStart.Invoke(Wave);
        float diff = Mathf.Clamp(Mathf.Log10(10 * Wave), 0, 2.5f) - 1;
        Spawner.SpawnRate = 3 - diff;
    }

    private List<EnemyShip> FilterEnemies(int MaxWave)
    {
        List<EnemyShip> enemies = new ();
        foreach (EnemyShip e in PossibleEnemies)
        {
            if (e.WaveDifficulty <= MaxWave)
            {
                enemies.Add(e);
            }
        }
        return enemies;
    }
    
}
