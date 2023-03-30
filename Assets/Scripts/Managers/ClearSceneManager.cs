using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Managers
{
    public class ClearSceneManager : MonoBehaviour
    {
        public GameObject balloon;
        public GameObject loveLetter;
        public GameObject spacian;
        public GameObject uiCanvas;
        public ParticleSystem particle;
        public GameObject clearUICanvas;

        public float balloonMoveTime;
        private bool isBalloonMoving = true;
        
        private Vector3 ballonStartPos;
        private Vector3 ballonEndPos;

        private float elapsedTime = 0f;
        private float rate;

        private bool isEndGame = false;

        private float coroutieTime = 0.9f;

        public bool jumpSpacianFlg = false;

        // Start is called before the first frame update
        void Start()
        {
            CanvasCoordinate.setCanvas(uiCanvas);
            setStartPosition();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isEndGame)
            {
                // 経過時間を過ぎたときの処理
                if (isBalloonMoving)
                {
                    MovingBalloon();
                }
                else
                {
                    isEndGame = true;

                    // UI座標からスクリーン座標に変換
                    Vector3 screenPos = balloon.transform.position;

                    // スクリーン座標からワールド座標に変換
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                    // パーティクルシステムのインスタンスを生成する。
                    ParticleSystem newParticle = Instantiate(particle);

                    newParticle.transform.localScale = balloon.transform.localScale;

                    newParticle.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 10);

                    // パーティクルを発生させて、バルーンを削除する。
                    GlobalCoroutine.Run(DestroyBalloonCoroutine(coroutieTime, newParticle));
                }
            }

            if (IsEndSpacianJump())
            {
                clearUICanvas.SetActive(true);
            }
        }

        private void setStartPosition()
        {
            Vector3 balloonSizeDelta = balloon.GetComponent<RectTransform>().sizeDelta;

            float ballonStartPosY = CanvasCoordinate.minY - (balloonSizeDelta.y / 2);

            ballonStartPos = new Vector3(balloon.transform.position.x, ballonStartPosY);
            ballonEndPos = new Vector3(balloon.transform.position.x, (CanvasCoordinate.maxY / 2));

            balloon.transform.position = ballonStartPos;
        }

        private void MovingBalloon()
        {
            if (elapsedTime >= balloonMoveTime)
            {
                isBalloonMoving = false;
                return;
            }

            // 経過時間の加算
            elapsedTime += Time.deltaTime;
            // 割合計算
            rate = Mathf.Clamp01(elapsedTime / balloonMoveTime);
            balloon.transform.position = Vector3.Lerp(ballonStartPos, ballonEndPos, rate);
        }

        private IEnumerator DestroyBalloonCoroutine(float coroutieTime, ParticleSystem particle)
        {
            yield return new WaitForSeconds(coroutieTime);

            particle.Play();
            Destroy(balloon);
            loveLetter.SetActive(true);

            JumpSpacian();
        }

        private void JumpSpacian()
        {

            SetJumpSpacianFlg(true);
        }

        private void SetJumpSpacianFlg(bool flag)
        {
            jumpSpacianFlg = flag;
        }

        private bool IsEndSpacianJump()
        {
            return spacian.GetComponent<Controllers.SpacianController>().isEndJumpFlag;
        }
    }
}

