using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidBody;

    private Transform target;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Target").transform; //heavy on cpu = only use for testing!!!
	}
	
    
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        //for every frame, the casted spell will travel to target even if its moving
        Vector2 direction = target.position - transform.position;
        myRigidBody.velocity = direction.normalized * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
