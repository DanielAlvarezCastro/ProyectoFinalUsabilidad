using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SensibilidadManager : MonoBehaviour
{
    public Slider sensibilidadSlider;
    public GameObject sensibilidadPanel;
    public TextMeshProUGUI sensibilidadText;
    public Aim aimScript;
    public float maxSensibilidad;
    float sensibilidad;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        sensibilidad = sensibilidadSlider.value * maxSensibilidad;
    }

    public void ShowPanel()
    {
        Cursor.visible = true;
        sensibilidadPanel.SetActive(true);
    }
    public void HidePanel()
    {
        UpdateSensibilidad();
        Cursor.visible = false;
        sensibilidadPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        Cursor.visible = false;
        GameSessionManager.Instance.SetSensitivity(sensibilidad);
        GameSessionManager.Instance.StartTest();
    }
    public void UpdateSensibilidad()
    {
        sensibilidad = sensibilidadSlider.value * maxSensibilidad;
        aimScript.SetSensitivity(sensibilidad);
    }
    // Update is called once per frame
    void Update()
    {
        sensibilidadText.text = (Mathf.Round(sensibilidadSlider.value * 100f) / 100f).ToString();
        if (sensibilidadPanel.activeInHierarchy)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPanel();
        }
    }
}
