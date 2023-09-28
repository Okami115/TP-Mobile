using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public float Timmer { get => timmer; set => timmer = value; }
    [SerializeField] private float timmer = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public enum GameType { SinglePlayer, MultiPlayer }
    public enum difficulti { easy, normal, hard }

    private StateMachine fsm;

    public EstadoJuego EstAct { get => estAct; set => estAct = value; }
    private EstadoJuego estAct = EstadoJuego.Calibrando;

    public GameType GetGameType { get => gameType; set => gameType = value; }
    private GameType gameType;

    public Player Player1;
    public Player Player2;


    public bool ConteoRedresivo { get => conteoRedresivo; set => conteoRedresivo = value; }
    private bool conteoRedresivo = true;

    public float ConteoParaInicion { get => conteoParaInicion; set => conteoParaInicion = value; }
    [SerializeField] private float conteoParaInicion = 3;

    public Text ConteoInicio { get => conteoInicio; set => conteoInicio = value; }
    [SerializeField] private Text conteoInicio;

    public Text TiempoDeJuegoText { get => tiempoDeJuegoText; set => tiempoDeJuegoText = value; }
    [SerializeField] private Text tiempoDeJuegoText;

    public float TiempEspMuestraPts { get => tiempEspMuestraPts; set => tiempEspMuestraPts = value; }

    [SerializeField] private float tiempEspMuestraPts = 3;

    [SerializeField] public Vector3[] PosCamionesCarrera = new Vector3[2];
    [SerializeField] public GameObject[] ObjsCalibracion1;
    [SerializeField] public GameObject[] ObjsCalibracion2;
    [SerializeField] public GameObject[] ObjsCarrera;

    public Action finTuto;


    private void Start()
    {
        fsm = new StateMachine();
        fsm.AddState<MenuState>(new MenuState(fsm, this));
        fsm.AddState<SinglePlayerState>(new SinglePlayerState(fsm, this));
        fsm.AddState<MultiPlayerState>(new MultiPlayerState(fsm, this));

        fsm.ChangeState<MultiPlayerState>();

    }

    private void Update() 
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Alpha0)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

        fsm.Update();

        tiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !conteoRedresivo);
    }

    public void FinCalibracion(int playerID)
    {
        if (playerID == 0)
        {
            Player1.FinTuto = true;
        }

        if (playerID == 1)
        {
            Player2.FinTuto = true;
        }

        if (Player1.FinTuto && gameType == GameType.SinglePlayer || Player1.FinTuto && Player2.FinTuto)
            finTuto?.Invoke();
    }

}
public class TimeOut : Condition
    {
        private float time;

        public TimeOut(float time)
        {
            this.time = time;
        }

        public override bool Check()
        {
            return time <= 0;
        }
    }
