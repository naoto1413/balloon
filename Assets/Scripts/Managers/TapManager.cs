using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class TapManager : MonoBehaviour
    {
        //フリック判定用 時間しきい値
        private float flickTime = 0.15f;
        //フリック判定用 移動距離
        private float flickMagnitude = 100;

        //タップ開始ポイント
        private Vector3 startPosition;
        //タップ終了ポイント
        private Vector3 endPosition;
        // 移動中のポジション
        private Vector3 movePosition;
        // 最後にいたポジション
        private Vector3 lastPosition;

        //フリック判定用 タイマー
        private float timer = 0.0f;

        // フリックの左右判定用のマージン
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
                //タップ開始時
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //タップ開始ポイントを取得
                    startPosition = Input.GetTouch(0).position;
                    lastPosition = Input.GetTouch(0).position;
                }
                //タップ終了時
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //タップ終了ポイントを取得
                    endPosition = Input.GetTouch(0).position;

                    //タップ開始～終了ポイントの距離
                    Vector3 direction = endPosition - startPosition;

                    //距離が指定以上、タップ時間が指定以下の場合、フリックと判定
                    if (direction.magnitude >= flickMagnitude && timer <= flickTime)
                    {
                        SetDirection(direction, MODE_IS_FLICK);
                    }
                    //タイマーを初期化
                    timer = 0.0f;
                }

                //タップ中
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //タップ時間がしきい値を越えた場合、スワイプと判定
                    if (timer >= flickTime)
                    {
                        //Swipe
                        // 移動中のポジションを取得
                        movePosition = Input.GetTouch(0).position;

                        Vector3 direction = movePosition - lastPosition;


                        SetDirection(direction, MODE_IS_SWIPE);

                        // 最後にいたポジションを更新
                        lastPosition = movePosition;
                    }
                    //押下している間、タイマーを加算
                    timer += Time.deltaTime;
                }
            } else
            {
                SetAllDirectionFalse();
            }
        }

        private void SetDirection(Vector3 direction, string mode)
        {
            //x軸の距離が大きい場合は左右への移動
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
            //y軸の距離が大きい場合は上下の移動
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
