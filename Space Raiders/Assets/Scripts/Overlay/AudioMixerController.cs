using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [field: SerializeField]
    public AudioMixer Mixer { get; private set; }

    [field: SerializeField]
    private float _volume;    
    public float Volume 
    {
        get => _volume;
        set
        {
            _volume = Mathf.Clamp(value, 0, 1);
            float scaledVolume = (_volume - 1) * 80;
            if(!Mixer.SetFloat("Volume", scaledVolume))
            {
                throw new System.Exception("Could not find exposed parameter 'Volume'.");
            }
        }
    }

}
