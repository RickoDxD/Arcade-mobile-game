using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpController : PowerUpController
{
    public override void Collect(PlayerController player)
    {
        player.ShieldPower++;
        base.Collect(player);
    }
}
