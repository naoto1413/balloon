using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;

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

        public float destroyBallooncoroutieTime = 0.9f;
        public float setActiveClearUICanvasCoroutieTime = 0.7f;
        public float jumpSpacianCoroutieTim = 0.9f;

        public bool jumpSpacianFlg = false;

        // SE���������̂�h��
        private bool isPlaySuccessSE = false;

        // Start is called before the first frame update
        void Start()
        {
            CanvasCoordinate.setCanvas(uiCanvas);
            setStartPosition();
            TuneBGMVolume(0.5f);
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
                    GlobalCoroutine.Run(DestroyBalloonCoroutine(destroyBallooncoroutieTime, newParticle));
                }
            }

            if (IsEndSpacianJump() && !clearUICanvas.activeSelf)
            {
                GlobalCoroutine.Run(SetActiveClearUICanvas(setActiveClearUICanvasCoroutieTime));
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
            PlayBombSE();
            Destroy(balloon);
            loveLetter.SetActive(true);

            GlobalCoroutine.Run(JumpSpacian(jumpSpacianCoroutieTim));
        }

        private IEnumerator SetActiveClearUICanvas(float coroutieTime)
        {
            yield return new WaitForSeconds(coroutieTime);

            clearUICanvas.SetActive(true);

            StopBGM();

            if (!isPlaySuccessSE)
            {
                PlaySuccessSE();
                isPlaySuccessSE = true;
            }
            
        }

        private IEnumerator JumpSpacian(float coroutieTime)
        {
            yield return new WaitForSeconds(coroutieTime);

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

        public void OnBackButton()
        {
            SceneManager.LoadScene("Title");
        }

        public void PlayButtonSE()
        {
            SoundManager.instance.PlayButtonSE();
        }

        public void PlayBombSE()
        {
            SoundManager.instance.PlayBombSE();
        }

        public void PlayJumpSE()
        {
            SoundManager.instance.PlayJumpSE();
        }

        public void StopBGM()
        {
            SoundManager.instance.StopBGM();
        }

        public void TuneBGMVolume(float volume)
        {
            SoundManager.instance.TuneBGMVolume(volume);
        }

        public void PlaySuccessSE()
        {
            SoundManager.instance.PlaySuccessSE();
        }
    }
}

