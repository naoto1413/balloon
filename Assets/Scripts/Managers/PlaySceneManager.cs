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

        private void Start()
        {
            staticGameOverUICanvas = gameOverUICanvas;
            staticParticle = particle;
            SetTotalGameTime();
        }

        static public void GameOver(GameObject balloon)
        {
            // UI座標からスクリーン座標に変換
            Vector3 screenPos = balloon.transform.position;

            // スクリーン座標からワールド座標に変換
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(staticParticle);

            newParticle.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 10);
            
            // パーティクルを発生させる。
            newParticle.Play();

            // balloonを削除
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

        private void SetTotalGameTime()
        {
            totalGameTime = backGroundUIManager.GetComponent<BackGroundColorChange>().totalTime;
        }
    }
}
　
