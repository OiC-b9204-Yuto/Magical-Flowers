using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.Common;
using MagicalFlowers.Base;
using MagicalFlowers.Player;

namespace MagicalFlowers.UI
{
    public class MessageLogManager : SingletonMonoBehaviour<MessageLogManager>
    {
        [SerializeField] MessageLogView logView;

        /// <summary>
        /// プレイヤーが攻撃するときのログ出力関数
        /// </summary>
        public void AttackLog(PlayerActor player, BaseActor target, int amount)
        {
            logView.AddLogText($"{player.GetActorName()}の攻撃！<color=yellow>{target.GetActorName()}</color>に<color=red>{amount}</color>のダメージをあたえた！");
        }
        /// <summary>
        /// 第三者（プレイヤー以外）が攻撃するときのログ出力関数
        /// </summary>
        public void AttackLog(BaseActor attaker, BaseActor target, int amount)
        {
            logView.AddLogText($"<color=yellow>{attaker.GetActorName()}</color>の攻撃！<color=yellow>{target.GetActorName()}</color>は<color=red>{amount}</color>のダメージをうけた！");
        }

        public void OtherDamageLog(PlayerActor player, int amount)
        {
            logView.AddLogText($"{player.GetActorName()}は<color=red>{amount}</color>のダメージをうけた");
        }

        public void OtherDamageLog(BaseActor baseActor, int amount)
        {
            logView.AddLogText($"<color=yellow>{baseActor.GetActorName()}</color>は<color=red>{amount}</color>のダメージをうけた");
        }

        public void OutputLog(string message)
        {
            logView.AddLogText(message);
        }
    }
}