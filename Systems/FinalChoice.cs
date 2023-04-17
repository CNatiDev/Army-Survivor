using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class FinalChoice : MonoBehaviour
{
    public UnityEvent FinalEvent;
    public void LoadFinal()
    {
        FinalEvent.Invoke();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vehicle")
        {
            LoadFinal();
        }
    }
}
