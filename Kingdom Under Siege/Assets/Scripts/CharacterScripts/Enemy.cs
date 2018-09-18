using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;


    private IState currentState;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    public Vector3 MyStartPosition { get; set; }

    [SerializeField]
    private float initAggroRange;

   public float MyAggroRange { get; set; }
  
    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        //enemy attack range (adjust depending on enemy type)
        MyAttackRange = 1;
        ChangeState(new IdleState()); //run idle state as soon as game starts
    }

    protected override void Update() //override Update method from Character.cs due to to its Update method being virtual void
    {
        if(IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }
            currentState.Update();
         
        }
        base.Update();
    }



    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }
    public override void DeSelect() //hides health bar 
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }


    public override void TakeDamage(float damage, Transform source)
    {
        if(!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);
            OnHealthChanged(health.MyCurrentValue);
        }

    }

 
    //for switching enemy states
    public void ChangeState(IState newState)
    {
        if(currentState != null) 
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    //setting target method
    public void SetTarget(Transform target)
    {
        if(MyTarget== null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange += initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
       this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }
}
