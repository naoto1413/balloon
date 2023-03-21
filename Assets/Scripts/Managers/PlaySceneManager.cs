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

        //UI���W����X�N���[�����W�ɕϊ�
        Vector3 screenPos = balloon.transform.position;

        ////�X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(staticParticle);

        newParticle.transform.localPosition = worldPos;

        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();

        // balloon���폜
        Destroy(balloon);

        staticGameOverUICanvas.SetActive(true);
    }
}
