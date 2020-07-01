using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;

public class ResultadosManager : MonoBehaviour
{
    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI precisionText;
    public TextMeshProUGUI tiempoApuntadoText;
    public TextMeshProUGUI velReaccionText;
    public TextMeshProUGUI trackingText;
    public TextMeshProUGUI notaText;

    public GameObject CurrPrecision,
                      CurrApuntado,
                      CurrReaccion,
                      CurrTracking,
                      CurrNota;

    float prueba = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSessionManager.Instance != null)
        {
            nombreText.text = GameSessionManager.Instance.GetPlayerName();
        }
        SetTexts(); 
    }
    //30-30-15-25
    //metodo para ajustar los numeritos cuando los tengamos
    void SetTexts()
    {
        notaText.text = "/ 100";

        Vector3 aux = CurrPrecision.transform.localScale;
        CurrPrecision.transform.localScale = new Vector3(0, aux.y, aux.z);
        StartCoroutine("setProgressBar");
    }

    //alarga las barras segun el score recibido
    public IEnumerator setProgressBar()
    {
        float notaFinal = prueba;
        while (CurrPrecision.transform.localScale.x < prueba)
        {
            Vector3 aux = CurrPrecision.transform.localScale;
            CurrPrecision.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        precisionText.text = (int)(prueba * 30) + "/30";
        prueba = 0.4f;
        notaFinal += prueba;
        while (CurrApuntado.transform.localScale.x < prueba)
        {
            Vector3 aux = CurrApuntado.transform.localScale;
            CurrApuntado.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        tiempoApuntadoText.text = (int)(prueba * 30) + "/30";
        prueba = 0.95f;
        notaFinal += prueba;
        while (CurrReaccion.transform.localScale.x < prueba)
        {
            Vector3 aux = CurrReaccion.transform.localScale;
            CurrReaccion.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        velReaccionText.text = (int)(prueba * 15) + "/15";
        prueba = 0.2f;
        notaFinal += prueba;
        while (CurrTracking.transform.localScale.x < prueba)
        {
            Vector3 aux = CurrTracking.transform.localScale;
            CurrTracking.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        trackingText.text = (int)(prueba * 25) + "/25";
        notaFinal = notaFinal / 4.0f;
        while (CurrNota.transform.localScale.x < notaFinal)
        {
            Vector3 aux = CurrNota.transform.localScale;
            CurrNota.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        notaText.text = (int)(notaFinal * 100) + "/100";
    }

    void Update()
    {
        
    }
    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
