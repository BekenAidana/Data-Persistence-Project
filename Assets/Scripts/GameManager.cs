using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject ErrorInputField; 

    public int bestScore;
    public int currentScore;
    public string bestUserName;
    public string currentUserName;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
    }

    public void StartGame()
    {
        if(String.IsNullOrEmpty(inputField.text))
        {
            ErrorInputField.SetActive(true);
        }
        else
        {
            currentUserName = inputField.text;
            SceneManager.LoadScene(1);
        }
    }
    [System.Serializable]
    class SaveScore
    {
        public string userName;
        public int score;
    }

    public void SaveBestScore()
    {
        SaveScore data = new SaveScore();
        data.userName = currentUserName;
        data.score = currentScore;

        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore data = JsonUtility.FromJson<SaveScore>(json);

            bestUserName = data.userName;
            bestScore = data.score;
        }
        if(String.IsNullOrEmpty(bestUserName))
        {
            bestUserName = "None";
            bestScore = 0;
        }
    }

}
