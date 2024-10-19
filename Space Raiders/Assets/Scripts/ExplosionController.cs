using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionController : MonoBehaviour
{
    public void Explode(MonoBehaviour destructable)
    {
        ExplosionController e = Instantiate(this, destructable.transform.position, Quaternion.identity);
        ParticleSystem particles = e.GetComponent<ParticleSystem>();
        e.gameObject.SetActive(true);
        e.Invoke(nameof(DestroySelf), particles.main.duration + 1);
    }

    private void DestroySelf() => Destroy(this.gameObject);
}
