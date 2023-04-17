using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipMouvment : MonoBehaviour
{
    #region Controller
    [HideInInspector]
    public Slider Ui_Controller;
    public float Speed_Horizontal;
    public float Speed_Vertical;
    private float currentRotationAngle = 0.0f;
    public float maxRotationAngle = 45f;
    #endregion
    private void Start()
    {
        GameManager.Instance.AssignVehicle();
    }
    void Update()
    {

        transform.Translate(Vector3.forward * Speed_Vertical * Time.deltaTime);
        transform.Translate(Vector3.right * Speed_Horizontal * Ui_Controller.value * Time.deltaTime);
        //Calculate rotation 
        float rotationAngle = Ui_Controller.value * maxRotationAngle;
        float deltaRotation = rotationAngle - currentRotationAngle;
        currentRotationAngle = rotationAngle;
        // Rotate the object around the y-axis
        transform.Rotate(Vector3.up * deltaRotation, Space.World);

    }
}
