using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	[SerializeField] private InputConfig inputConfig;
	public enum TipoInput {AWSD, Arrows, Mobile}
	public TipoInput InputAct;

	private bool isLeftPress;
	private bool isRightPress;
	float Giro = 0;
	
	public bool Habilitado = true;
	CarController carController;
		
	//---------------------------------------------------------//
	
	// Use this for initialization
	void Start ()
	{
		carController = GetComponent<CarController>();
#if UNITY_ANDROID
		InputAct = TipoInput.Mobile;
#else
		InputAct = TipoInput.AWSD;
#endif
    }

	// Update is called once per frame
	void Update () 
	{
		Giro = inputConfig.IsPressingLeft()
					? -1 : inputConfig.IsPressingRight()
							? 1 : 0;
		
        carController.SetGiro(Giro);
	}

    public float GetGiro()
	{
		return Giro;
	}
	public void ClickLeft()
	{
		isLeftPress = true;
    }

	public void ClickRight() 
	{
        isRightPress = true;
    }

	public void releaseLeft() 
	{
        isLeftPress = false;
    }

	public void releaseRight() 
	{ 
		isRightPress = false;
	}
	
}
