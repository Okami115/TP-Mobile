using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    GameManager gameManager;
    public MenuState(StateMachine machine, GameManager gameManager) : base(machine)
    {
        this.gameManager = gameManager;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
