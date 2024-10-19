using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyShip : IEnemyShip
{
    [field: SerializeField]
    public EnemyShipController Template { get; private set; }
    [field: SerializeField]
    public List<Transform> WayPoints { get; private set; }
    [field: SerializeField]
    public Transform SpawnPoint { get; private set; }
    [field: SerializeField]
    public int WaveDifficulty { get; private set; } = 1;

    public EnemyShip(EnemyShipController template, List<Transform> waypoints, Transform spawn)
    {
        Template = template;
        WayPoints = waypoints;
        SpawnPoint = spawn;
    }
}