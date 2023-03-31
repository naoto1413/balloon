using UnityEngine;
using UnityEngine.UI;

public class BackGroundColorChange : MonoBehaviour
{
    // �w�i��̎���
    public float backGroundPerTime = 60;
    // �w�i�̌o�ߎ���
    private float backGroundLimitTime;
    // �w�i�̐�
    public int backGroundCount = 3;
    // �w�i�̗���鑍����
    public float totalTime;
    // �w�i�̃C���f�b�N�X
    private int backGroundIndex = 0;
    
    // HSV�֘A
    // hue��ݒ肷��z��
    public float[] hueList = new float[3];
    private float saturation = 1.0f;
    private float saturation_speed;
    private float value = 1.0f;
    private float value_speed;

    //�^�C�}�[�ғ��t���O
    private bool counting;
    //1�b��ǂݎ��ϐ�
    private float elapsedtime;

    //�w�i�̐F�ω��p�ϐ�
    //�w�i�I�u�W�F�N�g
    public GameObject imageBack;
    //�w�i��Image�R���|�[�l���g
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
        //Image�R���|�[�l���g�擾
        imageback = imageBack.GetComponent<Image>();


        saturation_speed = SATURATION_DECREASE_MAX / backGroundPerTime;
        value_speed = VALUE_DECREASE_MAX / backGroundPerTime;

        // �o�ߎ��Ԃ̏�����
        ResetBackGroundLimitTime();
        PushStart();
    }

    // Update is called once per frame
    void Update()
    {
        //HSV�̎w��
        if (counting)
        {
            TimerCount();
        }
    }

    //�^�C�}�[�@�\
    void TimerCount()
    {
        // ���Ԃ̌v�Z
        CalculateTime();
        
        //1�b���̏����Ăяo��
        if (elapsedtime >= PER_SECOND)
        {
            BackColor();
            elapsedtime -= PER_SECOND;
        }

        //���Ԍo�߁A�Q�[���I�[�o�[�A�Ō�̔w�i���\������Ă���ꍇ�^�C�}�[�̒�~
        if (totalTime <= LIMIT_TIME || gameOverUICanvas.activeSelf || !imageBack.activeSelf)
        {
            counting = false;
        }
    }

    //�w�i�̃J���[��ω�������
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
                // �Ō�̔w�i�̏ꍇ
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
                // �O���̎��Ԃ�SATURATION���ő�l�A�㔼�ōŒ�l�֕ω�������B
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

    //�^�C�}�[�X�^�[�g
    public void PushStart()
    {
        counting = true;
    }

    // �w�i�̌o�ߎ��Ԃ̃��Z�b�g
    private void ResetBackGroundLimitTime()
    {
        backGroundLimitTime = backGroundPerTime;
    }

    // �o�ߎ��Ԃ̉��Z�E���Z
    private void CalculateTime()
    {
        //�ғ����̌o�ߎ���
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
