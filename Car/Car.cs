using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Car : MonoBehaviour
{
    #region Vehicle Control
    public float SpeedVertical; //The speed with which it moves vertically/forward
    public float SpeedHorizontal; //The speed with which it moves horizontally/left&right
    [HideInInspector]
    public Slider Ui_Controller; //The slider with which you control the direction, it has values between -1 and 1
    #endregion
    #region Wheels Control
    private float currentRotationAngle = 0.0f; //The rotation with which it starts, we use to be able to calculate the deltaRtation
    private float currentRotationAngle_b = 0.0f;
    public float maxRotationAngle = 10f;
    public float maxCarRotationAngle = 20f;//The maximum rotation at which the wheel can turn
    public float SpeedWheels; //The speed at which the wheels rotate
    public Transform[] Wheels; //The transform component of the wheels
    #endregion
    private void Start()
    {
        GameManager.Instance.AssignVehicle();
    }
    void Update()
    {   
        transform.Translate(Vector3.forward * SpeedVertical * Time.deltaTime);//Move the car forward
        transform.Translate(Vector3.right * SpeedHorizontal * Ui_Controller.value * Time.deltaTime);//Move the car sideways
        //Calculate deltaRotation for front wheels and object
        float rotationAngle = Ui_Controller.value * maxRotationAngle;
        float deltaRotation = rotationAngle - currentRotationAngle;
        currentRotationAngle = rotationAngle;
        float Car_rotationAngle = Ui_Controller.value * maxCarRotationAngle;
        float Car_deltaRotation = Car_rotationAngle - currentRotationAngle_b;
        currentRotationAngle_b = Car_rotationAngle;
        // Rotate the object/wheels around the y-axis
        transform.Rotate(Vector3.up * Car_deltaRotation, Space.World);
        Wheels[0].Rotate(Vector3.up * deltaRotation * SpeedHorizontal, Space.World);
        Wheels[1].Rotate(Vector3.up * deltaRotation * SpeedHorizontal, Space.World);
        for (int i=0;i<Wheels.Length;i++)
        {
            Wheels[i].Rotate(Vector3.right*SpeedWheels * Time.deltaTime);
        }
    }
}
