using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class PlaySceneManager : MonoBehaviour
    {
        public GameObject playerManager;
        public GameObject enemyManager;

        static private GameObject staticGameOverUICanvas;
        static private ParticleSystem staticParticle;

        public GameObject gameOverUICanvas;
        public ParticleSystem particle;

        static private float gameOverCoroutieTime = 0.9f;

        private void Start()
        {
            staticGameOverUICanvas = gameOverUICanvas;
            staticParticle = particle;
        }

        static public void GameOver(GameObject balloon)
        {
            RectTransform rect = balloon.GetComponent<RectTransform>();

            Camera mainCamera = Camera.main;

            // UI���W����X�N���[�����W�ɕϊ�
            Vector3 screenPos = balloon.transform.position;

            // �X�N���[�����W���烏�[���h���W�ɕϊ�
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
            ParticleSystem newParticle = Instantiate(staticParticle);

            newParticle.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 10);
            
            // �p�[�e�B�N���𔭐�������B
            newParticle.Play();

            // balloon���폜
            Destroy(balloon);

            GlobalCoroutine.Run(SetActiveCoroutine(gameOverCoroutieTime));
        }

        private static IEnumerator SetActiveCoroutine(float coroutieTime)
        {
            yield return new WaitForSeconds(coroutieTime);
            staticGameOverUICanvas.SetActive(true);
        }

        public void OnRetryButton()
        {
            SceneManager.LoadScene("Play");
        }

        public void OnBackButton()
        {
            SceneManager.LoadScene("Title");
        }
    }
}

