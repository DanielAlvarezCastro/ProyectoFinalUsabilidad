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
    public GameObject nameSelection;
    public TMP_InputField nameInput;
    string selectedName;
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
        nameSelection.SetActive(false);
    }
    public void ShowWeaponSelection()
    {
        testSelection.SetActive(false);
        weaponSelection.SetActive(true);
        nameSelection.SetActive(false);
    }
    public void ShowNameSelection()
    {
        testSelection.SetActive(false);
        weaponSelection.SetActive(false);
        nameSelection.SetActive(true);
    }
    public void SelectTest(int num)
    {
        selectedTest = num;
        ShowWeaponSelection();
    }
    public void SelectWeapon(int num)
    {
        selectedWeapon = num;
        ShowNameSelection();
    }
    public void StartGame()
    {
        if(nameInput.text=="")
            nameInput.text = "ANON";
        selectedName = nameInput.text;
        SceneManager.LoadScene(selectedTest);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
