using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour {

    //reference the players sprite renderer
    private SpriteRenderer parentRenderer;
    //lists all obstacles that the player collides with
    private List<Obstacle> obstacles = new List<Obstacle>();

	// Use this for initialization
	void Start ()
    {
        //creates the reference to the players sprite renderer
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
	}
	//when the player hits an obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle") //if we hit an obstacle
        {
            //create a reference the obstacle
            Obstacle o = collision.GetComponent<Obstacle>();
            //fades out the tree
            o.FadeOut();
            //if we are not colliding with anything else or we are colliding with something with a lesser sort order
            if(obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < parentRenderer.sortingOrder)
            {
                //change the sort order to be behind what we hit
                parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
            }
            //adds the obstacle to the list, so we can keep track of it
            obstacles.Add(o);
        }
    }
    //when we have stopped coliding with an obstacle
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle") //if we have stopped
        {
            //Creates a referance to the obstacle
            Obstacle o = collision.GetComponent<Obstacle>();
            //fades in the tree so that we cant see through it
            o.FadeIn();
            //Removes obstacle from list
            obstacles.Remove(o);
            //if we dont have any other obstacles
            if(obstacles.Count == 0)
            {
                parentRenderer.sortingOrder = 200;
            }
            else //we have other obstacles and we need to change the sort order
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }
        }
    }
}
