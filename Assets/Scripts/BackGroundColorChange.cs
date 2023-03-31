using UnityEngine;
using UnityEngine.UI;

public class BackGroundColorChange : MonoBehaviour
{
    // 背景一つの時間
    public float backGroundPerTime = 60;
    // 背景の経過時間
    private float backGroundLimitTime;
    // 背景の数
    public int backGroundCount = 3;
    // 背景の流れる総時間
    public float totalTime;
    // 背景のインデックス
    private int backGroundIndex = 0;
    
    // HSV関連
    // hueを設定する配列
    public float[] hueList = new float[3];
    private float saturation = 1.0f;
    private float saturation_speed;
    private float value = 1.0f;
    private float value_speed;

    //タイマー稼働フラグ
    private bool counting;
    //1秒を読み取る変数
    private float elapsedtime;

    //背景の色変化用変数
    //背景オブジェクト
    public GameObject imageBack;
    //背景のImageコンポーネント
    private Image imageback;

    public GameObject endImageBack;

    private const int START_INDEX = 0;
    private const float PER_SECOND = 1.0f;

    private const float SATURATION_DEFAULT = 1.0f;
    private const float SATURATION_MIN = 0;
    private const float SATURATION_MAX = 1.0f;
    private const float SATURATION_DECREASE_MAX = 0.9f;

    private const float VALUE_DEFAULT = 1.0f;
    private const float VALUE_MIN = 0;
    private const float VALUE_MAX = 1.0f;
    private const float VALUE_DECREASE_MAX = 0.9f;


    private const int LIMIT_TIME = 0;

    private const int FIRST_SATURATION_SPEED = 1;
    private const int OTHER_SATURATION_SPEED = 2;

    public GameObject gameOverUICanvas;

    private void Awake()
    {
        totalTime = backGroundPerTime * backGroundCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Imageコンポーネント取得
        imageback = imageBack.GetComponent<Image>();


        saturation_speed = SATURATION_DECREASE_MAX / backGroundPerTime;
        value_speed = VALUE_DECREASE_MAX / backGroundPerTime;

        // 経過時間の初期化
        ResetBackGroundLimitTime();
        PushStart();
    }

    // Update is called once per frame
    void Update()
    {
        //HSVの指定
        if (counting)
        {
            TimerCount();
        }
    }

    //タイマー機能
    void TimerCount()
    {
        // 時間の計算
        CalculateTime();
        
        //1秒毎の処理呼び出し
        if (elapsedtime >= PER_SECOND)
        {
            BackColor();
            elapsedtime -= PER_SECOND;
        }

        //時間経過、ゲームオーバー、最後の背景が表示されている場合タイマーの停止
        if (totalTime <= LIMIT_TIME || gameOverUICanvas.activeSelf || !imageBack.activeSelf)
        {
            counting = false;
        }
    }

    //背景のカラーを変化させる
    void BackColor()
    {
        if(backGroundLimitTime >= LIMIT_TIME)
        {
            if (backGroundIndex == START_INDEX)
            {
                CalculateSaturation(false, FIRST_SATURATION_SPEED);
                imageback.color = Color.HSVToRGB(hueList[backGroundIndex], saturation, VALUE_DEFAULT);
            }
            else if (backGroundIndex == hueList.Length - 1)
            {
                // 最後の背景の場合
                if (backGroundLimitTime >= (backGroundPerTime / 2))
                {
                    CalculateValue(false, OTHER_SATURATION_SPEED);
                    imageback.color = Color.HSVToRGB(hueList[backGroundIndex], saturation, value);
                }
                else
                {
                    imageBack.SetActive(false);
                    endImageBack.SetActive(true);
                }
            }
            else
            {
                // 前半の時間でSATURATIONを最大値、後半で最低値へ変化させる。
                if (backGroundLimitTime >= (backGroundPerTime / 2))
                {
                    CalculateSaturation(true, OTHER_SATURATION_SPEED);
                } else
                {
                    CalculateSaturation(false, OTHER_SATURATION_SPEED);
                }
                
                imageback.color = Color.HSVToRGB(hueList[backGroundIndex], saturation, VALUE_DEFAULT);
            }
        } else
        {
            backGroundIndex++;

            ResetBackGroundLimitTime();
        }
    }

    //タイマースタート
    public void PushStart()
    {
        counting = true;
    }

    // 背景の経過時間のリセット
    private void ResetBackGroundLimitTime()
    {
        backGroundLimitTime = backGroundPerTime;
    }

    // 経過時間の加算・減算
    private void CalculateTime()
    {
        //稼働時の経過時間
        float calculateTime = Time.deltaTime;
        totalTime -= calculateTime;
        elapsedtime += calculateTime;
        backGroundLimitTime -= calculateTime;
    }

    private void CalculateSaturation(bool isPlus, int multi)
    {
        if (isPlus)
        {
            saturation = Mathf.Clamp(saturation + (saturation_speed * (backGroundPerTime - backGroundLimitTime) * multi), SATURATION_MIN, SATURATION_MAX);
        } else
        {
            saturation = Mathf.Clamp(SATURATION_DEFAULT - (saturation_speed * (backGroundPerTime - backGroundLimitTime) * multi), SATURATION_MIN, SATURATION_MAX);
        }
    }

    private void CalculateValue(bool isPlus, int multi)
    {
        if (isPlus)
        {
            value = Mathf.Clamp(value + (value_speed * (backGroundPerTime - backGroundLimitTime) * multi), VALUE_MIN, VALUE_MAX);
        }
        else
        {
            value = Mathf.Clamp(VALUE_DEFAULT - (value_speed * (backGroundPerTime - backGroundLimitTime) * multi), VALUE_MIN, VALUE_MAX);
        }
    }
}
