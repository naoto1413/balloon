using UnityEngine;
using Common;
using Managers;

namespace Controllers
{
    public class BalloonController : MonoBehaviour
    {
        // 変数宣言
        public float mouseSensitivity = 100;
        public float touchSensitivity = 100;

        public GameObject canvas;

        public float imageMarginX = 1f;
        public float imageMarginY = 2f;

        void Start()
        {
            CanvasCoordinate.setCanvas(canvas);
        }

        // Update is called once per frame
        void Update()
        {
            // メソッドを呼び出す
            ChangeRotation();
            MoveSwipe();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlaySceneManager.gameOver(GameObject.Find("Balloon"));
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

        void ChangeRotation()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, -10);

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 10);
            }
        }
    }
}
