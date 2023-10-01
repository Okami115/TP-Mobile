using UnityEngine;
using static GameManager;
using UnityEngine.SceneManagement;

public class SinglePlayerState : State
{

    private bool intro = true;

    public SinglePlayerState(StateMachine machine, GameManager gameManager) : base(machine)
    {
        this.gameManager = gameManager;
        conditions.Add(typeof(TimeOut), new TimeOut(this.gameManager.Timmer));

    }

    public override void Enter()
    {
        intro = true;   
        gameManager.GetGameType = GameType.SinglePlayer; 
        if (gameManager.Player1 == null) { return; }

    }

    public override void Update()
    {
        if(gameManager.Player1 == null) { return; }


        switch (gameManager.EstAct)
        {
            case EstadoJuego.Calibrando:

#if UNITY_ANDROID
                gameManager.Player1.Seleccionado = true;
                if(intro)
                {
                    this.gameManager.finTuto += CambiarACarrera;
                    IniciarTutorial();
                    intro = false;
                }
#endif

#if UNITY_STANDALONE
                if (Input.GetKeyDown(KeyCode.W))
                {
                    gameManager.Player1.Seleccionado = true;
                }
#endif
                if (intro)
                {
                    this.gameManager.finTuto += CambiarACarrera;
                    IniciarTutorial();
                    intro = false;
                }

                break;

            case EstadoJuego.Jugando:

                if (Input.GetKey(KeyCode.Alpha9))
                {
                    gameManager.Timmer = 0;
                }

                if (gameManager.Timmer <= 0)
                {
                    FinalizarCarrera();
                }

                if (gameManager.ConteoRedresivo)
                {
                    gameManager.ConteoParaInicion -= T.GetDT();
                    if (gameManager.ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        gameManager.ConteoRedresivo = false;
                    }
                }
                else
                {
                    gameManager.Timmer -= T.GetDT();
                }

                if (gameManager.ConteoRedresivo)
                {
                    if (gameManager.ConteoParaInicion > 1)
                    {
                        gameManager.ConteoInicio.text = gameManager.ConteoParaInicion.ToString("0");
                    }
                    else
                    {
                        gameManager.ConteoInicio.text = "GO";
                    }
                }

                gameManager.ConteoInicio.gameObject.SetActive(gameManager.ConteoRedresivo);

                gameManager.TiempoDeJuegoText.text = gameManager.Timmer.ToString("00");

                break;

            case EstadoJuego.Finalizado:

                gameManager.TiempEspMuestraPts -= Time.deltaTime;
                if (gameManager.TiempEspMuestraPts <= 0)
                {
                    machine.ChangeState<MenuState>();
                    SceneManager.LoadScene(3);
                }

                break;
        }

        gameManager.TiempoDeJuegoText.transform.parent.gameObject.SetActive(gameManager.EstAct == EstadoJuego.Jugando && !gameManager.ConteoRedresivo);
    }

    public override void Exit()
    {
        this.gameManager.finTuto -= CambiarACarrera;
        FinalizarCarrera();
    }

    public void IniciarTutorial()
    {


        for (int i = 0; i < gameManager.ObjsCalibracion1.Length; i++)
        {
            gameManager.ObjsCalibracion1[i].SetActive(true);
        }


        for (int i = 0; i < gameManager.ObjsCarrera.Length; i++)
        {
            gameManager.ObjsCarrera[i].SetActive(false);
        }

        gameManager.Player1.CambiarATutorial();

        gameManager.TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        gameManager.ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera()
    {
        gameManager.Player1.GetComponent<Frenado>().RestaurarVel();
        gameManager.Player1.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        gameManager.EstAct = EstadoJuego.Finalizado;

        gameManager.Timmer = 0;


        DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

        DatosPartida.PtsGanador = gameManager.Player1.Dinero;

        gameManager.Player1.GetComponent<Frenado>().Frenar();

        gameManager.Player1.ContrDesc.FinDelJuego();
    }

    void CambiarACarrera()
    {

        gameManager.EstAct = EstadoJuego.Jugando;

        for (int i = 0; i < gameManager.ObjsCarrera.Length; i++)
        {
            gameManager.ObjsCarrera[i].SetActive(true);
        }

        //desactivacion de la calibracion
        gameManager.Player1.FinCalibrado = true;


        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (gameManager.Player1.LadoActual == Visualizacion.Lado.Izq)
        {
            gameManager.Player1.gameObject.transform.position = gameManager.PosCamionesCarrera[0];
        }
        else
        {
            gameManager.Player1.gameObject.transform.position = gameManager.PosCamionesCarrera[1];
        }

        gameManager.Player1.transform.forward = Vector3.forward;
        gameManager.Player1.GetComponent<Frenado>().Frenar();
        gameManager.Player1.CambiarAConduccion();

        gameManager.Player1.GetComponent<Frenado>().RestaurarVel();

        gameManager.Player1.GetComponent<ControlDireccion>().Habilitado = false;

        gameManager.Player1.transform.forward = Vector3.forward;

        gameManager.TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        gameManager.ConteoInicio.gameObject.SetActive(false);
    }


}
