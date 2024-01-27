using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerTwoInputHandler : MonoBehaviour
{
    //This class invokes input events.

    public event Action<Vector2> OnMoveInput;
    public event Action OnAttackInputDown;
    public event Action OnAttackInputUp;

    public static PlayerTwoInputHandler Instance;

    private PlayerControls playerInputAsset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void OnEnable()
    {
        playerInputAsset = PlayerInputHandler.Instance.PlayerInputAsset;

        SubscribePlayerInputEvents();
    }

    private void SubscribePlayerInputEvents()
    {
        playerInputAsset.PlayerTwo.Move.performed += ResolveMoveDown;

        playerInputAsset.PlayerTwo.Move.canceled += ResolveMoveInputUp;

        playerInputAsset.PlayerTwo.Attack.started += ResolveAttackInput;

        playerInputAsset.PlayerTwo.Attack.canceled += ResolveAttackInputCancelled;
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
}
