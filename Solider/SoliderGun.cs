using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SoliderGun : MonoBehaviour
{
    private Transform target; //Curent vehicle target
    public Transform gun; //Solider gun
    public Transform Solider;
    public float fireRate = 0.5f; //time between last and next fire
    public float nextFire = 0.0f;
    public GameObject bulletPrefab; //Bullet mesh
    public Transform firePoint; //The point from which the bullet leaves
    public float ShootPower; //The power with which the bullet leaves
    public float AttackRange; //The radius in which the player is attacked
    private bool PlayerInAttackRange;
    public LayerMask whatIsPlayer; //The layer attached to the player
    public Animator Solider_Animator; //The soldier's animator, used to play the clip we need
    public float AimRange; //The radius in which the player is chased
    private bool PlayerInAimRange;
    public NavMeshAgent agent; //AI solider component
    private void Update()
    {   //Check in which area the player is located
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);
        PlayerInAimRange = Physics.CheckSphere(transform.position, AimRange, whatIsPlayer);
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, whatIsPlayer);
        foreach (Collider collider in colliders)
        {
            Transform targetTransform = collider.gameObject.transform;
            target = targetTransform;
        }
        //Simulate Aim and Chase without shooting, for realistic feedback
        if (!PlayerInAttackRange && PlayerInAimRange)
        {
            Collider[] colliders_Aim = Physics.OverlapSphere(transform.position, AimRange, whatIsPlayer);
            foreach (Collider collider in colliders_Aim)
            {
                Transform targetTransform = collider.gameObject.transform;
                target = targetTransform;
            }
            Aim();
            ChasePlayer(target);
        }
        // Aim at the target
        if (target != null)
        {
            Aim();
        }
        // Shoot at the target
        if (target != null && PlayerInAttackRange)
        {
            Shoot();
        }
        if (!PlayerInAimRange && !PlayerInAttackRange)
        {
            Solider_Animator.Play("demo_combat_idle");
        }
        if(GameManager.Instance.MainVehicle!=null)
        if (GameManager.Instance.MainVehicle.transform.position.z>transform.position.z+10)
        {
            Destroy(this.gameObject);
        }
    }
    private void Fire()
    {
        Solider_Animator.Play("demo_combat_shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(firePoint.forward * ShootPower);
        Destroy(bullet, 2f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AimRange);

    }
    // Aim at the target
    public void Aim()
    {
    Vector3 targetPosition = new Vector3(target.position.x, gun.transform.position.y, target.position.z);
    gun.transform.LookAt(targetPosition);
    Solider.transform.LookAt(targetPosition, Vector3.up);
    }
    // Shoot at the target
    public void Shoot()
    {
        gun.LookAt(target);
        Solider.transform.LookAt(target, Vector3.up);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();

        }
    }
    private void ChasePlayer(Transform Player)
    {
        agent.SetDestination(Player.position);
        Solider_Animator.Play("demo_combat_run");
    }
   
}
