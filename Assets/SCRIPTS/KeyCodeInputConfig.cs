using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/InputConfig", order = 1)]
public class KeyCodeInputConfig : InputConfig
{
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    public override bool IsPressingLeft()
    {
        return Input.GetKey(left);
    }

    public override bool IsPressingRight()
    {
        return Input.GetKey(right);

    }
}
