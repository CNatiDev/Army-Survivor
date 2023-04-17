using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TankTurret : MonoBehaviour
{
    public float sphereRadius = 10f; // The radius of the sphere within which the turret can detect targets.
    public Transform gunBarrel; // The transform of the tank's main gun barrel.
    public GameObject tankShellPrefab; // The prefab for the tank's main gun shell.
    public float maxTargetDistance = 100f; // The maximum distance at which the turret can target enemies.

    private Transform target; // The transform of the current target, if any.

    private void Update()
    {
        // Find all enemy gameobjects within range and choose the nearest one as the target.
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);
        GameObject[] enemies = colliders
            .Where(c => c.CompareTag("Enemy"))
            .Select(c => c.gameObject)
            .Where(g => Vector3.Distance(transform.position, g.transform.position) <= maxTargetDistance)
            .ToArray();
        GameObject nearestEnemy = enemies.MinBy(g => Vector3.Distance(transform.position, g.transform.position));

        // If there is a target, aim the gun at it and shoot.
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            Vector3 targetPosition = target.position + Vector3.up * 0.5f; // Aim for the center of the target.
            gunBarrel.LookAt(targetPosition);

            // Shoot after a delay to simulate the tank's firing rate.
            Invoke("Shoot", 1f);
        }
    }

    private void Shoot()
    {
        // Instantiate a tank shell prefab at the gun barrel's position and rotation.
        GameObject shell = Instantiate(tankShellPrefab, gunBarrel.position, gunBarrel.rotation);

        // Get the rigidbody component of the shell and add a force to it.
        Rigidbody rb = shell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(gunBarrel.forward * 5000f); // Adjust the force value as needed.
        }
        Destroy(shell, 1f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

    }
}
