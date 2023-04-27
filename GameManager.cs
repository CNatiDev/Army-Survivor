using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }

    }
    private void Awake()
    {
        _instance = this;
        MainVehicle = GameObject.FindGameObjectWithTag("vehicle");
        Current_Level = PlayerPrefs.GetInt("LastLevel") + 1;
    }
    public Slider Ui_Controler;
    public Slider Health_Slider;
    public CinemachineVirtualCamera PlayerCamera;
    public GameObject MainVehicle;
    public GameObject Ground;
    private void Update()
    {
        MainVehicle = GameObject.FindGameObjectWithTag("vehicle");
        AssignVehicle();
        if (MainVehicle != null)
        Ground.transform.position = new Vector3(Ground.transform.position.x, Ground.transform.position.y, MainVehicle.transform.position.z);
        Health_Slider.value = health;
        CheckFinal();
        if (MainVehicle == null)
            Health_Slider.value = 0;
    }
    public void AssignVehicle()
    {
        Tank Tank = FindObjectOfType<Tank>();
        Car Car = FindObjectOfType<Car>();
        ShipMouvment Helicopter = FindObjectOfType<ShipMouvment>();
        Airplane Airplane = FindObjectOfType<Airplane>();
        if (Tank != null)
        {
            Tank.Ui_Controller = Ui_Controler;
            PlayerCamera.Follow = MainVehicle.transform;
        }
         if (Car != null)
        {
            Car.Ui_Controller = Ui_Controler;
            PlayerCamera.Follow = MainVehicle.transform;
        }
         if (Helicopter != null)
        {
            Helicopter.Ui_Controller = Ui_Controler;
            PlayerCamera.Follow = MainVehicle.transform;
        }
        if (Airplane != null)
        {
            Airplane.Ui_Controller = Ui_Controler;
            PlayerCamera.Follow = MainVehicle.transform;
        }

    }
    #region Level
    public int Current_Level = 0;
    public int ChoiceNumber = 0;
    public int health;
    [HideInInspector]
    public bool LevelFinish = false;
    public Transform Target_Solider;
    void CheckFinal()
    {
        if (ChoiceNumber == Current_Level)
        {
            LevelFinish = true;
        }
    }
    #endregion
    public UnityEvent FinalEvent;
    public void LoadFinalEvent()
    {
        Invoke("LoadFinal", 1);
    }
    public void LoadFinal()
    {
        FinalEvent.Invoke();
    }
    public void LoadMainScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
