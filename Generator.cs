using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    #region Generate choice
    public GameObject Choice;
    public GameObject Final_Choice;
    public float nextChoice_Position; //Next choice position
    private GameObject Choice_In_Scene;
    public void SpawnChoice()
    { 
        if (!GameManager.Instance.LevelFinish)
        { 
            Transform Player = GameManager.Instance.MainVehicle.transform;
            Vector3 Position = new Vector3(-1.71f, 10, Player.position.z + nextChoice_Position);
            Choice_In_Scene = Instantiate(Choice, Position, Choice.transform.rotation);
           
        }
    else
        {
            Transform Player = GameManager.Instance.MainVehicle.transform;
            Vector3 Position = new Vector3(-1.71f, 10, Player.position.z + nextChoice_Position);
            Choice_In_Scene = Instantiate(Final_Choice, Position, Choice.transform.rotation);
        }
    }
    #endregion
    #region Soliders
    public GameObject Solider;
    public float PlayerDistance;
    void Update()
    {
        if (GameManager.Instance.MainVehicle != null)
        {
            Transform Player = GameManager.Instance.MainVehicle.transform;
            if (Player != null)
            { transform.position = new Vector3(transform.position.x, transform.position.y, Player.position.z + PlayerDistance); }
        }
    }
    public void SpawnSolider(int Number_of_Soldiers)
    {
        for (int i = 0; i < Number_of_Soldiers; i++)
        {
            float pos_X = -1;
            for (int j = 0; j < 3; j++)
            {
                Vector3 Solider_Position = new Vector3(transform.position.x + pos_X, transform.position.y, transform.position.z);
                Instantiate(Solider, Solider_Position, Solider.transform.rotation);
                pos_X += 0.5f;
            }
        }

    }
    #endregion
}
