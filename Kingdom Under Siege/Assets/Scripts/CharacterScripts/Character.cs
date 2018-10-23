using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class prevents assigning of this script on objects by mistake
[RequireComponent(typeof(Rigidbody2D))] //automatically add Rigibody2D component when scrip is attached to object
[RequireComponent(typeof(Animator))] //automatically add animator component when scrip is attached to object
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private CircleCollider2D SwordCollider;


    public Animator MyAnimator { get; set; }

    private Vector2 direction; //protected = similar to private but allows inheritance access
    private Rigidbody2D myRigidbody;

    

    public bool IsAttacking { get; set; }


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

    public Transform MyTarget { get; set; }
    public bool IsMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool IsAlive //boolean for checking if alive
    {
        get
        {
           return health.MyCurrentValue > 0;
        }
    }
    // Use this for initialization
    protected virtual void Start () {
        health.Initialize(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>(); //access Unity Animator Component
		
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
        if(IsAlive)
        {
            //delta time = time that has pass after update (good for optimization)
            //transform.Translate(direction * speed * Time.deltaTime);
            //move player
            //.normalized = returns direction as a unit vector (prevents character from moving twice the speed when two buttons are pressed)
            myRigidbody.velocity = Direction.normalized * Speed;

        }


    }

    public virtual void HandleLayers()
    {
        if(IsAlive)
        {
            //if player is moving = do the animation
            if (IsMoving)
            {

                ActivateLayer("Walk Layer"); //layer weight use to show specific layers the higher the weight
                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);

            }
            else if (IsAttacking)
            {
                ActivateLayer("Attack Layer");
            }
            //else if not moving = set layer weight of layer 1 to 0 (to show idle)
            else
            {
                ActivateLayer("Idle Layer"); //layer weight use to show specific layers the higher the weight
            }
        }
        else
        {
            ActivateLayer("Death Layer"); //actiavte layer if not alive
        }
      
    }
   
    public virtual void ActivateLayer(string layerName)
    {
        for(int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName),1);
    }
    
    public virtual void TakeDamage(float damage, Transform source)
    {
    
        //decrement health
        health.MyCurrentValue -= damage;
        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidbody.velocity = Direction;
           MyAnimator.SetTrigger("die");
            SwordCollider.enabled = false;
            //character dies
        }
        
    }

    public void MeleeAttack()
    {
        //SwordCollider.enabled = !SwordCollider.enabled;
        SwordCollider.enabled = true;

    }

    //run by enemy attack behaviour (fixes bug where enemy damages player even if not playing attack animation (also turned off trigger on sword collider object attached to enemy)
    public void CancelAttack()
    {
        SwordCollider.enabled = false;
    }

}
