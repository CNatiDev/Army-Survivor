using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            GameManager.Instance.health -= 10;
            Debug.Log("hit");
            if (GameManager.Instance.health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
