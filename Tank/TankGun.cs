using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour
{//TurreteAim
    private Transform target;
    public Transform gun;
    public Transform Top_gun;
    public float AimRange;
    private bool EnemyInAimRange;
    public LayerMask whatIsEnemy;
    private void Update()
    {
        EnemyInAimRange = Physics.CheckSphere(transform.position, AimRange, whatIsEnemy);

        //Simulate Aim without shooting, for realistic feedback
        if (EnemyInAimRange)
        {
            Collider[] colliders_Aim = Physics.OverlapSphere(transform.position, AimRange, whatIsEnemy);
            foreach (Collider collider in colliders_Aim)
            {
                Transform targetTransform = collider.gameObject.transform;
                target = targetTransform;
            }
            Aim();
        }
        // Aim at the target
        if (target != null)
        {
            Aim();
        }
        gun.transform.rotation = Quaternion.EulerAngles(0, gun.transform.rotation.y, 0);
    }

    public void Aim()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gun.transform.position.y, target.position.z);
        gun.transform.LookAt(targetPosition, Vector3.up);
        Top_gun.LookAt(targetPosition, Vector3.right);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AimRange);

    }
}
