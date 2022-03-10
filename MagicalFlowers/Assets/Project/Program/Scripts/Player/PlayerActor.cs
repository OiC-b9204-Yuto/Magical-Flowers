using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public class PlayerActor : BaseActor
    {
        //移動先の座標
        protected Vector3 targetPosition;

        //斜め移動モード用フラグ
        [SerializeField]bool diagonalMode = false;
        //方向転換モード用フラグ
        [SerializeField] bool directionMode = false;

        Vector2Int inputValue;
        float moveInputTimer = 0;
        const float moveInputTime = 0.1f;


        //入力用プロバイダークラス
        IPlayerInputProvider inputProvider;

        private void Awake()
        {
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
            inputValue = inputProvider.GetMoveVector();
            bool inputCheck = false;
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
                        ActorState = ActorStateType.ActionBegin;
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
            //移動中処理
            var step = 5 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position == targetPosition)
            {
                ActorState = ActorStateType.ActionEnd;
            }
        }

        protected override void ActionEndProcess()
        {
            //移動完了したので座標を適用する
            position += direction;
        }
    }
}