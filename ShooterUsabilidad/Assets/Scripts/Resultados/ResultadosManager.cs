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
    [Range(0,30)]
    public int pruebaPrecision = 0;
    [Range(0,30)]
    public int pruebaApuntado = 0;
    [Range(0,15)]
    public int pruebaReaccion = 0;
    [Range(0,25)]
    public int pruebaTracking = 0;

    const int maxPrecision = 30;
    const int maxApuntado = 30;
    const int maxReaccion = 15;
    const int maxTracking = 25;

    private void OnValidate()
    {
        if (pruebaPrecision > maxPrecision)
            pruebaPrecision = maxPrecision;
        if (pruebaApuntado > maxApuntado)
            pruebaApuntado = maxApuntado;
        if (pruebaReaccion > maxReaccion)
            pruebaReaccion = maxReaccion;
        if (pruebaTracking > maxTracking)
            pruebaTracking = maxTracking;

    }
    // Start is called before the first frame update
    void Start()
    {
        if (maxApuntado + maxPrecision + maxReaccion + maxTracking > 100)
            print("oye que te pasas de 100 locuelo");

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
        precisionText.text = "/"+maxPrecision;
        tiempoApuntadoText.text = "/"+maxApuntado;
        velReaccionText.text = "/"+maxReaccion;
        trackingText.text = "/"+maxTracking;
        notaText.text = "/ 100";
        CurrPrecision.transform.localScale = new Vector3(0, 1, 1);
        CurrApuntado.transform.localScale = new Vector3(0, 1, 1);
        CurrReaccion.transform.localScale = new Vector3(0, 1, 1);
        CurrTracking.transform.localScale = new Vector3(0, 1, 1);
        CurrNota.transform.localScale = new Vector3(0, 1, 1);

        StartCoroutine("setProgressBar");
    }

    //alarga las barras segun el score recibido
    public IEnumerator setProgressBar()
    {
        while (CurrPrecision.transform.localScale.x < ((float)pruebaPrecision/(float)maxPrecision) )
        {
            Vector3 aux = CurrPrecision.transform.localScale;
            CurrPrecision.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            precisionText.text = (int)(CurrPrecision.transform.localScale.x * (float)maxPrecision) + "/" + maxPrecision;
            yield return new WaitForSeconds(0.01f);
        }
        //precisionText.text = pruebaPrecision + "/" + maxPrecision;

        while (CurrApuntado.transform.localScale.x < ((float)pruebaApuntado/(float)maxApuntado) )
        {
            Vector3 aux = CurrApuntado.transform.localScale;
            CurrApuntado.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            tiempoApuntadoText.text = (int)(CurrApuntado.transform.localScale.x * (float)maxApuntado) + "/" + maxApuntado;
            yield return new WaitForSeconds(0.01f);
        }
        //tiempoApuntadoText.text = pruebaApuntado + "/30";

        while (CurrReaccion.transform.localScale.x < ((float)pruebaReaccion/(float)maxReaccion) )
        {
            Vector3 aux = CurrReaccion.transform.localScale;
            CurrReaccion.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            velReaccionText.text = (int)(CurrReaccion.transform.localScale.x * (float)maxReaccion) + "/" + maxReaccion;
            yield return new WaitForSeconds(0.01f);
        }
        //velReaccionText.text = pruebaReaccion + "/15";

        while (CurrTracking.transform.localScale.x < ((float)pruebaTracking/(float)maxTracking) )
        {
            Vector3 aux = CurrTracking.transform.localScale;
            CurrTracking.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            trackingText.text = (int)(CurrTracking.transform.localScale.x * (float)maxTracking) + "/" + maxTracking;
            yield return new WaitForSeconds(0.01f);
        }
        //trackingText.text = pruebaTracking + "/" + maxTracking;

        int notaFinal = pruebaApuntado + pruebaPrecision + pruebaReaccion + pruebaTracking;
        while (CurrNota.transform.localScale.x < ((float)notaFinal/100.0) )
        {
            Vector3 aux = CurrNota.transform.localScale;
            CurrNota.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            notaText.text = (int)(CurrNota.transform.localScale.x * 100.0f) + "/100";
            yield return new WaitForSeconds(0.01f);
        }
        notaText.text = notaFinal + "/100";
    }

    void Update()
    {
        
    }
    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
