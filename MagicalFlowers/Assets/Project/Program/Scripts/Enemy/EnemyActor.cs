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
        //�������g�̏��
        [SerializeField] [Range(0, 100)] private float PlayerSearchDistance = 15.0f; //�v���C���[�����ƂȂ镨�̂̋���
        //�v���C���[�̏��
        private PlayerActor Player;
        Vector2Int PlayerPosition;
        //�ړ�����W
        public Vector3 targetPosition;
        Vector3 LatestPositon;
        float Distance;
        //�����_���ړ�
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
            
            if (Distance > PlayerSearchDistance) //�v���C���[�Ƃ̋�����������
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
            else //�v���C���[�Ƃ̋������߂���
            {
                this.transform.LookAt(Player.transform);
                this.GetComponent<Pathfinding>().FindPath(transform.position);
                DebugLogger.Log("x:" + (GridGenerator.Instance.FinalPath[0].GridX).ToString() + " / " + "y" + (GridGenerator.Instance.FinalPath[0].GridY).ToString());
                direction = new Vector2Int(GridGenerator.Instance.FinalPath[0].GridX, GridGenerator.Instance.FinalPath[0].GridY) - position;
            }
            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }

        }
    }
}