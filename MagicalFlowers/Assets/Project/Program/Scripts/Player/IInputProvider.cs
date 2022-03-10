using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public interface IPlayerInputProvider
    {
        /// <summary>
        /// ¶ã0,0Šî€‚Ì“ü—Íæ“¾
        /// </summary>
        /// <returns>x:Left -1,Right 1/ y:up -1, down 1 </returns>
        public Vector2Int GetMoveVector();

        public bool GetAttackButton();
        public bool GetPickUpButton();
        public bool GetSkipTurnButton();
        public bool GetDiagonalModeButton();
        public bool GetDirectionModeButton();
    }
}