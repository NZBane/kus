using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IdleState : IState
{
    private Enemy parent;
    public void Enter(Enemy parent)
    {
        this.parent = parent;
 
        this.parent.Reset();
    }

    public void Exit()
    {
      
    }

    public void Update()
    {
        //cahnge to follow state if player is close
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}
