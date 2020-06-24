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
    int currentScene = 0;
    bool completeTest=false;
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
            SetCompleteTest(true);
            SceneManager.LoadScene(sceneNames[1]);
        }
        else
        {
            SceneManager.LoadScene(sceneNames[selectedTest]);
        }
    }
    public void GoToNextScene()
    {
        currentScene++;
        if (currentScene >= sceneNames.Length) currentScene = 0;
        SceneManager.LoadScene(sceneNames[currentScene]);
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
