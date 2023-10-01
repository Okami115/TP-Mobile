using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MngPts : MonoBehaviour 
{
	public float TiempEmpAnims = 2.5f;
	float Tempo = 0;
	
	public GUISkin GS_Dinero;

	[SerializeField] private TextMeshProUGUI text1;
	[SerializeField] private TextMeshProUGUI text2;

	public Texture2D[] Ganadores;
	public GUISkin GS_Ganador;
	
	public GameObject Fondo;
	
	public float TiempEspReiniciar = 10;

	public float TiempParpadeo = 0.7f;
	float TempoParpadeo = 0;
	bool PrimerImaParp = true;
	
	public bool ActivadoAnims = false;
	
	Visualizacion Viz = new Visualizacion();
	
	//---------------------------------//
	
	// Use this for initialization
	void Start () 
	{		
		SetGanador();
	}
	
	// Update is called once per frame
	void Update () 
	{			
		TiempEspReiniciar -= Time.deltaTime;
		if(TiempEspReiniciar <= 0 )
		{
			SceneManager.LoadScene(0);
		}
		
		
		if(ActivadoAnims)
		{
			TempoParpadeo += Time.deltaTime;
			
			if(TempoParpadeo >= TiempParpadeo)
			{
				TempoParpadeo = 0;
				
				if(PrimerImaParp)
					PrimerImaParp = false;
				else
				{
					TempoParpadeo += 0.1f;
					PrimerImaParp = true;
				}
			}
		}
		
		if(!ActivadoAnims)
		{
			Tempo += Time.deltaTime;
			if(Tempo >= TiempEmpAnims)
			{
				Tempo = 0;
				ActivadoAnims = true;
			}
		}
		
		
	}
	
	void OnGUI()
	{
		if(ActivadoAnims)
		{
			SetDinero();
			SetCartelGanador();
		}
		
		GUI.skin = null;
	}
	
	//---------------------------------//
	
	
	void SetGanador()
	{
		switch(DatosPartida.LadoGanadaor)
		{
		case DatosPartida.Lados.Der:
			
			GS_Ganador.box.normal.background = Ganadores[1];
			
			break;
			
		case DatosPartida.Lados.Izq:
			
			GS_Ganador.box.normal.background = Ganadores[0];
			
			break;
		}
	}
	
	void SetDinero()
	{
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)//izquierda
		{
			if (!PrimerImaParp)
			{
				text1.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
            }
        }
		else
		{
			text1.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
		}
		
		
		
		if(DatosPartida.LadoGanadaor == DatosPartida.Lados.Der)//derecha
		{
			if(!PrimerImaParp)
			{
				text2.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsGanador);
			}
		}
		else
		{
			text2.text = "$" + Viz.PrepararNumeros(DatosPartida.PtsPerdedor);
		}
		
	}
	
	void SetCartelGanador()
	{
		GUI.skin = GS_Ganador;
	}
	
	public void DesaparecerGUI()
	{
		ActivadoAnims = false;
		Tempo = -100;
	}
}
