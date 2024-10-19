using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControllerObserver
{
    public void OnChange(PlayerController player);
}
