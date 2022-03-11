using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.UI;

namespace MagicalFlowers.Player
{
    public class PlayerActor : BaseActor
    {
        public PlayerInventory inventory;

        //移動先の座標
        protected Vector3 targetPosition;
        //斜め移動モード用フラグ
        [SerializeField] bool diagonalMode = false;
        //方向転換モード用フラグ
        [SerializeField] bool directionMode = false;
        //足音
        [SerializeField] AudioClip FootStepSound;
        //アニメーター
        Animator animator;
        Vector2Int inputValue;
        float moveInputTimer = 0;
        const float moveInputTime = 0.1f;


        //入力用プロバイダークラス
        IPlayerInputProvider inputProvider;

        private void Awake()
        {
            animator = this.GetComponent<Animator>();
            inputProvider = new UnityInputProvider();
        }

        private void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            position = StageUtility.WorldPos2StagePos(transform.position);
            StageManager.Instance.AddActor(this);
        }

        protected override void InputWaitProcess()
        {
            if (inventory.IsOpen)
            {
                return;
            }

            bool inputCheck = false;

            //攻撃宣言時
            if(inputProvider.GetAttackButton())
            {
                ActionState = ActionType.Attack;
                ActorState = ActorStateType.ActionBegin;
            }

            inputValue = inputProvider.GetMoveVector();
            //斜め移動モード用の入力確認分岐
            if (diagonalMode)
            {
                inputCheck = inputValue.x != 0 && inputValue.y != 0;
            }
            else
            {
                inputCheck = inputValue.x != 0 || inputValue.y != 0;
            }
            

            if (inputCheck)
            {
                moveInputTimer += Time.deltaTime;
                direction = inputValue;

                //回転(仮 滑らかにした方がいいかも)
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x,-direction.y) * Mathf.Rad2Deg, Vector3.up);

                //方向モードでない時かつ一定時間入力しているときのみ移動する
                if (!directionMode && moveInputTimer >= moveInputTime)
                {
                    if (StageManager.Instance.CheckMove(position ,direction))
                    {
                        targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                        ActionState = ActionType.Move;
                        ActorState = ActorStateType.ActionBegin;
                        AudioManager.Instance.SE.PlayOneShot(FootStepSound);
                        moveInputTimer = 0;
                    }
                }
            }
            else
            {
                moveInputTimer = 0;
            }

        }

        protected override void ActionBeginProcess()
        {
            switch (ActionState)
            {
                //移動中処理
                case ActionType.Move:
                    animator.SetBool("isMove", true);
                    var step = 5 * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                    if (transform.position == targetPosition)
                    {
                        ActorState = ActorStateType.ActionEnd;
                    }
                    break;
                    //攻撃
                case ActionType.Attack:
                    if(!StageManager.Instance.CheckAttack(position, direction))
                    {
                        ActorState = ActorStateType.ActionEnd;
                        return;
                    }
                    var enemy = StageManager.Instance.GetActorData(
                    position.x + direction.x, position.y + direction.y);
                    if (enemy == null)
                    {
                        ActorState = ActorStateType.ActionEnd;
                        return;
                    }

                    int atkBonus = 0;
                    
                    foreach (var item in effects)
                    {
                        if (item.Type == EffectType.AtkUp)
                        {
                            atkBonus += item.value;
                        }
                    }
                    int d = enemy.TakeDamage(attackPoint + atkBonus);
                    MessageLogManager.Instance.AttackLog(this, enemy, d);   
                    ActorState = ActorStateType.ActionEnd;
                    break;
                case ActionType.None:
                    break;
            }
            
        }

        protected override void ActionEndProcess()
        {
            switch (ActionState)
            {
                case ActionType.Move:
                    //移動完了したので座標を適用する
                    animator.SetBool("isMove", false);
                    position += direction;
                    break;
                case ActionType.Attack:
                    break;
                case ActionType.None:
                    break;
            }
            ActionState = ActionType.None;
        }
        public override int TakeDamage(int damege)
        {
            int d = damege - defensePoint;
            if (d <= 0) { d = 1; }
            health -= d;
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
            return d;
        }

        public override string GetActorName()
        {
            return "プレイヤー";
        }
    }
}