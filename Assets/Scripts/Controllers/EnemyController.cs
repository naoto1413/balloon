using UnityEngine;
using Common;

public class EnemyController: MonoBehaviour
{
    // スピード設定の範囲
    public int speedMin = 200;
    public int speedMax = 300;
    // スピード
    int speed;

    // 角度設定の範囲
    public int angleMin = 240;
    public int angleMax = 330;
    // 角度変更の設定値
    int changeAngle = 90;
    // 角度
    float angle;

    // 進行方向
    Vector3 direction;

    bool turnRightFlg = true;
    bool turnLeftFlg = true;

    // 画像サイズの半分の値
    float imagePositionX;

    private void Start()
    {
        // 角度をランダムに設定
        angle = Random.Range(angleMin, angleMax);

        // スピードをランダムに設定
        speed = Random.Range(speedMin, speedMax);

        // 画像サイズの半分の値を設定
        imagePositionX = GetComponent<Collider2D>().bounds.extents.x;

        // 進行方向を決定
        ChangeDirection();

        // 画面の端を取得しておく。
        CanvasCoordinate.setCanvas(GameObject.Find("EnemyCanvas"));
    }

    void Update()
    {
        // 位置を更新
        transform.position += direction * speed * Time.deltaTime;


        // 画面の左右の端まで行った場合に反対方向に進む
        if (transform.position.x <= CanvasCoordinate.minX + imagePositionX && turnRightFlg)
        {
            angle = angle + changeAngle;
            // 進行方向を変更
            ChangeDirection();

            turnRightFlg = false;
            turnLeftFlg = true;
        } 
        else if(transform.position.x >= CanvasCoordinate.maxX - imagePositionX && turnLeftFlg)
        {
            angle = angle - changeAngle;

            // 進行方向を変更
            ChangeDirection();

            turnRightFlg = true;
            turnLeftFlg = false;
        }

        if (transform.position.y <= CanvasCoordinate.minY)
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private void ChangeDirection()
    {
        // 角度をラジアンに変換
        float rad = angle * Mathf.Deg2Rad;
        // ラジアンから進行方向を設定
        direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
