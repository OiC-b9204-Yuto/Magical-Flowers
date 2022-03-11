using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.Item;

namespace MagicalFlowers.Base
{
    public abstract class BaseActor : BaseObject
    {
        //キャラの向き
        protected Vector2Int direction;

        //アクションの状態用
        private ActorStateType actorState;
        public ActorStateType ActorState
        {
            get { return actorState; }
            set {
                if (actorState == value) { return; }
                actorState = value;
                if (value == ActorStateType.ActionEnd) { ActionEndProcess(); } 
            }
        }

        public enum ActorStateType
        {
            InputWait,
            ActionBegin,
            ActionEnd,
        }

        public void ActorActionStateReset() { actorState = ActorStateType.InputWait; }

        private ActionType actionState;
        public ActionType ActionState
        {
            get { return actionState; }
            set { if (actionState == value) { return; } actionState = value; }
        }
        public enum ActionType
        {
            Move,
            Attack,

            None
        }

        //ステータス
        protected int health;
        protected int maxHealth;
        protected int attackPoint;
        protected int defensePoint;
        //バフ、デバフ用の効果用リスト（未作成）
        protected struct effect
        {
            public EffectType  Type;
            public int         value;
        }

        protected List<effect> effects = new List<effect>();
        public virtual void UpdateAction()
        {
            switch (actorState)
            {
                case ActorStateType.InputWait:
                    InputWaitProcess();
                    break;
                case ActorStateType.ActionBegin:
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

        //エフェクト周り
        public void AddEffects(EffectType type , int value)
        {
            effect eff = new effect();
            eff.Type = type;
            eff.value = value;
            effects.Add(eff);
        }
    }
}