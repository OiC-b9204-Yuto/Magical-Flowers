using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Base
{
    public abstract class BaseActor : BaseObject
    {
        //キャラの向き
        protected Vector2Int direction;

        //アクションの状態用
        private ActorState actorActionState;
        public ActorState ActorActionState
        {
            get { return actorActionState; }
            set {
                if (actorActionState == value) { return; }
                actorActionState = value;
                if (value == ActorState.ActionEnd) { ActionEndProcess(); } 
            }
        }

        public enum ActorState
        {
            InputWait,
            ActionBegin,
            ActionEnd,
        }

        public void ActorActionStateReset() { actorActionState = ActorState.InputWait; }

        public enum ActionType
        {
            Move,
            Attack
        }

        //ステータス
        protected int health;
        protected int maxHealth;
        protected int attackPoint;
        protected int defensePoint;
        //バフ、デバフ用の効果用リスト（未作成）

        public virtual void UpdateAction()
        {
            switch (actorActionState)
            {
                case ActorState.InputWait:
                    InputWaitProcess();
                    break;
                case ActorState.ActionBegin:
                    ActionBeginProcess();
                    break;
            }
        }
        protected abstract void InputWaitProcess();
        protected abstract void ActionBeginProcess();
        protected abstract void ActionEndProcess();

        //ダメージを受ける関数
        public void TakeDamage(int damege)
        {
            int d = damege - defensePoint;
            health -= d > 0 ? d : 1;
            
        }
        //回復を受ける関数
        public void TakeHeal(int heal)
        {
            health += heal;
            if(health > maxHealth) { health = maxHealth; }
        }

        //体力の最大値を増やす関数
        public void AddMaxHealth(int value)
        {
            maxHealth += value;
        }

        //攻撃力を増やす関数
        public void AddAttackPoint(int value)
        {
            attackPoint += value;
        }

        //防御力を増やす関数
        public void AddDefensePoint(int value)
        {
            defensePoint += value;
        }
    }
}