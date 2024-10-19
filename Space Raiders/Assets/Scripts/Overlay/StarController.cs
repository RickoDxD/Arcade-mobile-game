using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(OnScreenController))]
public class StarController : SpeedController
{
    public UnityEvent OnDestroyed => GetComponent<OnScreenController>().OnDestroyed;
    public Sprite Sprite { 
        get => GetComponent<SpriteRenderer>().sprite;
        set => GetComponent<SpriteRenderer>().sprite = value;
    }

    public override void Move()
    {
        base.Move();
        transform.position = (Vector2)transform.position + (Speed * Time.deltaTime);
    }
}
