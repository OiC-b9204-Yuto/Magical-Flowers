using MagicalFlowers.Base;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Enemy
{
    public class EnemyActor : BaseActor
    {
        //プレイヤーの情報
        [SerializeField]private PlayerActor Player;
        [SerializeField] Vector2Int PlayerPosition;
        //移動先座標
        public Vector3 targetPosition;
        [SerializeField] Vector3 LatestPositon;
        [SerializeField] int RandomX;
        [SerializeField] int RandomY;
        [SerializeField] bool LockPlayer;
        [SerializeField] float Distance;
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
            position = StageUtility.WorldPos2StagePos(transform.position);
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
            PlayerPosition = Player.Position;
            Distance = (PlayerPosition - position).sqrMagnitude;
            Mathf.Abs(Distance);
            if (Distance > 15) //プレイヤーとの距離が遠い時
            {
                Vector3 diff = this.transform.position - LatestPositon;
                LatestPositon = this.transform.position;
                if(diff.magnitude > 0.01f)
                {
                    transform.rotation = Quaternion.LookRotation(diff);
                }
                RandomX = Random.Range(-1, 2);
                RandomY = Random.Range(-1, 2);
                direction = new Vector2Int(RandomX, RandomY);
            }
            else //プレイヤーとの距離が近い時
            {
                this.transform.LookAt(Player.transform);
                RandomX = Random.Range(-1, 2);
                RandomY = Random.Range(-1, 2);
                direction = new Vector2Int(RandomX, RandomY);
            }
            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }
            
        }
    }
}