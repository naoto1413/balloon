using UnityEngine;

namespace Common
{
    public class CanvasCoordinate
    {
        // �������W
        static public float minX;
        static public float minY;

        // �E����W
        static public float maxX;
        static public float maxY;

        // Start is called before the first frame update
        static public void setCanvas(GameObject canvas)
        {
            // Canvas�̎l���̍��W���擾
            RectTransform rectTransform = canvas.GetComponent<RectTransform>();

            Vector3[] corners = new Vector3[4];

            rectTransform.GetWorldCorners(corners);

            // �������W
            minX = corners[0][0];
            minY = corners[0][1];

            // �E����W
            maxX = corners[2][0];
            maxY = corners[2][1];
        }
    }

}
