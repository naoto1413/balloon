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
        public GameObject backGroundUIManager;

        static private GameObject staticGameOverUICanvas;
        static private ParticleSystem staticParticle;

        public GameObject gameOverUICanvas;
        public ParticleSystem particle;

        static private float gameOverCoroutieTime = 0.9f;

        private float totalGameTime;

        private float elapsedTime = 0f;

        public bool isGameClear = false;

        private void Start()
        {
            staticGameOverUICanvas = gameOverUICanvas;
            staticParticle = particle;
            SetTotalGameTime();
        }

        private void Update()
        {
            if (playerManager.GetComponent<Managers.PlayerManager>().isEndClearMove)
            {
                LoadScene("Clear");
            }

            // �o�ߎ��Ԃ̉��Z
            elapsedTime += Time.deltaTime;
            
            if(elapsedTime >= totalGameTime)
            {
                SetIsGameClear(true);
            }
        }

        static public void GameOver(GameObject balloon)
        {
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

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void OnRetryButton()
        {
            LoadScene("Play");
        }

        public void OnBackButton()
        {
            LoadScene("Title");
        }

        private void SetTotalGameTime()
        {
            totalGameTime = backGroundUIManager.GetComponent<BackGroundColorChange>().totalTime;
            Debug.Log(totalGameTime);
        }

        private void SetIsGameClear(bool flag)
        {
            isGameClear = flag;
        }
    }
}
�@
