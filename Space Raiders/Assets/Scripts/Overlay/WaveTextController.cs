using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTextController : MonoBehaviour
{
    public void ShowWave(int wave)
    {
        GetComponent<TextMeshProUGUI>().text = $"Wave {wave}";
        gameObject.SetActive(true);
        Invoke(nameof(HideOverlay), 3);
    }

    public void HideOverlay() => gameObject.SetActive(false);
}
