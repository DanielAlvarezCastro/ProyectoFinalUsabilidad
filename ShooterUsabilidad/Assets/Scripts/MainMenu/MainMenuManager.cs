using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateDescription(string txt)
    {
        descriptionText.text = txt;
    }
    public void GoToScene(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
