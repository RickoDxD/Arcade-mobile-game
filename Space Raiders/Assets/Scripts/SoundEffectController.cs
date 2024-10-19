using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour
{

    [field: SerializeField]
    public AudioClip Clip { get; private set; }

    public void PlaySound() => PlaySound(Clip);

    public void PlaySound(AudioClip toPlay)
    {
        SoundEffectController clone = Instantiate(this);
        AudioSource source = clone.GetComponent<AudioSource>();
        source.clip = toPlay;
        source.playOnAwake = true;
        clone.gameObject.SetActive(true);
        clone.Invoke(nameof(DestroySelf), source.clip.length + 1);        
    }

    private void DestroySelf() => Destroy(this.gameObject);

}
