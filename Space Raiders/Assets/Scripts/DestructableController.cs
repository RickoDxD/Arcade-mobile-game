using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Collider2D))]
public class DestructableController : MonoBehaviour
{
    public GameController GameController { get; set; }
    [field: SerializeField]
    public int Points { get; private set; }
    [field: SerializeField]
    public UnityEvent<LaserController, DestructableController>  OnHit { get; private set; }
    [field: SerializeField]
    public UnityEvent<DestructableController> OnDestroyed { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserController asLaser = other.GetComponent<LaserController>();
        if (asLaser != null)
        {
            if (OnHit.GetPersistentEventCount() == 0)
            {
                DefaultDestroy(asLaser);
            }
            else
            {
                OnHit.Invoke(asLaser, this);
            }
        }
    }

    public void DefaultDestroy(LaserController laser)
    {
        GameController.IncrementScore(Points);
        Destroy(laser.gameObject);
        OnDestroyed.Invoke(this);
        Destroy(this.gameObject);
        
    }
}
