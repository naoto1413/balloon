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
            SoundManager.instance.PlayBGM();
            staticGameOverUICanvas = gameOverUICanvas;
            staticParticle = particle;
            SetTotalGameTime();
            TuneBGMVolume(1.0f);

        }

        private void Update()
        {
            if (playerManager.GetComponent<Managers.PlayerManager>().isEndClearMove)
            {
                LoadScene("Clear");
            }

            // 経過時間の加算
            elapsedTime += Time.deltaTime;
            
            if(elapsedTime >= totalGameTime)
            {
                SetIsGameClear(true);
            }
        }

        static public void GameOver(GameObject balloon)
        {
            StopBGM();

            // UI座標からスクリーン座標に変換
            Vector3 screenPos = balloon.transform.position;

            // スクリーン座標からワールド座標に変換
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(staticParticle);

            newParticle.transform.localPosition = new Vector3(worldPos.x, worldPos.y, 10);
            
            // パーティクルを発生させる。
            newParticle.Play();

            PlayBombSE();

            // balloonを削除
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
        }

        private void SetIsGameClear(bool flag)
        {
            isGameClear = flag;
        }

        public void PlayButtonSE()
        {
            SoundManager.instance.PlayButtonSE();
        }

        public static void PlayBombSE()
        {
            SoundManager.instance.PlayBombSE();
        }

        public static void StopBGM()
        {
            SoundManager.instance.StopBGM();
        }

        public void TuneBGMVolume(float volume)
        {
            SoundManager.instance.TuneBGMVolume(volume);
        }
    }
}
　
