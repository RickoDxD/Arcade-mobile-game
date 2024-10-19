using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnpackerController : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnUnpack { get; private set; }
    [field: SerializeField]
    public Transform Ignore { get; private set; }

    void Start()
    {
        transform.DetachChildren();
        Ignore.parent = transform;
        OnUnpack.Invoke();
        Destroy(gameObject);
    }
}
