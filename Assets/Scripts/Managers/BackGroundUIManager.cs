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

            //Debug.Log(imageSizeDelta.y / 2);
            //Debug.Log(CanvasCoordinate.maxY);

            //float posY = imageSizeDelta.y / 2 + CanvasCoordinate.maxY;

            //Vector3 startWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(backGroundCanvas.transform.position.x, posY));
            //Vector3 middleWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(backGroundCanvas.transform.position.x, posY + imageSizeDelta.y));
            //Vector3 endWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(backGroundCanvas.transform.position.x, posY + imageSizeDelta.y * 2));

            //startBackGroundImage.transform.position = new Vector3(startWorldPos.x, startWorldPos.y);
            //middleBackGroundImage.transform.position = new Vector3(middleWorldPos.x, middleWorldPos.y);
            //endBackGroundImage.transform.position = new Vector3(endWorldPos.x, endWorldPos.y);

            //startBackGroundImage.transform.position = new Vector3(startBackGroundImage.transform.position.x, startBackGroundImage.transform.position.y + CanvasCoordinate.minY);
            //middleBackGroundImage.transform.position = new Vector3(middleBackGroundImage.transform.position.x, middleBackGroundImage.transform.position.y + CanvasCoordinate.minY);
            //endBackGroundImage.transform.position = new Vector3(endBackGroundImage.transform.position.x, endBackGroundImage.transform.position.y + CanvasCoordinate.minY);



            // 1ï™Ç≈îwåiÇ™ì¸ÇÍë÷ÇÌÇÈÇÊÇ§Ç…ÉXÉsÅ[ÉhÇê›íË
            // è„îºï™Ç∆â∫îºï™Ç≈2î{
            scrollSpeed = CanvasCoordinate.maxY / 60;
        }

        // Update is called once per frame
        void Update()
        {
            // ÉQÅ[ÉÄÉIÅ[ÉoÅ[ÇÃèÍçáÇÕîwåiÇí‚é~
            if (!gameOverUICanvas.activeSelf)
            {
                startBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                middleBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                endBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
            }
        }
    }

}
