using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class prevents assigning of this script on objects by mistake
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Animator animator;
    protected Vector2 direction; //protected = similar to private but allows inheritance access

    // Use this for initialization
   protected virtual void Start () {
        animator = GetComponent<Animator>(); //access Unity Animator Component
		
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

        //if player is moving = do the animation
        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        //else if not moving = set layer weight of layer 1 to 0 (to show idle)
        else
        {
            animator.SetLayerWeight(1, 0); //layer weight use to show specific layers the higher the weight
        }
        


    }
    //animator method
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1); //layer weight use to show specific layers the higher the weight
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
