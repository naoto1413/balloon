using Common;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    // �G�𐶐�����b��
    public float createTime;

    public GameObject canvas;

    float maxY;

    float deltaTime;

    private void Start()
    {
        CanvasCoordinate.setCanvas(canvas);

        // ����W
        maxY = CanvasCoordinate.maxY;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime -= Time.deltaTime;
        if (deltaTime <= 0.0f)
        {
            deltaTime = createTime;
            int index = Random.Range(0, EnemyPrefabs.Length);
            GameObject prefab = Instantiate(EnemyPrefabs[index], new Vector3(0, maxY / 2, 0), Quaternion.identity);
            updateCreatedPrefab(prefab);
        }
    }

    private void updateCreatedPrefab(GameObject prefab)
    {
        // Enemycanvas�ɃZ�b�g
        prefab.transform.SetParent(canvas.transform, false);
    }
}
