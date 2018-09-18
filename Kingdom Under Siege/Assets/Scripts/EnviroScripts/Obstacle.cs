using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IComparable<Obstacle>
{
    //Obstacles spriterenderer
    public SpriteRenderer MySpriteRenderer { get; set; }
    //Color to use when the obstacle isn't faded
    private Color defaultColor;
    //Color to use when the obstacle is faded
    private Color fadedColor;
    //Compares all obstacles, so we can pick the one with the lowest sort order
    public int CompareTo(Obstacle other)
    {
        if (MySpriteRenderer.sortingOrder > other.MySpriteRenderer.sortingOrder)
        {
            return 1; //if this obstacle has a higher sort order
        }
        else if (MySpriteRenderer.sortingOrder < other.MySpriteRenderer.sortingOrder)
        {
            return -1; //if this obstacle has a lower sort order
        }
        return 0; //if both obstacles have an equal sort order
    }

    // Use this for initialization
    void Start ()
    {
        //reference to the spriterenderer
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        //creates the colors
        defaultColor = MySpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
	}
	//fade out the obstacle
    public void FadeOut()
    {
        MySpriteRenderer.color = fadedColor;
    }
    //fade in the obstacle
    public void FadeIn()
    {
        MySpriteRenderer.color = defaultColor;
    }
}
