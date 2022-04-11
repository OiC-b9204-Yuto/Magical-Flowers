using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.Common;
using MagicalFlowers.Base;
using MagicalFlowers.Player;
using MagicalFlowers.Item;

namespace MagicalFlowers.UI
{
    public class MessageLogManager : SingletonMonoBehaviour<MessageLogManager>
    {
        [SerializeField] MessageLogView logView;

        /// <summary>
        /// �v���C���[���U������Ƃ��̃��O�o�͊֐�
        /// </summary>
        public void AttackLog(PlayerActor player, BaseActor target, int amount)
        {
            logView.AddLogText($"{player.GetActorName()}�̍U���I<color=yellow>{target.GetActorName()}</color>��<color=red>{amount}</color>�̃_���[�W�����������I");
        }
        /// <summary>
        /// �U�����ʂ̃��O�o�͊֐�
        /// </summary>
        public void AttackLog(BaseActor attaker, BaseActor target, int amount)
        {
            var player = attaker as PlayerActor;
            if (!player) 
            {
                logView.AddLogText($"<color=yellow>{attaker.GetActorName()}</color>�̍U���I<color=yellow>{target.GetActorName()}</color>��<color=red>{amount}</color>�̃_���[�W���������I");
            }
            else
            {
                AttackLog(player, target, amount);
            }
        }

        public void OtherDamageLog(PlayerActor player, int amount)
        {
            logView.AddLogText($"{player.GetActorName()}��<color=red>{amount}</color>�̃_���[�W��������");
        }

        public void OtherDamageLog(BaseActor actor, int amount)
        {
            logView.AddLogText($"<color=yellow>{actor.GetActorName()}</color>��<color=red>{amount}</color>�̃_���[�W��������");
        }

        public void ItemHealLog(BaseActor actor, available item)
        {
            logView.AddLogText($"<color=yellow>{actor.GetActorName()}</color>��<color=orange>{item.Name}</color>���g�p�����B�̗͂�<color=lime>{item.value}</color>�񕜂���");
        }

        public void ItemAttackUpLog(BaseActor actor, available item)
        {
            logView.AddLogText($"<color=yellow>{actor.GetActorName()}</color>��<color=orange>{item.Name}</color>���g�p�����B�U���͂�<color=lime>{item.value}</color>�㏸����");
        }

        public void ItemDefenseUpLog(BaseActor actor, available item)
        {
            logView.AddLogText($"<color=yellow>{actor.GetActorName()}</color>��<color=orange>{item.Name}</color>���g�p�����B�h��͂�<color=lime>{item.value}</color>�㏸����");
        }

        public void ItemMaxHealthUpLog(BaseActor actor, available item)
        {
            logView.AddLogText($"<color=yellow>{actor.GetActorName()}</color>��<color=orange>{item.Name}</color>���g�p�����B�̗͂̍ő�l��<color=lime>{item.value}</color>�㏸����");
        }

        public void OutputLog(string message)
        {
            logView.AddLogText(message);
        }
    }
}