using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public class PlayerActor : BaseActor
    {
        //�ړ���̍��W
        protected Vector3 targetPosition;

        //�΂߈ړ����[�h�p�t���O
        bool diagonalMode = false;
        //�����]�����[�h�p�t���O
        bool directionMode = false;

        Vector2Int inputValue;
        float moveInputTimer = 0;
        const float moveInputTime = 0.1f;


        //���͗p�v���o�C�_�[�N���X
        IPlayerInputProvider inputProvider;

        private void Awake()
        {
            inputProvider = new UnityInputProvider();
        }

        protected override void InputWaitProcess()
        {
            inputValue = inputProvider.GetMoveVector();
            bool inputCheck = false;
            //�΂߈ړ����[�h�p�̓��͊m�F����
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

                //��](�� ���炩�ɂ���������������)
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x,-direction.y) * Mathf.Rad2Deg, Vector3.up);

                //�������[�h�łȂ�������莞�ԓ��͂��Ă���Ƃ��݈̂ړ�����
                if (!directionMode && moveInputTimer >= moveInputTime)
                {
                    if (StageManager.Instance.CheckMove(position + direction))
                    {
                        targetPosition = transform.position + new Vector3(direction.x, 0, -direction.y);
                        ActorActionState = ActorState.ActionBegin;
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
            //�ړ�������
            var step = 5 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position == targetPosition)
            {
                ActorActionState = ActorState.ActionEnd;
            }
        }

        protected override void ActionEndProcess()
        {
            //�ړ����������̂ō��W��K�p����
            position += direction;
        }
    }
}