using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [field: SerializeField]
    public float FireRate { get; private set; } = .5f;
    [field: SerializeField]
    public float LastFired { get; private set; } = 0;
    [field: SerializeField]
    public GameObject LaserTemplate { get; private set; }

    public void Fire(Transform spawnPosition)
    {
        if (Time.time < LastFired + FireRate) return;
        GameObject laser = Object.Instantiate(LaserTemplate);
        laser.transform.position = spawnPosition.position;
        LastFired = Time.time;
    }

}