using UnityEngine;

public interface IInputProvider
{
    Vector2 GetMovement();
    Vector2 GetRotation();
    bool GetJumping();
}


