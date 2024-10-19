using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMeterController : MonoBehaviour
{ 
    private PlayerController _attached;
    public void AttachToPlayer(PlayerController player) {
        if (_attached != null)
        {
            _attached.OnChange.RemoveListener(Render);
        }
        _attached = player;
        player.OnChange.AddListener(Render);
        Render(player);
    }

    public void Render(PlayerController player)
    {
        
        int nodes = 0;
        foreach(Transform child in this.transform)
        {
            child.gameObject.SetActive(player.ShieldPower > nodes);            
            nodes++;
        }
    }
}
