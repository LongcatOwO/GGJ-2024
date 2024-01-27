using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerInputHandler : MonoBehaviour, IPlayerInputHandler
{
    //This class invokes input events.

    public event Action<Vector2> OnMoveInput;
    public event Action OnAttackInputDown;
    public event Action OnAttackInputUp;
    public event Action OnPickupInput;

    private PlayerControls playerInputAsset;    

    private void Awake()
    {
        playerInputAsset = new PlayerControls();
        SubscribePlayerInputEvents();
    }

    private void OnEnable()
    {
        playerInputAsset.General.Enable();
    }

    private void OnDisable()
    {
        playerInputAsset.General.Disable();
    }

    private void SubscribePlayerInputEvents()
    {
        playerInputAsset.General.Move.performed += ResolveMoveDown;

        playerInputAsset.General.Move.canceled += ResolveMoveInputUp;

        playerInputAsset.General.Attack.started += ResolveAttackInput;

        playerInputAsset.General.Attack.canceled += ResolveAttackInputCancelled;

        playerInputAsset.General.Pickup.performed += ResolvePickup;
    }

    private void ResolveAttackInputCancelled(InputAction.CallbackContext context)
    {
        OnAttackInputUp?.Invoke();
    }

    private void ResolveAttackInput(InputAction.CallbackContext context)
    {
        OnAttackInputDown?.Invoke();
    }

    private void ResolveMoveInputUp(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(Vector2.zero);
    }

    private void ResolveMoveDown(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    private void ResolvePickup(InputAction.CallbackContext context)
    {
        OnPickupInput?.Invoke();
    }
}
