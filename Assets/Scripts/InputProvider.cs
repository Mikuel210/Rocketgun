using UnityEngine;

public abstract class InputProvider : MonoBehaviour
{
    public abstract Vector2 GetMovement();
    public abstract Vector2 GetRotation();
    public abstract bool GetJumping();
}


