using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactor : MonoBehaviour
{
    public virtual void OnImpact(PlayerController player)
    {
        Destroy(this.gameObject);
    }
}
