using UnityEngine;
using Common;
using Managers;

namespace Controllers
{
    public class BalloonController : MonoBehaviour
    {
        public GameObject playerManager;
        public GameObject tapManager;


        // �ϐ��錾
        public float mouseSensitivity = 100;
        public float touchSensitivity = 100;

        public GameObject canvas;

        public float imageMarginX = 1f;
        public float imageMarginY = 2f;

        public float movingRotation = 15f;

        private float clearElapsedTime = 0f;
        private float clearLerpRaterate;
        public float clearMoveSpeed = 5f;
        private Vector3 clearStartPos;
        private Vector3 clearEndPos;
        private bool isSetClearPos = false;

        public bool isEndClearMove = false;

        void Start()
        {
            CanvasCoordinate.setCanvas(canvas);
        }

        // Update is called once per frame
        void Update()
        {
            if (playerManager.GetComponent<Managers.PlayerManager>().isGameClear)
            {
                ClearMove();

                // �ړ�������������t���O��ύX����B
                if (transform.position.y >= clearEndPos.y)
                {
                    SetIsEndClearMove(true);
                }

            }
            else
            {
                // �Q�[���N���A�ȊO�̏ꍇ�͑���ł���
                ChangeRotation();
                MoveSwipe();
            }
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!playerManager.GetComponent<Managers.PlayerManager>().isGameClear)
            {
                PlaySceneManager.GameOver(GameObject.Find("Balloon"));
            }
        }

        // �X���C�v���ď㉺���E�ɓ�����
        void MoveSwipe()
        {
            // ��(�])�����̓���
            float fHorizontalInput = Input.GetAxis("Horizontal");
            // �c(�x)�����̓���
            float fVerticalInput = Input.GetAxis("Vertical");

            float newPositionX = fHorizontalInput * mouseSensitivity * Time.deltaTime;
            float newPositionY = fVerticalInput * mouseSensitivity * Time.deltaTime;

            if (Input.touchCount > 0)
            {
                newPositionX = Input.touches[0].deltaPosition.x * touchSensitivity * Time.deltaTime;
                newPositionY = Input.touches[0].deltaPosition.y * touchSensitivity * Time.deltaTime;
            }

            // �ʒu���X�V
            transform.Translate(newPositionX, newPositionY, 0, Space.World);


            // �摜�T�C�Y�̔����̒l
            float imagePositionX = GetComponent<Collider2D>().bounds.extents.x;
            float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;

            // ��ʊO�ɏo�Ȃ��悤�ɒ���
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, CanvasCoordinate.minX + (imagePositionX * imageMarginX), CanvasCoordinate.maxX - (imagePositionX * imageMarginX)),
                Mathf.Clamp(transform.position.y, CanvasCoordinate.minY + (imagePositionY * imageMarginY), CanvasCoordinate.maxY - (imagePositionY * imageMarginY)),
                0
                );
        }

        private void ChangeRotation()
        {
            if (Input.GetKey(KeyCode.RightArrow) || tapManager.GetComponent<Managers.TapManager>().isRightSwipe)
            {
                transform.rotation = Quaternion.Euler(0, 0, -movingRotation);

            }
            else if (Input.GetKey(KeyCode.LeftArrow) || tapManager.GetComponent<Managers.TapManager>().isLeftSwipe)
            {
                transform.rotation = Quaternion.Euler(0, 0, movingRotation);
            }
        }

        private void SetClearPos()
        {
            // 1�x�ڂ̂ݐݒ肷��B
            if(!isSetClearPos)
            {
                isSetClearPos = true;

                // �X�^�[�g�ʒu��ݒ�
                clearStartPos = transform.position;

                // �ړ��ʒu��ݒ�
                float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;
                clearEndPos = new Vector3(CanvasCoordinate.maxX / 2, (imagePositionY * imageMarginY) + CanvasCoordinate.maxY); 
            }
        }

        private void ClearMove()
        {
            // �Q�[���N���A���̈ʒu�A�ړ��ʒu��ݒ�
            SetClearPos();

            // �Q�[���N���A�̏ꍇ�͑���s��
            // �o�ߎ��Ԃ̉��Z
            clearElapsedTime += Time.deltaTime;
            // �����v�Z
            clearLerpRaterate = Mathf.Clamp01(clearElapsedTime / clearMoveSpeed);
            transform.position = Vector3.Lerp(clearStartPos, clearEndPos, clearLerpRaterate);
        }

        private void SetIsEndClearMove(bool flag)
        {
            isEndClearMove = true;
        }
    }
}
