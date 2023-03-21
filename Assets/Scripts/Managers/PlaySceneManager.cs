using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject enemyManager;

    static public GameObject staticGameOverUICanvas;
    static public ParticleSystem staticParticle;

    public GameObject gameOverUICanvas;
    public ParticleSystem particle;

    private void Start()
    {
        staticGameOverUICanvas = gameOverUICanvas;
        staticParticle = particle;
    }

    static public void gameOver(GameObject balloon)
    {
        RectTransform rect = balloon.GetComponent<RectTransform>();

        Camera mainCamera = Camera.main;

        //UI座標からスクリーン座標に変換
        Vector3 screenPos = balloon.transform.position;

        ////スクリーン座標→ワールド座標に変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(staticParticle);

        newParticle.transform.localPosition = worldPos;

        // パーティクルを発生させる。
        newParticle.Play();

        // balloonを削除
        Destroy(balloon);

        staticGameOverUICanvas.SetActive(true);
    }
}
