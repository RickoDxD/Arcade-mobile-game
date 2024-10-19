using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    [field: SerializeField]
    public GameObject ToToggle { get; private set; }

    public void Toggle() => ToToggle.SetActive(!ToToggle.activeSelf);
}
