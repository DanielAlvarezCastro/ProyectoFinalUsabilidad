﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionShootingTest : MonoBehaviour
{
    private ManageObjetives manageObjetives;

    private GameObject actualObjetive;

    //Tiempo que dura la prueba
    public float testDuration;

    //El tiempo que ha pasado desde que la prueba ha empezado
    float actualTestTime = 0.0f;

    //Cuanto se retrocede al fallar un objetivo
    public float missPenalty;

    //Suma 1 por cada pulsacion buena
    private int actualNumObj = 0;

    //Informa de cuando ha empezado la prueba
    private ClickToStart startEvent;

    //Para controlar cuando aparece el primer objetivo
    private bool firstSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        //Guardamos referencia al script para poder hacer spawn cuando se necesite
        manageObjetives = GetComponent<ManageObjetives>();
        startEvent = GetComponent<ClickToStart>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando la prueba ha empezado:
        if (startEvent.IsTestStarted())
        {
            if (!firstSpawn)
            {
                //Se crea el primer objetivo
                actualObjetive = manageObjetives.Spawn();
                firstSpawn = true;
            }

            //Mientras quede tiempo
            if (actualTestTime < testDuration)
            {
                //Se aumenta el tiempo de la prueba y el del objetivo.
                actualTestTime += Time.deltaTime;
               
            }
            //Termina la prueba
            else
            {
                startEvent.endTestText();
                Debug.Log("La prueba ha terminado.");
            }
        }
    }

    /// <summary>
    /// Estos dos métodos tienen que llamarse así en todos los scripts que controlen la prueba para que Target.cs 
    /// sea capaz de llegar a ellos independientemente de como se llame el script. Esto también implica que este script 
    /// tiene estas dentro de un objeto llamado TestManager en la escena.
    /// </summary>
    //Registra que un objetivo ha sido disparado por el jugador
    public void targetDestroyed(Target.TargetInfo info)
    {
        //Suma 1 a las dianas golpeadas
        actualNumObj++;

        //Se coloca una nueva diana en una posicion random y se reinicia su tiempo de desaparicion
        newObjetive(true);

        //Para comprobar la puntuacion
        Debug.Log("Se han acertado: " + actualNumObj);
    }
    //Registra el tiempo de vida de un objetivo se ha terminado 
    public void targetMissed(Target.TargetInfo info)
    {
        //Se coloca una nueva diana en una posicion random y se reinicia su tiempo de desaparicion
        newObjetive(false);

        //Para comprobar la puntuacion
        Debug.Log("Se ha fallado un objetivo");
    }

    //Hace aparecer ua nueva diana y reinicia su tiempo de desaparicion FALTA AUMENTAR SU TAMAÑO SI ES NECESARIO
    void  newObjetive(bool lastTargetHit)
    {
        actualObjetive = manageObjetives.dependientSpawn(actualObjetive.transform.position);
        setTargetDificulty(actualObjetive, lastTargetHit);
    }

    //Método que calibra la dificultad de el objetivo siguiente depende de la prueba a la prueba en la que se encuentre
    void setTargetDificulty(GameObject target, bool lastTargetHit)
    {
        if (!lastTargetHit) actualNumObj -= (int) (actualNumObj*missPenalty);
        target.GetComponent<Target>().deepOffset = transform.position.z * (Mathf.Log10(actualNumObj + 1) / Mathf.Log10(200));
        target.GetComponent<Target>().sizeScale = 0.10f/(Mathf.Log10(actualNumObj+1) / Mathf.Log10(200));
        Debug.Log(transform.position.z * (Mathf.Log10(actualNumObj + 1) / Mathf.Log10(200)));
    }  
}


