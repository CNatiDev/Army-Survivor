using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGun : MonoBehaviour
{
    private Transform target;
    public Transform gun;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float ShootPower;
    public float AttackRange;
    public float AimRange;
    private bool EnemyInAimRange;
    private bool EnemyInAttackRange;
    public LayerMask whatIsEnemy;
    private void Update()
    {
        EnemyInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsEnemy);
        EnemyInAimRange = Physics.CheckSphere(transform.position, AimRange, whatIsEnemy);
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, whatIsEnemy);
        foreach (Collider collider in colliders)
        {
            Transform targetTransform = collider.gameObject.transform;
            target = targetTransform;
        }
        //Simulate Aim without shooting, for realistic feedback
        if (!EnemyInAttackRange && EnemyInAimRange)
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
        // Shoot at the target
        if (target != null && EnemyInAttackRange && transform.position.z < target.position.z)
        {
            gun.LookAt(target, Vector3.up);
            GameManager.Instance.Target_Solider = target;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }
        gun.transform.rotation = Quaternion.EulerAngles(0, gun.transform.rotation.y, 0);
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(firePoint.forward * ShootPower);
        Destroy(bullet, 3f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AimRange);

    }

    public void Aim()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gun.transform.position.y, target.position.z);
        gun.transform.LookAt(targetPosition, Vector3.up);
    }
}
