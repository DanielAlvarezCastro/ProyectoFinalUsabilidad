using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager Instance;
    string playerName;
    int selectedWeapon;
    int selectedTest;
    public GameObject[] weapons;
    public string[] sceneNames;
    int currentScene;
    bool completeTest=false;
    float sensibilidad;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetPlayerName(string name)
    {
        playerName = name;
    }
    public void SetSelectedWeapon(int num)
    {
        selectedWeapon = num;
    }
    public void SetSelectedTest(int num)
    {
        selectedTest = num;
    }
    public string GetPlayerName()
    {
        return playerName;
    }
    public int GetSelectedTest()
    {
        return selectedTest;
    }
    public void SetCompleteTest(bool b)
    {
        completeTest = b;
    }
    public bool GetCompleteTest()
    {
        return completeTest;
    }
    public GameObject GetSelectedWeapon()
    {
        return weapons[selectedWeapon];
    }
    public void StartGame()
    {
        if (selectedTest == 0)
        {
            selectedTest = 1;
            SetCompleteTest(true);
        }
        else
        {
            SetCompleteTest(false);
        }
        currentScene = 1;
        SceneManager.LoadScene(sceneNames[1]);
    }
    public void SetSensitivity(float sens)
    {
        sensibilidad = sens;
    }
    public float GetSensitivity()
    {
        return sensibilidad;
    }
    public void GoToNextScene()
    {
        //Si es un test individual al terminar vuelve al main menu
        if (!completeTest)
        {
            SceneManager.LoadScene(sceneNames[0]);
        }
        else
        {
            currentScene++;
            if (currentScene >= sceneNames.Length) currentScene = 0;
            SceneManager.LoadScene(sceneNames[currentScene]);
        }
    }
    public void StartTest()
    {
        SceneManager.LoadScene(sceneNames[selectedTest+1]);
    }
    public void EndGame()
    {
        selectedWeapon = 0;
        selectedTest = 0;
        completeTest = false;
        currentScene = 0;
        SceneManager.LoadScene(sceneNames[0]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
