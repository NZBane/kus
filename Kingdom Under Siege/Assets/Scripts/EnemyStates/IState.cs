using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //interface defines function  that interfaces needs to have (a template for itnerface)
{
   
    void Enter(Enemy parent);

    void Update();

    void Exit();


	
}
