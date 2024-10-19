using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    public void Play(GameController controller) => GetComponent<AudioSource>().Play();
    public void Stop(GameController controller) => GetComponent<AudioSource>().Stop();
}
