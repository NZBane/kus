using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class prevents assigning of this script on objects by mistake
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    protected Vector2 direction; //protected = similar to private but allows inheritance access

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    //virtual void update allows it to be overriden by another update
	protected virtual void Update ()
    {
        Move();
     }

    public void Move()
    {
        //delta time = time that has pass after update (good for optimization)
        transform.Translate(direction * speed * Time.deltaTime);

    }
}
