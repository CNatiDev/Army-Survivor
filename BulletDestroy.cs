using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public string EnemyTag;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Destroy(this.gameObject);
        }
    }
}
