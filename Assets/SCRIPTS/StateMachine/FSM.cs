using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class  FSM : MonoBehaviour
{
    public static FSM instance {  get; private set; }

    [SerializeField] private StateMachine stateMachine;

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if (instance == null)
        {
            instance = FindObjectOfType<FSM>();

            if (instance == null)
            {
                GameObject singletonObject = new GameObject("FSM");
                instance = singletonObject.AddComponent<FSM>();
            }

            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this);
        }

        Debug.Log("FSM Awaked");
    }

    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.AddState<MenuState>(new MenuState(stateMachine));
        stateMachine.AddState<SinglePlayerState>(new SinglePlayerState(stateMachine, gameManager));
        stateMachine.AddState<MultiPlayerState>(new MultiPlayerState(stateMachine, gameManager));

        stateMachine.ChangeState<MenuState>();
        Debug.Log("FSM Started");
    }

    private void Update()
    {

        if(gameManager == null)
        {
            Debug.Log("Change GM");
            gameManager = FindAnyObjectByType<GameManager>();
            stateMachine.SetGameManager(gameManager);
        }

        stateMachine.Update();
    }

    public void ChangeScene(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.SinglePlayer:
                stateMachine.ChangeState<SinglePlayerState>();
                break;
            case GameType.MultiPlayer:
                stateMachine.ChangeState<MultiPlayerState>();
                break;
            default:
                break;

        }
    }

}

public class StateMachine
{
    private State currentState;
    private Dictionary<Type, State> states;


    public StateMachine()
    {
        states = new Dictionary<Type, State>();
    }

    public void AddState<T>(State state) where T : State
    {
        states.Add(typeof(T), state);
    }

    public void Update()
    {
        currentState.Update();
    }

    public void ChangeState<T>() where T : State
    {
        currentState?.Exit();

        Type nextStateType = typeof(T);

        if (states.TryGetValue(nextStateType, out var state)) 
        { 
            currentState = state;
            currentState.Enter();
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        currentState.gameManager = gameManager;
    }
}

public abstract class State
{
    protected StateMachine machine;
    protected Dictionary<Type, Condition> conditions = new Dictionary<Type, Condition> ();
    public GameManager gameManager;

    protected State(StateMachine machine)
    {
        this.machine = machine;
    }

    public bool CheckCondition<T>() where T : Condition
    {
        bool ret = false;
        Type conditionType = typeof(T);

        if (conditions.TryGetValue(conditionType, out var condition))
            ret = condition.Check();
        
        return ret;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class Condition
{
    public virtual bool Check() => true;
}
