using UnityEngine;
using Common;

public class EnemyController: MonoBehaviour
{
    // �X�s�[�h�ݒ�͈̔�
    public int speedMin = 200;
    public int speedMax = 300;
    // �X�s�[�h
    int speed;

    // �p�x�ݒ�͈̔�
    public int angleMin = 240;
    public int angleMax = 330;
    // �p�x�ύX�̐ݒ�l
    int changeAngle = 90;
    // �p�x
    float angle;

    // �i�s����
    Vector3 direction;

    bool turnRightFlg = true;
    bool turnLeftFlg = true;

    // �摜�T�C�Y�̔����̒l
    float imagePositionX;

    private void Start()
    {
        // �p�x�������_���ɐݒ�
        angle = Random.Range(angleMin, angleMax);

        // �X�s�[�h�������_���ɐݒ�
        speed = Random.Range(speedMin, speedMax);

        // �摜�T�C�Y�̔����̒l��ݒ�
        imagePositionX = GetComponent<Collider2D>().bounds.extents.x;

        // �i�s����������
        ChangeDirection();

        // ��ʂ̒[���擾���Ă����B
        CanvasCoordinate.setCanvas(GameObject.Find("EnemyCanvas"));
    }

    void Update()
    {
        // �ʒu���X�V
        transform.position += direction * speed * Time.deltaTime;


        // ��ʂ̍��E�̒[�܂ōs�����ꍇ�ɔ��Ε����ɐi��
        if (transform.position.x <= CanvasCoordinate.minX + imagePositionX && turnRightFlg)
        {
            angle = angle + changeAngle;
            // �i�s������ύX
            ChangeDirection();

            turnRightFlg = false;
            turnLeftFlg = true;
        } 
        else if(transform.position.x >= CanvasCoordinate.maxX - imagePositionX && turnLeftFlg)
        {
            angle = angle - changeAngle;

            // �i�s������ύX
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
        // �p�x�����W�A���ɕϊ�
        float rad = angle * Mathf.Deg2Rad;
        // ���W�A������i�s������ݒ�
        direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }
}
