using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    GameManager gameManager;
    public MenuState(StateMachine machine, GameManager gameManager) : base(machine)
    {
        this.gameManager = gameManager;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
