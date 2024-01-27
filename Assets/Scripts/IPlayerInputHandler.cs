using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInputHandler
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnAttackInputDown;
    public event Action OnAttackInputUp;
    public event Action OnPickupInput;
}
