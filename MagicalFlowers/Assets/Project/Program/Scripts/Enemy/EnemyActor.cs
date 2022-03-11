using MagicalFlowers.Base;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using MagicalFlowers.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MagicalFlowers.UI;
using MagicalFlowers.Item;

namespace MagicalFlowers.Enemy
{
    public class EnemyActor : BaseActor
    {
        [SerializeField] EnemyParameter parameter;
 
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

        [SerializeField] CameraRenderCheck cameraRenderCheck;
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            StageManager.Instance.AddActor(this);
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
            position = StageUtility.WorldPos2StagePos(transform.position);
            maxHealth = parameter.MaxHealth;
            health = parameter.MaxHealth;
            attackPoint = parameter.Attack;
            defensePoint = parameter.Defense;

            GameManager.Instance.AddActor(this);
        }

        protected override void ActionBeginProcess()
        {
            switch (ActionState)
            {
                case ActionType.Move:
                   
                    if (!cameraRenderCheck || cameraRenderCheck.IsRender)
                    {
                        var step = 5 * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                    }
                    else
                    {
                        //�J�����Ɏʂ��Ă��Ȃ��ꍇ�͂����Ɉړ�������
                        transform.position = targetPosition;
                    }

                    if (transform.position == targetPosition)
                    {
                        ActorState = ActorStateType.ActionEnd;
                    }
                    break;
                case ActionType.Attack:
                    int atkBonus = 0;

                    foreach (var item in effects)
                    {
                        if (item.Type == EffectType.AtkUp)
                        {
                            atkBonus += item.value;
                        }
                    }
                    int d = Player.TakeDamage(attackPoint + atkBonus, this);
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
            direction = PlayerPosition - Position;

            if(Distance < 1.5f && StageManager.Instance.CheckAttack(position,direction))//�ړG��
            {
                ActionState = ActionType.Attack;
                ActorState = ActorStateType.ActionBegin;
            }
            else if(Distance <= PlayerSearchDistance)//�v���C���[�Ƃ̋������߂���
            {
                this.transform.LookAt(Player.transform);
                this.GetComponent<Pathfinding>().FindPath(transform.position);
                //DebugLogger.Log("x:" + (GridGenerator.Instance.FinalPath[0].GridX).ToString() + " / " + "y" + (GridGenerator.Instance.FinalPath[0].GridY).ToString());
                direction = new Vector2Int(GridGenerator.Instance.FinalPath[0].GridX, GridGenerator.Instance.FinalPath[0].GridY) - position;
                ActionState = ActionType.Move;
            }
            else //�v���C���[�Ƃ̋�����������
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
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg, Vector3.up);

            if (StageManager.Instance.CheckMove(position, direction))
            {
                targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                ActorState = ActorStateType.ActionBegin;
            }

        }

        public override string GetActorName()
        {
            return parameter.Name;
        }

        public override int TakeDamage(int damege, BaseActor actor)
        {
            int d = damege - defensePoint;
            if (d <= 0) { d = 1; }
            health -= d;
            MessageLogManager.Instance.AttackLog(actor, this, d);
            if (health <= 0)
            {
                IsDead = true;
                StageManager.Instance.RemoveActor(this);
                Destroy(this.gameObject);
                actor.currentExp += parameter.Exp;
                MessageLogManager.Instance.OutputLog($"<color=yellow>{this.GetActorName()}</color>��|�����I{actor.GetActorName()}��<color=cyan>{parameter.Exp}</color>�̌o���l�����I");
            }
            return d;
        }
    }
}