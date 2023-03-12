using UnityEngine;

public class BalloonController: MonoBehaviour
{
    // �ϐ��錾
    public float mouseSensitivity = 100;
    public float touchSensitivity = 100;

    public GameObject canvas;

    public float imageMarginX = 1f;
    public float imageMarginY = 2f;

    // Update is called once per frame
    void Update()
    {
        // ���\�b�h���Ăяo��
        ChangeRotation();
        MoveSwipe();
    }

    // �X���C�v���ď㉺���E�ɓ�����
    void MoveSwipe()
    {
        // ��(�])�����̓���
        float fHorizontalInput = Input.GetAxis("Horizontal");
        // �c(�x)�����̓���
        float fVerticalInput = Input.GetAxis("Vertical");
        
        float newPositionX = fHorizontalInput * mouseSensitivity * Time.deltaTime;
        float newPositionY = fVerticalInput * mouseSensitivity * Time.deltaTime;
        
        if (Input.touchCount > 0)
        {
            newPositionX = Input.touches[0].deltaPosition.x * touchSensitivity * Time.deltaTime;
            newPositionY = Input.touches[0].deltaPosition.y * touchSensitivity * Time.deltaTime;
        }

        // �ʒu���X�V
        transform.Translate(newPositionX, newPositionY, 0, Space.World);

        // Canvas�̎l���̍��W���擾
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];

        rectTransform.GetWorldCorners(corners);

        // �������W
        float minX = corners[0][0];
        float minY = corners[0][1];

        // �E����W
        float maxX = corners[2][0];
        float maxY = corners[2][1];


        // �摜�T�C�Y�̔����̒l
        float imagePositionX = GetComponent<Collider2D>().bounds.extents.x;
        float imagePositionY = GetComponent<Collider2D>().bounds.extents.y;

        // ��ʊO�ɏo�Ȃ��悤�ɒ���
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
