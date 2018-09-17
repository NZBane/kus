using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    private Enemy parent; 
    private void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    ///For Enemy Sight Collider (detect pLayer)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //checks if tag is player then mark it as target
        {
            parent.SetTarget(collision.transform);
        }
    }

    

}
