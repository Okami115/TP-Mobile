
using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/MobileConfig", order = 1)]
public class MobileInputConfig : InputConfig
{
    public override bool IsPressingLeft()
    {
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2;
    }

    public override bool IsPressingRight()
    {
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x >= Screen.width / 2;

    }
}