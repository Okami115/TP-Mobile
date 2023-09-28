using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	public enum TipoInput {AWSD, Arrows, Mobile}
	public TipoInput InputAct = TipoInput.AWSD;

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(InputAct)
		{
            case TipoInput.AWSD:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.A)) {
						Giro = -1;
                    }
                    else if (Input.GetKey(KeyCode.D)) {
						Giro = 1;
                    }
                    else {
						Giro = 0;
					}
                }
                break;

            case TipoInput.Arrows:
                if (Habilitado) {
                    if (Input.GetKey(KeyCode.LeftArrow)) {
						Giro = -1;
					}
                    else if (Input.GetKey(KeyCode.RightArrow)) {
						Giro = 1;
					}
                    else {
						Giro = 0;
					}
                }
                break;

			case TipoInput.Mobile:
				if (Habilitado) 
				{ 
					if(isLeftPress)
						Giro = -1;
					else if (isRightPress)
						Giro = 1;
					else 
						Giro = 0;
					
				}
				break;
        }

		
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
