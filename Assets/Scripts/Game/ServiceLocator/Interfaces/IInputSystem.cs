using UnityEngine;

public interface IInputSystem {
    Vector2 GetMoveVector();
    Vector2 GetMousePosition();
    InputSystem GetInputSystem();
}

