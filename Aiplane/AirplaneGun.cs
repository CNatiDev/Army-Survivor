using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneGun : MonoBehaviour
{
    private Transform target;
    public Transform gun;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float ShootPower;
    public float AttackRange;
    private bool EnemyInAttackRange;
    public LayerMask whatIsEnemy;
    private void Update()
    {
        EnemyInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsEnemy);
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, whatIsEnemy);
        foreach (Collider collider in colliders)
        {
            Transform targetTransform = collider.gameObject.transform;
            target = targetTransform;
        }
        // Aim at the target
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, gun.transform.position.y, target.position.z);
            gun.transform.LookAt(targetPosition,Vector3.forward);
        }
        // Shoot at the target
        if (target != null && EnemyInAttackRange && transform.position.z < target.position.z)
        {
            gun.LookAt(target , Vector3.forward);
            GameManager.Instance.Target_Solider = target;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }
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
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.red;
    }
}