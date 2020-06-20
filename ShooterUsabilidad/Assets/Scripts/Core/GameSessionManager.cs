using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager Instance;
    string playerName;
    int selectedWeapon;
    string selectedTest;
    public GameObject[] weapons;
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
    public void SetSelectedTest(string testName)
    {
        selectedTest = testName;
    }
    public string GetPlayerName()
    {
        return playerName;
    }
    public string GetSelectedTest()
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
