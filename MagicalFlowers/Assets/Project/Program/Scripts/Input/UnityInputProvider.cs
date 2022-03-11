using MagicalFlowers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputProvider : IPlayerInputProvider
{
    public Vector2Int GetMoveVector()
    {
        return new Vector2Int(
            Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0,
            Input.GetKey(KeyCode.W) ? -1 : Input.GetKey(KeyCode.S) ? 1 : 0            
            );
    }

    public bool GetAttackButton()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetPickUpButton()
    {
        throw new System.NotImplementedException();
    }

    public bool GetSkipTurnButton()
    {
        throw new System.NotImplementedException();
    }

    public bool GetDiagonalModeButton()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

    public bool GetDirectionModeButton()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}
