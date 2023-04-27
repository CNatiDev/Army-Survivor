using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Transform player;  // The player's transform
    public Transform gun;  // The gun's transform
    public float rotationSpeed = 5f;  // The speed at which the gun rotates
    public float fireRate = 0.5f;  // The time between shots
    public GameObject bulletPrefab;  // The bullet prefab to instantiate

    private float fireTimer;  // The timer to keep track of firing
    private void Start()
    {
        player = GameManager.Instance.MainVehicle.transform;
    }
    private void Update()
    {
        player = GameManager.Instance.MainVehicle.transform;
        // Get the direction towards the player
        Vector3 direction = player.position - gun.position;
        direction.y = 0f;  // Ignore the y-axis to only rotate horizontally

        // Rotate the gun towards the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        gun.rotation = Quaternion.Slerp(gun.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Fire if the timer allows it
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        // Instantiate a bullet at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);

        // Apply a force to the bullet in the direction the gun is facing
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(gun.right * 1000f);
    }
}
