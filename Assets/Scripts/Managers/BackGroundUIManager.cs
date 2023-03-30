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

            // 1•ª‚Å”wŒi‚ª“ü‚ê‘Ö‚í‚é‚æ‚¤‚ÉƒXƒs[ƒh‚ğİ’è
            // ã”¼•ª‚Æ‰º”¼•ª‚Å2”{
            scrollSpeed = CanvasCoordinate.maxY / 60;
        }

        // Update is called once per frame
        void Update()
        {
            // ƒQ[ƒ€ƒI[ƒo[‚Ìê‡‚Í”wŒi‚ğ’â~
            if (!gameOverUICanvas.activeSelf)
            {
                startBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                middleBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
                endBackGroundImage.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed);
            }
        }
    }

}
