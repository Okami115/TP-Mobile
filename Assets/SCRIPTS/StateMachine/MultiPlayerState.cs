using UnityEngine;
using static GameManager;
using UnityEngine.SceneManagement;

public class MultiPlayerState : State
{
    private bool intro = true;

    public MultiPlayerState(StateMachine machine, GameManager gameManager) : base(machine)
    {
        this.gameManager = gameManager;
        conditions.Add(typeof(TimeOut), new TimeOut(this.gameManager.Timmer));
        this.gameManager.finTuto += CambiarACarrera;
    }

    public override void Enter()
    {
        intro = true;
        gameManager.GetGameType = GameType.MultiPlayer;
        if (gameManager.Player1 == null || gameManager.Player2) { return; }
    }

    public override void Exit()
    {
        gameManager.finTuto -= CambiarACarrera;
        FinalizarCarrera();
    }

    public override void Update()
    {
        if (CheckCondition<TimeOut>())
        {
            //machine.ChangeState<MenuState>();
        }

        if (gameManager.Player1 == null || gameManager.Player2 == null) { return; }

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

        switch (gameManager.EstAct)
        {
            case EstadoJuego.Calibrando:

#if UNITY_ANDROID
                gameManager.Player1.Seleccionado = true;
                gameManager.Player2.Seleccionado = true;
#endif
#if UNITY_STANDALONE

                if (Input.GetKeyDown(KeyCode.W))
                {
                    gameManager.Player1.Seleccionado = true;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    gameManager.Player2.Seleccionado = true;
                }

#endif
                if (intro)
                {
                    this.gameManager.finTuto += CambiarACarrera;
                    gameManager.GetGameType = GameType.MultiPlayer;
                    IniciarTutorial();
                    intro = false;
                }

                break;

            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
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
                    //baja el tiempo del juego
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

    public void IniciarTutorial()
    {


        for (int i = 0; i < gameManager.ObjsCalibracion1.Length; i++)
        {
            gameManager.ObjsCalibracion1[i].SetActive(true);
        }

        for (int i = 0; i < gameManager.ObjsCalibracion2.Length; i++)
        {
            gameManager.ObjsCalibracion2[i].SetActive(true);
        }

        for (int i = 0; i < gameManager.ObjsCarrera.Length; i++)
        {
            gameManager.ObjsCarrera[i].SetActive(false);
        }

        gameManager.Player1.CambiarATutorial();
        gameManager.Player2.CambiarATutorial();

        gameManager.TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        gameManager.ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera()
    {
        gameManager.Player1.GetComponent<Frenado>().RestaurarVel();
        gameManager.Player1.GetComponent<ControlDireccion>().Habilitado = true;

        gameManager.Player2.GetComponent<Frenado>().RestaurarVel();
        gameManager.Player2.GetComponent<ControlDireccion>().Habilitado = true;

    }

    void FinalizarCarrera()
    {
        gameManager.EstAct = EstadoJuego.Finalizado;

        gameManager.Timmer = 0;

        if (gameManager.Player1.Dinero > gameManager.Player2.Dinero)
        {
            //lado que gano
            if (gameManager.Player1.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
            //puntajes
            DatosPartida.PtsGanador = gameManager.Player1.Dinero;
            DatosPartida.PtsPerdedor = gameManager.Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (gameManager.Player2.LadoActual == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = gameManager.Player2.Dinero;
            DatosPartida.PtsPerdedor = gameManager.Player1.Dinero;
        }

        gameManager.Player1.GetComponent<Frenado>().Frenar();
        gameManager.Player2.GetComponent<Frenado>().Frenar();

        gameManager.Player1.ContrDesc.FinDelJuego();
        gameManager.Player2.ContrDesc.FinDelJuego();
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

        for (int i = 0; i < gameManager.ObjsCalibracion1.Length; i++)
        {
            gameManager.ObjsCalibracion1[i].SetActive(false);
        }

        gameManager.Player2.FinCalibrado = true;

        for (int i = 0; i < gameManager.ObjsCalibracion2.Length; i++)
        {
            gameManager.ObjsCalibracion2[i].SetActive(false);
        }


        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (gameManager.Player1.LadoActual == Visualizacion.Lado.Izq)
        {
            gameManager.Player1.gameObject.transform.position = gameManager.PosCamionesCarrera[0];

            gameManager.Player2.gameObject.transform.position = gameManager.PosCamionesCarrera[1];
        }
        else
        {
            gameManager.Player1.gameObject.transform.position = gameManager.PosCamionesCarrera[1];

            gameManager.Player2.gameObject.transform.position = gameManager.PosCamionesCarrera[0];

        }

        gameManager.Player1.transform.forward = Vector3.forward;
        gameManager.Player1.GetComponent<Frenado>().Frenar();
        gameManager.Player1.CambiarAConduccion();

        gameManager.Player1.GetComponent<Frenado>().RestaurarVel();

        gameManager.Player1.GetComponent<ControlDireccion>().Habilitado = false;

        gameManager.Player1.transform.forward = Vector3.forward;

        gameManager.Player2.transform.forward = Vector3.forward;
        gameManager.Player2.GetComponent<Frenado>().Frenar();
        gameManager.Player2.CambiarAConduccion();

        gameManager.Player2.GetComponent<Frenado>().RestaurarVel();

        gameManager.Player2.GetComponent<ControlDireccion>().Habilitado = false;

        gameManager.Player2.transform.forward = Vector3.forward;


        gameManager.TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        gameManager.ConteoInicio.gameObject.SetActive(false);
    }
}
