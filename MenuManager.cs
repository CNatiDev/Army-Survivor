using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      Pause(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause(bool pause)
    {
        if (pause)
            Time.timeScale = 0f;
        if (!pause)
            Time.timeScale = 1f;
    }
}
