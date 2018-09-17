using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        parent.Direction = Vector2.zero; //set direction to null
    }

    public void Update()
    {
        if(parent.MyTarget != null)
        {
            parent.Direction = (parent.MyTarget.transform.position - parent.transform.position).normalized; //change direction to character

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyTarget.position, parent.Speed * Time.deltaTime);

            //varaible which continuesly checks enemy's distance from targeted player
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);
            if (distance <= parent.MyAttackRange) //if close enough to attack = change to attack state
            {
                parent.ChangeState(new AttackState());
            }
        }
        if(!parent.InRange)
        {
            parent.ChangeState(new EvadeState());
        }
        
    }
}