using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : Singleton<InputSystem> {
    private DefaultInputActions playerInput;

    public static Action OnFire;
    
    protected override void Awake() {
        base.Awake();
        
        playerInput = new DefaultInputActions();
        playerInput.Enable();
        
        playerInput.Player.Fire.performed += Fire;
    }

    #region CallbackFunctions

    private void Fire(InputAction.CallbackContext ctx) {
        OnFire?.Invoke();
    }
    
#endregion

    public Vector2 GetMoveVector() {
        return playerInput.Player.Move.ReadValue<Vector2>();;
    }

    public Vector2 GetMousePosition() {
        return playerInput.UI.Point.ReadValue<Vector2>();
    }
    

    private void OnDestroy() {
        playerInput.Player.Fire.performed -= Fire;
    }
}
