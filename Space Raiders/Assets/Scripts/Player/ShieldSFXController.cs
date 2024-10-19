using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSFXController : SoundEffectController
{

    [field: SerializeField]
    public AudioClip PowerDownSound { get; private set; }
    private int _previousShield = 0;

    public void CheckPlaySound(PlayerController player)
    {
        if (player.ShieldPower < _previousShield)
        {
            this.PlaySound();
            if (player.ShieldPower == 0)
            {
                this.PlaySound(PowerDownSound);
            }
        }
        _previousShield = player.ShieldPower;
    }
}