using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : IInputSystem {
    public InputActions playerInput;

    public Action OnFire;
    public Action OnRightClick;
    
    public InputSystem() {
        
        playerInput = new InputActions();
        playerInput.Enable();
        
        playerInput.Player.Fire.performed += Fire;
        playerInput.Player.RightClick.performed += RightClick;
    }

    #region CallbackFunctions

    private void Fire(InputAction.CallbackContext ctx) {
        OnFire?.Invoke();
        // GetClickObject();
    }
    
    private void RightClick(InputAction.CallbackContext ctx) {
        OnRightClick?.Invoke();
    }
    
    #endregion

    public Vector2 GetMoveVector() {
        return playerInput.Player.Move.ReadValue<Vector2>();;
    }

    public Vector2 GetMousePosition() {
        return playerInput.UI.Point.ReadValue<Vector2>();
    }

    public void GetClickObject() {
        Ray ray = Camera.main.ScreenPointToRay(GetMousePosition());
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Debug.Log(hit.collider.gameObject.tag);
        }
    }

    public InputSystem GetInputSystem() {
        return this;
    }

    public void OnDestroy() {
        playerInput.Player.Fire.performed -= Fire;
    }
}
