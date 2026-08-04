using UnityEngine;

public class EnemyInputProvider : InputProvider
{
    public override Vector2 GetMovement() => Vector2.zero;
    public override Vector2 GetRotation() => Vector2.zero;
    public override bool GetJumping() => false;
}
