using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public GameObject testSelection;
    public GameObject weaponSelection;
    int selectedTest;
    int selectedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        ShowTestSelection();
    }
    public void UpdateDescription(string txt)
    {
        descriptionText.text = txt;
    }
    public void ShowTestSelection()
    {
        testSelection.SetActive(true);
        weaponSelection.SetActive(false);
    }
    public void ShowWeaponSelection()
    {
        testSelection.SetActive(false);
        weaponSelection.SetActive(true);
    }
    public void SelectTest(int num)
    {
        selectedTest = num;
        ShowWeaponSelection();
    }
    public void SelectWeapon(int num)
    {
        selectedWeapon = num;
        SceneManager.LoadScene(selectedTest);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
