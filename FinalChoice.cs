using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalChoice : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vehicle")
        {   
            SaveManager.Instance.SaveLastLevel();
            GameManager.Instance.LoadFinalEvent();
            this.gameObject.SetActive(false);
        }
    }
}
