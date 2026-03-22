using System;
using UnityEngine;

public class InputReader : MonoBehaviour, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    public event Action OnAttacking;
    public event Action OnChangingWeapon;
    public event Action OnUIExit;
    public bool IsHoldAttacking {  get; private set; }

    private PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Player.SetCallbacks(this);
        inputActions.UI.SetCallbacks(this);

        inputActions.Enable();
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        IsHoldAttacking = context.performed;

        if (context.performed == false) { return; }
        OnAttacking?.Invoke();
    }

    public void OnEsc(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed == false) { return; }
        OnUIExit?.Invoke();
    }

    public void OnChangeWeapon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed == false) { return; }
        OnChangingWeapon?.Invoke();
    }
}
