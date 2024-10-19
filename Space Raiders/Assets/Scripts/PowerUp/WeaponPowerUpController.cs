using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUpController : PowerUpController
{
    [field: SerializeField]
    public Weapon Weapon { get; private set; }

    public override void Collect(PlayerController player)
    {
        player.Weapon = Weapon;
        base.Collect(player);
    }
}
