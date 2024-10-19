using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    [field: SerializeField]
    private float _spawnRate = 3;
    public float SpawnRate
    {
        get => _spawnRate;
        set
        {
            if (value <= 0)
                throw new System.ArgumentException($"SpawnRate must be a positive value but was {value}");
            CancelInvoke(nameof(SpawnNext));
            _spawnRate = value;
            InvokeRepeating(nameof(SpawnNext), SpawnRate, SpawnRate);
        }
    }

    [field: SerializeField]
    private List<IEnemyShip> EnemyQueue { get; set; } = new();
    [field: SerializeField]
    public GameController GameController { get; set; }
    [field: SerializeField]
    public UnityEvent<EnemyShipController> OnSpawn { get; private set; }

    void Start()
    {
        this.InvokeRepeating(nameof(SpawnNext), SpawnRate, SpawnRate);
    }

    private void SpawnNext()
    {
        if (EnemyQueue.Count > 0)
        {
            IEnemyShip enemy = EnemyQueue[0];
            EnemyQueue.RemoveAt(0);
            EnemyShipController newEnemy = EnemyShipController.Spawn(enemy, GameController);
            OnSpawn.Invoke(newEnemy);
        }
    }

    public void EnqueueEnemies(List<IEnemyShip> enemies)
    {
        foreach (IEnemyShip e in enemies)
        {
            EnemyQueue.Add(e);
        }
    }

    public void EnqueEnemy(IEnemyShip enemy) => EnemyQueue.Add(enemy);
}
