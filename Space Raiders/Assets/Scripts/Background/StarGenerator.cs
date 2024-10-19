using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [field: SerializeField]
    public Sprite[] StarSprites { get; private set; }
    [field: SerializeField]
    public StarController Template { get; private set; }
    [field: SerializeField]
    public int MaxStars { get; private set; }
    [field: SerializeField]
    public int Stars { get; private set; }
    [field: SerializeField]
    public float MinY { get; private set; } = 6f;
    [field: SerializeField]
    public float MaxY { get; private set; } = 8f;
    [field: SerializeField]
    public float MinX { get; private set; } = -2.5f;
    [field: SerializeField]
    public float MaxX { get; private set; } = 2.5f;
    [field: SerializeField]
    public float MinSpeed { get; private set; } = 1f;
    [field: SerializeField]
    public float MaxSpeed { get; private set; } = 8f;

    public void Start()
    {
        InvokeRepeating(nameof(SpawnStar), 0, 1);
    }

    public void SpawnStar()
    {
        int spawned = 0;
        while (spawned++ < 15 && Stars < MaxStars)
        {
            StarController sc = Instantiate(Template, this.transform);
            sc.Sprite = StarSprites[Random.Range(0, StarSprites.Length)];
            sc.Speed = new(0, -Random.Range(MinSpeed, MaxSpeed));
            sc.transform.position = new(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
            Stars++;
            sc.OnDestroyed.AddListener(() => Stars--);
        }
    }

}
