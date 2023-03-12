using UnityEngine;

public class BalloonController: MonoBehaviour
{
    // 変数宣言
    public float mouseSensitivity = 100;
    public float touchSensitivity = 100;

    public GameObject canvas;

    public float imageMarginX = 1f;
    public float imageMarginY = 2f;

    // Update is called once per frame
    void Update()
    {
        // メソッドを呼び出す
        ChangeRotation();
        MoveSwipe();
    }

    // スワイプして上下左右に動かす
    void MoveSwipe()
    {
        // 横(Ⅹ)方向の入力
        float fHorizontalInput = Input.GetAxis("Horizontal");
        // 縦(Ｙ)方向の入力
        float fVerticalInput = Input.GetAxis("Vertical");
        
        float newPositionX = fHorizontalInput * mouseSensitivity * Time.deltaTime;
        float newPositionY = fVerticalInput * mouseSensitivity * Time.deltaTime;
        
        if (Input.touchCount > 0)
        {
            newPositionX = Input.touches[0].deltaPosition.x * touchSensitivity * Time.deltaTime;
            newPositionY = Input.touches[0].deltaPosition.y * touchSensitivity * Time.deltaTime;
        }

        // 位置を更新
        transform.Translate(newPositionX, newPositionY, 0, Space.World);

        // Canvasの四隅の座標を取得
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];

        rectTransform.GetWorldCorners(corners);

        // 左下座標
        float minX = corners[0][0];
        float minY = corners[0][1];

        // 右上座標
        float maxX = corners[2][0];
        float maxY = corners[2][1];


        // 画像サイズの半分の値
        float imagePositionX = GetComponent<Collider2D>().bounds.extents.x;
        float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;

        // 画面外に出ないように調整
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX + (imagePositionX * imageMarginX), maxX - (imagePositionX * imageMarginX)),
            Mathf.Clamp(transform.position.y, minY + (imagePositionY * imageMarginY), maxY - (imagePositionY * imageMarginY)),
            0
            );
    }

    void ChangeRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, -10);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 10);
        }
        
    }
}
