using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceManager : MonoBehaviour
{
    public Image[] VehicleIcon;
    public int RandomVehicle;
    public ChangeVehicle change;
    public Canvas Canvas;
    void Start()
    {
        RandomVehicle = Random.Range(0,VehicleIcon.Length);
        change.NewVehicle = RandomVehicle;
        VehicleIcon[RandomVehicle].gameObject.SetActive(true);
        Canvas.worldCamera = FindObjectOfType<Camera>();
    }
    


}
