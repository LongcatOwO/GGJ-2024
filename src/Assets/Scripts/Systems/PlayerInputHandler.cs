using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerInputHandler : MonoBehaviour
{
    //This class invokes input events.

    public static PlayerInputHandler Instance;

    public event Action<Vector2> OnMoveInput;
    public event Action OnAttackInputDown;
    public event Action OnAttackInputUp;
    public event Action OnPickupInput;
    public event Action OnChangeTargetInput;
    
    public event Action<Vector2> OnPlayerTwoMoveInput;
    public event Action OnPlayerTwoAttackInputDown;
    public event Action OnPlayerTwoAttackInputUp;
    public event Action OnPlayerTwoPickupInput;

    [Header("Player Input Properties")]
    [SerializeField] private bool enablePlayerTwoInput;

    private PlayerControls playerInputAsset;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        playerInputAsset = new PlayerControls();
    }

    private void OnEnable()
    {
        SubscribePlayerInputEvents();

        if (enablePlayerTwoInput)
        {
            SubscribePlayerTwoInputEvents();
        }

        playerInputAsset.Enable();
    }

    private void OnDisable()
    {
        playerInputAsset.Disable();
    }

    private void SubscribePlayerInputEvents()
    {
        playerInputAsset.General.Move.performed += ResolveMoveDown;

        playerInputAsset.General.Move.canceled += ResolveMoveInputUp;

        playerInputAsset.General.Attack.started += ResolveAttackInput;

        playerInputAsset.General.Attack.canceled += ResolveAttackInputCancelled;

        playerInputAsset.General.Pickup.performed += ResolvePickup;

        playerInputAsset.General.ChangeTarget.performed += ResolveChangeTargetInput;
    }

    private void ResolveChangeTargetInput(InputAction.CallbackContext context)
    {
        OnChangeTargetInput?.Invoke();
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

    #region Player Two Input
    public void SubscribePlayerTwoInputEvents()
    {
        playerInputAsset.PlayerTwo.Move.performed += ResolvePlayerTwoMoveDown;

        playerInputAsset.PlayerTwo.Move.canceled += ResolvePlayerTwoMoveInputUp;

        playerInputAsset.PlayerTwo.Attack.started += ResolvePlayerTwoAttackInput;

        playerInputAsset.PlayerTwo.Attack.canceled += ResolvePlayerTwoAttackInputCancelled;

        playerInputAsset.PlayerTwo.Pickup.performed += ResolvePlayerTwoPickup;
    }

    private void ResolvePlayerTwoAttackInputCancelled(InputAction.CallbackContext context)
    {
        OnPlayerTwoAttackInputUp?.Invoke();
    }

    private void ResolvePlayerTwoAttackInput(InputAction.CallbackContext context)
    {
        OnPlayerTwoAttackInputDown?.Invoke();
    }

    private void ResolvePlayerTwoMoveInputUp(InputAction.CallbackContext context)
    {
        OnPlayerTwoMoveInput?.Invoke(Vector2.zero);
    }

    private void ResolvePlayerTwoMoveDown(InputAction.CallbackContext context)
    {
        OnPlayerTwoMoveInput?.Invoke(context.ReadValue<Vector2>() * -1);
    }

    private void ResolvePlayerTwoPickup(InputAction.CallbackContext context)
    {
        OnPlayerTwoPickupInput?.Invoke();
    }
    #endregion
}
