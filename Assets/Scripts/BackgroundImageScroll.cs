/******************************************************************************
Name:           BackgroundImageScroll
Description:    The script manages the effect of the scolling backgroundimage
                on the startscreen.
Author(s):      Daniel Rittrich
Date:           2022-06-03
Version:        V1.0
TODO:           - 
******************************************************************************/
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
