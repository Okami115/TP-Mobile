using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	private InputConfig inputConfig;
	[SerializeField] public InputConfig inputConfigMobile;
	[SerializeField] public InputConfig inputConfigPC;

	float Giro = 0;

    bool isLeftPress = false;
    bool isRightPress = false;

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

#if UNITY_ANDROID
        Giro = inputConfig.IsPressingLeft() && GameManager.GameType.MultiPlayer != FSM.instance.GetState() || isLeftPress && GameManager.GameType.MultiPlayer == FSM.instance.GetState()
                    ? -1 : inputConfig.IsPressingRight() && GameManager.GameType.MultiPlayer != FSM.instance.GetState() || isRightPress && GameManager.GameType.MultiPlayer == FSM.instance.GetState()
                            ? 1 : 0;
#else
        Giro = inputConfig.IsPressingLeft()
            ? -1 : inputConfig.IsPressingRight()
                    ? 1 : 0;
#endif

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

    public void ReleaseLeft()
    {
        isLeftPress = false;
    }

    public void ReleaseRight()
    {
        isRightPress = false;
    }

}
