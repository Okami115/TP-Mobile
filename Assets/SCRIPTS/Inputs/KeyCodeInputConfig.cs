using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/InputConfig", order = 1)]
public class KeyCodeInputConfig : InputConfig
{
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode down;

    public override bool IsPressingDown()
    {
        return Input.GetKey(down);
    }

    public override bool IsPressingLeft()
    {
        return Input.GetKey(left);
    }

    public override bool IsPressingRight()
    {
        return Input.GetKey(right);

    }
}
