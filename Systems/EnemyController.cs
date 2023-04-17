using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 50;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("vehicle"))
        {
            Destroy(this.gameObject);
        }
    }
}