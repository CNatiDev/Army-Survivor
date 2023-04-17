using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tank : MonoBehaviour
{
    #region Vehicle Control
    public float SpeedVertical; //The speed with which it moves vertically/forward
    public float SpeedHorizontal; //The speed with which it moves horizontally/left&right
    [HideInInspector]
    public Slider Ui_Controller; //The slider with which you control the direction, it has values between -1 and 1
    #endregion
    #region Wheels Control
    private float currentRotationAngle = 0.0f; //The rotation with which it starts, we use to be able to calculate the deltaRtation
    private float maxRotationAngle = 35f; //The maximum rotation at which the wheel can turn
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
        // Rotate the object/wheels around the y-axis
        transform.Rotate(Vector3.up * deltaRotation, Space.World);
        //transform.localPosition = new Vector3(transform.localPosition.x,0, transform.localPosition.z);
    }
}
