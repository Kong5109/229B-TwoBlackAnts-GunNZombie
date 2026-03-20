using System;
using UnityEngine;

public class InputReader : MonoBehaviour, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    public event Action OnAttacking;
    public event Action OnUIExit;
    [field : SerializeField] public bool IsHoldAttacking {  get; private set; }

    private PlayerInput inputActions;

    private void OnEnable()
    {
        inputActions = new PlayerInput();

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
}
