using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Common;

namespace Managers
{
    public class BackGroundUIManager : MonoBehaviour
    {
        public GameObject startBackGroundImage; 
        public GameObject middleBackGroundImage; 
        public GameObject endBackGroundImage;

        public GameObject backGroundCanvas;

        private float scrollSpeed = 0;

        public GameObject gameOverUICanvas;

        // Start is called before the first frame update
        void Start()
        {
            CanvasCoordinate.setCanvas(backGroundCanvas);

            Vector3 imageSizeDelta = startBackGroundImage.GetComponent<RectTransform>().sizeDelta;

            // 1分で背景が入れ替わるようにスピードを設定
            // 上半分と下半分で2倍
            scrollSpeed = CanvasCoordinate.maxY / 60;
        }

        // Update is called once per frame
        void Update()
        {
            // ゲームオーバーの場合は背景を停止
            if (!gameOverUICanvas.activeSelf)
            {
                startBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                middleBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                endBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
            }
        }
    }

}
