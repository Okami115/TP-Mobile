using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	private InputConfig inputConfig;
	[SerializeField] public InputConfig inputConfigMobile;
	[SerializeField] public InputConfig inputConfigPC;

	float Giro = 0;
	
	public bool Habilitado = true;
	CarController carController;

	void Start ()
	{
		carController = GetComponent<CarController>();

#if UNITY_ANDROID
		inputConfig = inputConfigMobile;
#else
		inputConfig = inputConfigPC;
#endif
    }


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

	
}
