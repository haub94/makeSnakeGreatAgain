using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageScroll : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;

    void Start()
    {

    }

    // Daniel - 03.06.2022 - Bewegt ein Bild in gewuenschte Richtung
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
