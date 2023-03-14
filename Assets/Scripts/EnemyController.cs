using UnityEngine;

public class EnemyController: MonoBehaviour
{
    public int move;

    private void Start()
    {
        move = Random.Range(-300, -100);
    }

    void Update()
    {
        // 位置を更新
        transform.Translate(0, move * Time.deltaTime, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject, 0.2f);
    }
}
