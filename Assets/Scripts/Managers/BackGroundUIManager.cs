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

            // 1���Ŕw�i������ւ��悤�ɃX�s�[�h��ݒ�
            // �㔼���Ɖ�������2�{
            scrollSpeed = CanvasCoordinate.maxY / 60;
        }

        // Update is called once per frame
        void Update()
        {
            // �Q�[���I�[�o�[�̏ꍇ�͔w�i���~
            if (!gameOverUICanvas.activeSelf)
            {
                startBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                middleBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                endBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
            }
        }
    }

}
