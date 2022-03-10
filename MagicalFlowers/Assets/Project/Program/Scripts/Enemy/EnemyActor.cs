using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Enemy
{
    public class EnemyActor : BaseActor
    {
        //プレイヤーの情報
        Vector2Int PlayerPosition;
        //移動先座標
        public Vector3 targetPosition;
        [SerializeField] Vector2Int ShownMyPosition;
        [SerializeField] Vector2Int ShownTargetPosition;
        [SerializeField]int RandomX;
        [SerializeField] int RandomY;


        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            position = StageUtility.WorldPos2StagePos(transform.position);
        }
        void Update()
        {

        }

        protected override void ActionBeginProcess()
        {
            var step = 5 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position == targetPosition)
            {
                ActorState = ActorStateType.ActionEnd;
            }
        }

        protected override void ActionEndProcess()
        {
            position += direction;
        }

        protected override void InputWaitProcess()
        {
            ShownMyPosition = position;
            ShownTargetPosition = PlayerPosition;
            RandomX = Random.Range(-10, 10);
            RandomY = Random.Range(-10, 10);
            if (RandomX >= 3){ RandomX = 1; }else if(RandomX <= -3) { RandomX = -1; }else if(RandomX <= 0) { RandomX = 0; }
            if (RandomY >= 3){ RandomY = 1; }else if(RandomY <= -3) { RandomY = -1; }else if (RandomY <= 0) { RandomY = 0; }
            direction = new Vector2Int(RandomX, RandomY);
            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }
            
        }
    }
}