using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour {

    private SpriteRenderer parentRenderer;

    private List<Obstacle> obstacles = new List<Obstacle>();

	// Use this for initialization
	void Start ()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeOut();
            if(obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
            }

            obstacles.Add(o);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            //Creates a referance to the obstacle
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeIn();
            //Removes obstacle from list
            obstacles.Remove(o);
            if(obstacles.Count == 0)
            {
                parentRenderer.sortingOrder = 200;
            }
            else
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }
        }

    }


}
