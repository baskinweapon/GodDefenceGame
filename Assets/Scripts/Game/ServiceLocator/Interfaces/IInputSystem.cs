using System;
using UnityEngine;

public interface IInputSystem {
    public Action OnFire { get; set; }
    public Action OnRightClick { get; set; }
    Vector2 GetMoveVector();
    public bool IsPressed();
    Vector2 GetMousePosition();
}

