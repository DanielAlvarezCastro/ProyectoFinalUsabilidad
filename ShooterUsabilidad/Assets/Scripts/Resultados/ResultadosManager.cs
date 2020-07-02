using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
using System.ComponentModel;
using Unity.Collections;

public class ResultadosManager : MonoBehaviour
{
    //referencia al analysis manager
    AnalysisManager am;

    [Header("Textos de la interfaz")]
    public TextMeshProUGUI nombreText;
    public TextMeshProUGUI precisionText;
    public TextMeshProUGUI tiempoApuntadoText;
    public TextMeshProUGUI velReaccionText;
    public TextMeshProUGUI trackingText;
    public TextMeshProUGUI notaText;

    [Header("Barras de disciplinas")]
    public GameObject CurrPrecision;
    public GameObject CurrApuntado;
    public GameObject CurrReaccion;
    public GameObject CurrTracking;
    public GameObject CurrNota;

    public const int maxPrecision = 30;
    public const int maxApuntado = 30;
    public const int maxReaccion = 15;
    public const int maxTracking = 25;

    [Header("Notas obtenidas")]

    [Range(0, maxPrecision)]
    public int notaPrecision = 0;
    [Range(0, maxApuntado)]
    public int notaApuntado = 0;
    [Range(0, maxReaccion)]
    public int notaReaccion = 0;
    [Range(0, maxTracking)]
    public int notaTracking = 0;

    private int maxFinal = 0;

    void Start()
    {
        GameObject.FindObjectOfType<AnalysisManager>().ponderateStatsAndFinalScore();
        am = GameObject.FindObjectOfType<AnalysisManager>();

        maxFinal = maxApuntado + maxPrecision + maxReaccion + maxTracking;
        if (maxFinal > 100)
            //recordatorio por si el score maximo posible es mayor a 100
            print("man que te pasas de 100 eh");

        if (GameSessionManager.Instance != null)
        {
            nombreText.text = GameSessionManager.Instance.GetPlayerName();
        }
        SetNotes();
    }

    //método para pillar los numeritos del AnalysisManager
    private void SetNotes()
    {
        //coger los numeritos y tal
        //los datos vienen en porcentajes, hay que dividirlos entre 100 y multiplicarlos por el maximo de puntos
        notaPrecision = Math.Min(maxPrecision, (int)((am.mediaPrecision / 100.0) * (float)maxPrecision));
        notaApuntado = Math.Min(maxApuntado, (int)((am.mediaAimTime / 100.0) * (float)maxApuntado));
        notaReaccion = Math.Min(maxReaccion, (int)((am.mediaReactionTime / 100.0) * (float)maxReaccion));
        notaTracking = Math.Min(maxTracking,(int)((am.mediaTracking / 100.0) * (float)maxTracking));
        //por ultimo llamamos al metodo que poner en marcha el resto
        SetTexts();
    }

    private void SetTexts()
    {
        //ajuste inicial de los textos de la interfaz, por si acaso
        precisionText.text = "/" + maxPrecision;
        tiempoApuntadoText.text = "/" + maxApuntado;
        velReaccionText.text = "/" + maxReaccion;
        trackingText.text = "/" + maxTracking;
        notaText.text = "/ 100";
        //aseguramos que las barras estan vacias
        CurrPrecision.transform.localScale = new Vector3(0, 1, 1);
        CurrApuntado.transform.localScale = new Vector3(0, 1, 1);
        CurrReaccion.transform.localScale = new Vector3(0, 1, 1);
        CurrTracking.transform.localScale = new Vector3(0, 1, 1);
        CurrNota.transform.localScale = new Vector3(0, 1, 1);
        //logica de la carga de barras
        StartCoroutine("setProgressBar");
    }

    //alarga las barras segun el score que se tenga
    public IEnumerator setProgressBar()
    {
        //comportamiento de cada una de las barras
        //barra de Precision
        while (CurrPrecision.transform.localScale.x < ((float)notaPrecision / (float)maxPrecision))
        {
            Vector3 aux = CurrPrecision.transform.localScale;
            CurrPrecision.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            precisionText.text = (int)(CurrPrecision.transform.localScale.x * (float)maxPrecision) + "/" + maxPrecision;
            yield return new WaitForSeconds(0.01f);
        }
        //barra de Apuntado
        while (CurrApuntado.transform.localScale.x < ((float)notaApuntado / (float)maxApuntado))
        {
            Vector3 aux = CurrApuntado.transform.localScale;
            CurrApuntado.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            tiempoApuntadoText.text = (int)(CurrApuntado.transform.localScale.x * (float)maxApuntado) + "/" + maxApuntado;
            yield return new WaitForSeconds(0.01f);
        }
        //barra de Reaccion
        while (CurrReaccion.transform.localScale.x < ((float)notaReaccion / (float)maxReaccion))
        {
            Vector3 aux = CurrReaccion.transform.localScale;
            CurrReaccion.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            velReaccionText.text = (int)(CurrReaccion.transform.localScale.x * (float)maxReaccion) + "/" + maxReaccion;
            yield return new WaitForSeconds(0.01f);
        }
        //barra de Tracking
        while (CurrTracking.transform.localScale.x < ((float)notaTracking / (float)maxTracking))
        {
            Vector3 aux = CurrTracking.transform.localScale;
            CurrTracking.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            trackingText.text = (int)(CurrTracking.transform.localScale.x * (float)maxTracking) + "/" + maxTracking;
            yield return new WaitForSeconds(0.01f);
        }
        //calculo de la nota final en funcion de las 4 disciplinas
        int notaFinal = notaApuntado + notaPrecision + notaReaccion + notaTracking;
        //barra de Nota Final
        while (CurrNota.transform.localScale.x < ((float)notaFinal / (float)maxFinal))
        {
            Vector3 aux = CurrNota.transform.localScale;
            CurrNota.transform.localScale = aux + new Vector3(0.01f, 0, 0);
            notaText.text = (int)(CurrNota.transform.localScale.x * 100.0f) + "/" + maxFinal;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
