using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesOverlayController : MonoBehaviour
{

    [field: SerializeField]
    public Image LivesImage { get; private set; }

    [field: SerializeField]
    public RectTransform Container { get; private set; }

    public void Start()
    {
        LivesImage.gameObject.SetActive(false);
    }

    public void RenderLives(int amount)
    {
        foreach (Transform child in Container)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i < amount; i++)
        {
            Image image = Instantiate<Image>(LivesImage, Container);
            image.gameObject.SetActive(true);
        }
    }
}
