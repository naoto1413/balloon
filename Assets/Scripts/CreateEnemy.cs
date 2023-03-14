using Common;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public float time;

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
            deltaTime = time;
            int index = Random.Range(0, EnemyPrefabs.Length);
            GameObject prefab = Instantiate(EnemyPrefabs[index], new Vector3(0, maxY / 2, 0), Quaternion.identity);
            updateCreatedPrefab(prefab);
        }
    }

    private void updateCreatedPrefab(GameObject prefab)
    {
        // �p�x��ύX
        prefab.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-30f, 30f));

        // Enemycanvas�ɃZ�b�g
        prefab.transform.SetParent(canvas.transform, false);
    }
}
