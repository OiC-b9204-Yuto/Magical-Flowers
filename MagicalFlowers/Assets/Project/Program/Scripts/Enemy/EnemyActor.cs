using MagicalFlowers.Base;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using MagicalFlowers.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MagicalFlowers.Enemy
{
    public class EnemyActor : BaseActor
    {
        //自分自身の情報
        [SerializeField] [Range(0, 100)] private float PlayerSearchDistance = 15.0f; //プレイヤー発見となる物体の距離
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
        //A*
        Node node;
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
            switch (ActionState)
            {
                case ActionType.Move:
                    var step = 5 * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                    if (transform.position == targetPosition)
                    {
                        ActorState = ActorStateType.ActionEnd;
                    }
                    break;
                case ActionType.Attack:
                    
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
                    position += direction;
                    break;
                case ActionType.Attack:
                    break;
                case ActionType.None:
                    break;
            }
            ActionState = ActionType.None;
        }

        protected override void InputWaitProcess()
        {
            PlayerPosition = Player.Position;
            Distance = Vector2Int.Distance(PlayerPosition, position);
            Mathf.Abs(Distance);
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x,-direction.y) * Mathf.Rad2Deg, Vector3.up);

            if(Distance < 1.5f && StageManager.Instance.CheckAttack(position,direction))//接敵中
            {
                ActionState = ActionType.Attack;
                ActorState = ActorStateType.ActionBegin;
            }
            else if(Distance <= PlayerSearchDistance)//プレイヤーとの距離が近い時
            {
                this.transform.LookAt(Player.transform);
                this.GetComponent<Pathfinding>().FindPath(transform.position);
                //DebugLogger.Log("x:" + (GridGenerator.Instance.FinalPath[0].GridX).ToString() + " / " + "y" + (GridGenerator.Instance.FinalPath[0].GridY).ToString());
                direction = new Vector2Int(GridGenerator.Instance.FinalPath[0].GridX, GridGenerator.Instance.FinalPath[0].GridY) - position;
                ActionState = ActionType.Move;
            }
            else //プレイヤーとの距離が遠い時
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
                ActionState = ActionType.Move;
            }

            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }

        }
    }
}