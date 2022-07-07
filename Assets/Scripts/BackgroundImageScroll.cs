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

public class BackgroundImageScroll : MonoBehaviour {
    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;

    /*
     * Author: Daniel Rittrich 
     * Description: Moves an image permanently to a new coordinate and creates a fluid movingeffect. 
     * Parameter: X and Y parameter must be set in Unity.
     * Return: -
    */
    void Update() {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
