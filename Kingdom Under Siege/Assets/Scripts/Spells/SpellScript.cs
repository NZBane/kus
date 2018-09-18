using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidBody;
    private int damage;
    
    public Transform MyTarget { get; private set; } //allows targeting of different enemies

    private Transform source;
  

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
     
        // target = GameObject.Find("Target").transform; //heavy on cpu = only use for testing!!!

	}

    public void Initialize(Transform target, int damage, Transform source)
    {
        this.MyTarget = target;
        this.damage = damage;
        this.source = source;
    }
	
    
	
    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            //for every frame, the casted spell will travel to target even if its moving
            Vector2 direction = MyTarget.position - transform.position;

            //Move spell using rigidbody2d
            myRigidBody.velocity = direction.normalized * speed;

            //rotation angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //rotates spell towards targeted enemy
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //when spell collides
    {
        if (collision.tag == "HitBox" && collision.transform == MyTarget) //collison.transform fixes the bug were wrong target is hit //if collides with object wiith hitbox tag and is on the MyTarget position
        {
            Character c = collision.GetComponentInParent<Character>();
            speed = 0;
            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("Impact");
            myRigidBody.velocity = Vector2.zero;
            MyTarget = null; 
        }
    }
}
