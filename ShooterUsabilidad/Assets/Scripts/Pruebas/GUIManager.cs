using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GUIManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelResultados;
    public TextMeshProUGUI notaText;
    float notaNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetNota(float num)
    {
        notaNum = num;
    }
    public void ShowStartPanel(bool b)
    {
        panelInicio.SetActive(b);
    }
    public void ShowResultsPanel(bool b)
    {
        panelResultados.SetActive(b);
        if (b)
        {
            notaText.text = "Nota " + notaNum.ToString();
        }
    }
    public void StartTest()
    {
        ShowStartPanel(false);
    }
    public void EndTest()
    {
        ShowResultsPanel(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (panelInicio.activeInHierarchy)
        {
            Time.timeScale = 0;
            if (Input.GetMouseButtonDown(0))
            {
                ShowStartPanel(false);
            }
        }
        else if (panelResultados.activeInHierarchy)
        {
            Time.timeScale = 0;
            if (Input.GetMouseButtonDown(0))
            {
                GameSessionManager.Instance.GoToNextScene();
            }
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
