using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class TapManager : MonoBehaviour
    {
        //�t���b�N����p ���Ԃ������l
        private float flickTime = 0.15f;
        //�t���b�N����p �ړ�����
        private float flickMagnitude = 100;

        //�^�b�v�J�n�|�C���g
        private Vector3 startPosition;
        //�^�b�v�I���|�C���g
        private Vector3 endPosition;
        // �ړ����̃|�W�V����
        private Vector3 movePosition;
        // �Ō�ɂ����|�W�V����
        private Vector3 lastPosition;

        //�t���b�N����p �^�C�}�[
        private float timer = 0.0f;

        // �t���b�N�̍��E����p�̃}�[�W��
        public int flickMargin = 10;

        public bool isRightFlick = false;
        public bool isLeftFlick = false;
        public bool isUpFlick = false;
        public bool isDownFlick = false;

        public bool isRightSwipe = false;
        public bool isLeftSwipe = false;
        public bool isUpSwipe = false;
        public bool isDownSwipe = false;

        private const string MODE_IS_FLICK = "flick";
        private const string MODE_IS_SWIPE = "swipe";

        private const string DIRECTION_IS_RIGHT = "right";
        private const string DIRECTION_IS_LEFT = "left";
        private const string DIRECTION_IS_UP = "up";
        private const string DIRECTION_IS_DOWN = "down";

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                //�^�b�v�J�n��
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //�^�b�v�J�n�|�C���g���擾
                    startPosition = Input.GetTouch(0).position;
                    lastPosition = Input.GetTouch(0).position;
                }
                //�^�b�v�I����
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //�^�b�v�I���|�C���g���擾
                    endPosition = Input.GetTouch(0).position;

                    //�^�b�v�J�n�`�I���|�C���g�̋���
                    Vector3 direction = endPosition - startPosition;

                    //�������w��ȏ�A�^�b�v���Ԃ��w��ȉ��̏ꍇ�A�t���b�N�Ɣ���
                    if (direction.magnitude >= flickMagnitude && timer <= flickTime)
                    {
                        SetDirection(direction, MODE_IS_FLICK);
                    }
                    //�^�C�}�[��������
                    timer = 0.0f;
                }

                //�^�b�v��
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //�^�b�v���Ԃ��������l���z�����ꍇ�A�X���C�v�Ɣ���
                    if (timer >= flickTime)
                    {
                        //Swipe
                        // �ړ����̃|�W�V�������擾
                        movePosition = Input.GetTouch(0).position;

                        Vector3 direction = movePosition - lastPosition;


                        SetDirection(direction, MODE_IS_SWIPE);

                        // �Ō�ɂ����|�W�V�������X�V
                        lastPosition = movePosition;
                    }
                    //�������Ă���ԁA�^�C�}�[�����Z
                    timer += Time.deltaTime;
                }
            } else
            {
                SetAllDirectionFalse();
            }
        }

        private void SetDirection(Vector3 direction, string mode)
        {
            //x���̋������傫���ꍇ�͍��E�ւ̈ړ�
            if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
            {
                if (direction.x >= 0 + flickMargin)
                {
                    SetMoveDirection(DIRECTION_IS_RIGHT, mode);
                }
                else if (direction.x < 0 - flickMargin)
                {
                    SetMoveDirection(DIRECTION_IS_LEFT, mode);
                }
            }
            //y���̋������傫���ꍇ�͏㉺�̈ړ�
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y >= 0)
                {
                    SetMoveDirection(DIRECTION_IS_UP, mode);
                }
                else
                {
                    SetMoveDirection(DIRECTION_IS_DOWN, mode);
                }
            }
        }

        private void SetMoveDirection(string direction, string mode)
        {
            if (mode == MODE_IS_FLICK)
            {
                SetFlickDirection(direction);
            } else if (mode == MODE_IS_SWIPE)
            {
                SetSwipeDirection(direction);
            }
        }

        private void SetFlickDirection(string direction)
        {
            if (direction == DIRECTION_IS_RIGHT)
            {
                isRightFlick = true;
                isLeftFlick = false;
                isUpFlick = false;
                isDownFlick = false;
            }
            else if (direction == DIRECTION_IS_LEFT)
            {
                isRightFlick = false;
                isLeftFlick = true;
                isUpFlick = false;
                isDownFlick = false;
            }
            else if (direction == DIRECTION_IS_UP)
            {
                isRightFlick = false;
                isLeftFlick = false;
                isUpFlick = true;
                isDownFlick = false;
            }
            else if (direction == DIRECTION_IS_DOWN)
            {
                isRightFlick = false;
                isLeftFlick = false;
                isUpFlick = false;
                isDownFlick = true;
            }
        }

        private void SetSwipeDirection(string direction)
        {
            if (direction == DIRECTION_IS_RIGHT)
            {
                isRightSwipe = true;
                isLeftSwipe = false;
                isUpSwipe = false;
                isDownSwipe = false;
            }
            else if (direction == DIRECTION_IS_LEFT)
            {
                isRightSwipe = false;
                isLeftSwipe = true;
                isUpSwipe = false;
                isDownSwipe = false;
            }
            else if (direction == DIRECTION_IS_UP)
            {
                isRightSwipe = false;
                isLeftSwipe = false;
                isUpSwipe = true;
                isDownSwipe = false;
            }
            else if (direction == DIRECTION_IS_DOWN)
            {
                isRightSwipe = false;
                isLeftSwipe = false;
                isUpSwipe = false;
                isDownSwipe = true;
            }
        }

        private void SetAllDirectionFalse()
        {
            isRightFlick = false;
            isLeftFlick = false;
            isUpFlick = false;
            isDownFlick = false;

            isRightSwipe = false;
            isLeftSwipe = false;
            isUpSwipe = false;
            isDownSwipe = false;
        }
    }
}
