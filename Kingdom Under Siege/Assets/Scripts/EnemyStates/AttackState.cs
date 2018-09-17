using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;
    private float attackCooldown = 3; //for attack itnerval time
    private float extraRange = 0.1f; //extra enemy distance advantage (not really improtant)

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
    }

    public void Update()
    {

        if(parent.MyAttackTime >= attackCooldown && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
        }

        if(parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);
        if(distance >= parent.MyAttackRange+extraRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState()); //if enemy loses target = change to idle
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;
        parent.MyAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //wait for seconds before stopping attack
        parent.IsAttacking = false;
    }
}
