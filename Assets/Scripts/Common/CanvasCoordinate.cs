using UnityEngine;

namespace Common
{
    public class CanvasCoordinate
    {
        // 左下座標
        static public float minX;
        static public float minY;

        // 右上座標
        static public float maxX;
        static public float maxY;

        // Start is called before the first frame update
        static public void setCanvas(GameObject canvas)
        {
            // Canvasの四隅の座標を取得
            RectTransform rectTransform = canvas.GetComponent<RectTransform>();

            Vector3[] corners = new Vector3[4];

            rectTransform.GetWorldCorners(corners);

            // 左下座標
            minX = corners[0][0];
            minY = corners[0][1];

            // 右上座標
            maxX = corners[2][0];
            maxY = corners[2][1];
        }
    }

}
