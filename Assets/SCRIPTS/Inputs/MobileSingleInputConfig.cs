using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/MobileSingleConfig", order = 1)]
public class MobileSingleInputConfig : InputConfig
{
    public override bool IsPressingDown()
    {
        return true;
    }

    public override bool IsPressingLeft()
    {

        return Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2;
    }

    public override bool IsPressingRight()
    {

        return Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2;

    }
}
