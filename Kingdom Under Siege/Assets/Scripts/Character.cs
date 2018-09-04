using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class prevents assigning of this script on objects by mistake
[RequireComponent(typeof(Rigidbody2D))] //automatically add Rigibody2D component when scrip is attached to object
[RequireComponent(typeof(Animator))] //automatically add animator component when scrip is attached to object
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    protected Animator myAnimator;
    protected Vector2 direction; //protected = similar to private but allows inheritance access
    private Rigidbody2D myRigidbody;
    protected bool isAttacking = false;
    protected Coroutine attackRoutine;
    [SerializeField]
    protected Transform hitBox;
    [SerializeField]
    protected Stat health;
    //allow UIManager to access health for the enemy
    public Stat MyHealth
    {
        get { return health; }
    }
    [SerializeField]
    private float initHealth; //for adjusting health of player
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
    // Use this for initialization
   protected virtual void Start () {
        health.Initialize(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>(); //access Unity Animator Component
		
	}
	
	// Update is called once per frame
    //virtual void update allows it to be overriden by another update
	protected virtual void Update ()
    {
        HandleLayers();
     }

    //always use fixedupdate when using rigidbody
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //delta time = time that has pass after update (good for optimization)
        //transform.Translate(direction * speed * Time.deltaTime);
        //move player
        //.normalized = returns direction as a unit vector (prevents character from moving twice the speed when two buttons are pressed)
        myRigidbody.velocity = direction.normalized * speed;


    }

    public void HandleLayers()
    {
        //if player is moving = do the animation
        if (IsMoving)
        {
         
            ActivateLayer("Walk Layer"); //layer weight use to show specific layers the higher the weight
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
            StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("Attack Layer");
        }
        //else if not moving = set layer weight of layer 1 to 0 (to show idle)
        else
        {
            ActivateLayer("Idle Layer"); //layer weight use to show specific layers the higher the weight
        }
    }
   
    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName),1);
    }
    
    public virtual void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
          
        }
        
    }
    public virtual void TakeDamage(float damage)
    {
        //decrement health
        health.MyCurrentValue -= damage;
        if (health.MyCurrentValue <= 0)
        {
            myAnimator.SetTrigger("die");
            //character dies
        }
        
    }
}
