using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/MobileConfig", order = 1)]
public class MobileInputConfig : InputConfig
{

    [SerializeField] private int divider;
    [SerializeField] private int dividerIndex;

    public override bool IsPressingDown()
    {
        return true;
    }

    public override bool IsPressingLeft()
    {
        //var screenDivision = Screen.width / divider * dividerIndex;

        if(dividerIndex == 1)
        {
            return Input.GetMouseButton(0) && Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width / 4;
        }
        else
        {
            return Input.GetMouseButton(0) && Input.mousePosition.x > (Screen.width * 2) / 4 && Input.mousePosition.x < (Screen.width * 3)  / 4;

        }
    }

    public override bool IsPressingRight()
    {
        //var screenDivision = Screen.width / divider * dividerIndex;

        if (dividerIndex == 1)
        {
            return Input.GetMouseButton(0) && Input.mousePosition.x > (Screen.width * 1) / 4 && Input.mousePosition.x < (Screen.width * 2) / 4;
        }
        else
        {
            return Input.GetMouseButton(0) && Input.mousePosition.x > (Screen.width * 3) / 4 && Input.mousePosition.x < (Screen.width * 4) / 4;

        }

    }
}
