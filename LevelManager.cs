using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Level Manager is null");
            }
            return _instance;
        }

    }
    public GameObject[] Level_Text;
    public GameObject[] Level_Button;
    public Generator generator;
    public int LastLevelComplete;
    private void Awake()
    {
        _instance = this;
        Level_Text = GameObject.FindGameObjectsWithTag("Level");
        Level_Button = GameObject.FindGameObjectsWithTag("Button_Level");
        if (PlayerPrefs.HasKey("LastLevel"))
            LastLevelComplete = PlayerPrefs.GetInt("LastLevel") + 1;
    }
    void Start()
    {
        for (int i = 0; i < Level_Text.Length; i++)
        { 
            Level_Text[i].gameObject.GetComponent<TextMeshProUGUI>().text = i.ToString(); 
        }
        for (int i = 0; i < Level_Button.Length; i++)
        {
            int index = i; // Capture the current value of i in a local variable
            Level_Button[i].gameObject.GetComponent<Button>().onClick.AddListener(() =>LoadLevel(index));
        }
        for (int i = 0; i <=LastLevelComplete; i++)
        {
            Level_Button[i].gameObject.GetComponent<Button>().interactable = true;
        }
        for (int i = LastLevelComplete+1; i < Level_Button.Length; i++)
        {
            Level_Button[i].gameObject.GetComponent<Button>().interactable = false;
        }

    }
    public void LoadLevel(int i)
    {
        GameManager.Instance.Current_Level = int.Parse(Level_Text[i].GetComponent<TextMeshProUGUI>().text);
    }
    public void nextLevels()
    {
        int NewLevels = 20;
        for (int i = 0; i < Level_Text.Length; i++)
        { Level_Text[i].gameObject.GetComponent<TextMeshProUGUI>().text = (i+NewLevels).ToString(); }
        for (int i = 0; i < Level_Button.Length; i++)
        {
            int index = i + NewLevels; // Capture the current value of i in a local variable
            Level_Button[i].gameObject.GetComponent<Button>().onClick.AddListener(() => LoadLevel(index));
        }
        NewLevels += 20;
    }
    public void perviousLevels()
    {
        for (int i = 0; i < Level_Text.Length; i++)
        { Level_Text[i].gameObject.GetComponent<TextMeshProUGUI>().text = (i).ToString(); }
        for (int i = 0; i < Level_Button.Length; i++)
        {
            int index = i; // Capture the current value of i in a local variable
            Level_Button[i].gameObject.GetComponent<Button>().onClick.AddListener(() => LoadLevel(index));
        }
    }
}
