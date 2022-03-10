using MagicalFlowers.Base;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MagicalFlowers.Enemy
{
    public class EnemyActor : BaseActor
    {
        //自分自身の情報
        [SerializeField] [Range(10, 100)] private float PlayerSearchDistance = 15.0f; //プレイヤー発見となる物体の距離
        //プレイヤーの情報
        private PlayerActor Player;
        Vector2Int PlayerPosition;
        //移動先座標
        public Vector3 targetPosition;
        Vector3 LatestPositon;
        float Distance;
        //ランダム移動
        int RandomX;
        int RandomY;
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            StageManager.Instance.AddActor(this);
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
            Distance = Vector2Int.Distance(PlayerPosition, position);
            Mathf.Abs(Distance);
            if (Distance > PlayerSearchDistance) //プレイヤーとの距離が遠い時
            {
                
                Vector3 diff = this.transform.position - LatestPositon;
                LatestPositon = this.transform.position;
                if (diff.magnitude > 0.01f)
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
                //ActorState = ActorStateType.ActionEnd;
            }
            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }

        }
    }
}