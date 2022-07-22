using UnityEngine;

public class PlayerMovementInput : IInputModule
{
    public Vector2 GetInput()
    {
        return new Vector2(
            UnityEngine.Input.GetAxis("Horizontal"),
            UnityEngine.Input.GetAxis("Vertical"));
    }
}
