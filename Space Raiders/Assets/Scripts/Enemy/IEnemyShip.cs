using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEnemyShip
{
    public EnemyShipController Template { get; }
    public List<Transform> WayPoints { get; }
    public Transform SpawnPoint { get; }
}