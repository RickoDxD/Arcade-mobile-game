using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PowerUpController : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent<PowerUpController> OnCollect { get; private set; }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        Collect(player);
    }

    public virtual void Collect(PlayerController player)
    {
        OnCollect.Invoke(this);
        Destroy(this.gameObject);
    }
}
