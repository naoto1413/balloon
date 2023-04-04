using UnityEngine;
using Common;
using Managers;

namespace Controllers
{
    public class BalloonController : MonoBehaviour
    {
        public GameObject playerManager;
        public GameObject tapManager;


        // 変数宣言
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

                // 移動が完了したらフラグを変更する。
                if (transform.position.y >= clearEndPos.y)
                {
                    SetIsEndClearMove(true);
                }

            }
            else
            {
                // ゲームクリア以外の場合は操作できる
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

        // スワイプして上下左右に動かす
        void MoveSwipe()
        {
            // 横(Ⅹ)方向の入力
            float fHorizontalInput = Input.GetAxis("Horizontal");
            // 縦(Ｙ)方向の入力
            float fVerticalInput = Input.GetAxis("Vertical");

            float newPositionX = fHorizontalInput * mouseSensitivity * Time.deltaTime;
            float newPositionY = fVerticalInput * mouseSensitivity * Time.deltaTime;

            if (Input.touchCount > 0)
            {
                newPositionX = Input.touches[0].deltaPosition.x * touchSensitivity * Time.deltaTime;
                newPositionY = Input.touches[0].deltaPosition.y * touchSensitivity * Time.deltaTime;
            }

            // 位置を更新
            transform.Translate(newPositionX, newPositionY, 0, Space.World);


            // 画像サイズの半分の値
            float imagePositionX = GetComponent<Collider2D>().bounds.extents.x;
            float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;

            // 画面外に出ないように調整
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
            // 1度目のみ設定する。
            if(!isSetClearPos)
            {
                isSetClearPos = true;

                // スタート位置を設定
                clearStartPos = transform.position;

                // 移動位置を設定
                float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;
                clearEndPos = new Vector3(CanvasCoordinate.maxX / 2, (imagePositionY * imageMarginY) + CanvasCoordinate.maxY); 
            }
        }

        private void ClearMove()
        {
            // ゲームクリア時の位置、移動位置を設定
            SetClearPos();

            // ゲームクリアの場合は操作不可
            // 経過時間の加算
            clearElapsedTime += Time.deltaTime;
            // 割合計算
            clearLerpRaterate = Mathf.Clamp01(clearElapsedTime / clearMoveSpeed);
            transform.position = Vector3.Lerp(clearStartPos, clearEndPos, clearLerpRaterate);
        }

        private void SetIsEndClearMove(bool flag)
        {
            isEndClearMove = true;
        }
    }
}
