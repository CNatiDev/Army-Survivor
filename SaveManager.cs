using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
public class SaveManager : MonoBehaviour
{    private static SaveManager _instance;
     public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Save Manager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void SaveLastLevel()
    {
        PlayerPrefs.SetInt("LastLevel", GameManager.Instance.Current_Level);
        PlayerPrefs.Save();
    }
}
