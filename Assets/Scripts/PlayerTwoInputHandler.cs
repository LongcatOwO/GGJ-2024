using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerTwoInputHandler : MonoBehaviour, IPlayerInputHandler
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
        playerInputAsset.PlayerTwo.Enable();
    }

    private void OnDisable()
    {
        playerInputAsset.PlayerTwo.Disable();
    }

    private void SubscribePlayerInputEvents()
    {
        playerInputAsset.PlayerTwo.Move.performed += ResolveMoveDown;

        playerInputAsset.PlayerTwo.Move.canceled += ResolveMoveInputUp;

        playerInputAsset.PlayerTwo.Attack.started += ResolveAttackInput;

        playerInputAsset.PlayerTwo.Attack.canceled += ResolveAttackInputCancelled;

        playerInputAsset.PlayerTwo.Pickup.performed += ResolvePickup;
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
        Vector2 inputDirection = context.ReadValue<Vector2>();

        OnMoveInput?.Invoke(inputDirection * -1);
    }

    private void ResolvePickup(InputAction.CallbackContext context)
    {
        OnPickupInput?.Invoke();
    }
}
