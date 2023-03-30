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
                // �o�ߎ��Ԃ��߂����Ƃ��̏���
                if (isBalloonMoving)
                {
                    MovingBalloon();
                }
                else
                {
                    isEndGame = true;

                    // UI���W����X�N���[�����W�ɕϊ�
                    Vector3 screenPos = balloon.transform.position;

                    // �X�N���[�����W���烏�[���h���W�ɕϊ�
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

                    // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
                    ParticleSystem newParticle = Instantiate(particle);

                    newParticle.transform.localScale = balloon.transform.localScale;

                    newParticle.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 10);

                    // �p�[�e�B�N���𔭐������āA�o���[�����폜����B
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

            // �o�ߎ��Ԃ̉��Z
            elapsedTime += Time.deltaTime;
            // �����v�Z
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

