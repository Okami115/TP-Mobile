using UnityEngine;

public abstract class InputConfig : ScriptableObject
{
    public abstract bool IsPressingLeft();  
    public abstract bool IsPressingRight();  
    public abstract bool IsPressingDown();  
}
