using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVehicle : MonoBehaviour
{
    public GameObject[] VehiclePrefab; //Vehicles prefab for spawn
    private GameObject ActiveVehicle; //Curent vehicle who is running
    public GameObject ChoiceNeighbor; //Second choice
    public int NewVehicle; //Vehicle order in array
    private int NumberOfSoliders;
    public void ChangeVehiclePrefab(int Vehicle_Position_In_Array)
    {
            ActiveVehicle = GameManager.Instance.MainVehicle;
            Destroy(ActiveVehicle.gameObject);
            Vector3 position = new Vector3(ActiveVehicle.transform.position.x, VehiclePrefab[Vehicle_Position_In_Array].transform.position.y, ActiveVehicle.transform.position.z);
            Quaternion rotation = Quaternion.EulerAngles(0, 0, 0);
            Instantiate(VehiclePrefab[Vehicle_Position_In_Array], position, rotation);
        #region Spawn next choice
        NumberOfSoliders = 3;
            Generator generator = FindObjectOfType<Generator>();
            generator.SpawnChoice();
            generator.SpawnSolider(NumberOfSoliders);
        NumberOfSoliders++;
            GameManager.Instance.ChoiceNumber++;
            #endregion
            GameManager.Instance.Ui_Controler.value = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vehicle")
        {
            ChangeVehiclePrefab(NewVehicle); //Change vehicle prefab
            this.gameObject.SetActive(false);
            ChoiceNeighbor.SetActive(false);
            Destroy(this.gameObject);
            Destroy(ChoiceNeighbor.gameObject);
        }
    }
}
