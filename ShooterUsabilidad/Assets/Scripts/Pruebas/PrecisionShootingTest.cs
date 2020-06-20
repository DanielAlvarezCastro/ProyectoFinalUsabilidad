using System.Collections;
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

    //Tiempo que tarda un objetivo en desaparecer
    public float objetiveTime;
    float actualObjetiveTime = 0.0f;

    //Suma 1 por cada pulsacion buena
    private int actualNumObj = 0;

    //Cuenta las pulsaciones que son erroneas
    private int failClick = 0;

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
                actualObjetiveTime += Time.deltaTime;

                //El probador ha fallado, crea una nueva diana
                if(actualObjetiveTime >= objetiveTime)
                {
                    newObjetive();
                }

                //Diana golpeada con éxito
                //FALTA: COMPROBAR CON EL RAYCAST SI SE HA DADO EN UNA DIANA
                else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
                {
                    //Suma 1 a las dianas golpeadas
                    actualNumObj++;

                    //Se coloca una nueva diana en una posicion random y se reinicia su tiempo de desaparicion
                    newObjetive();

                    //Para comprobar la puntuacion
                    Debug.Log("Se han acertado: " + actualNumObj);
                }
                //FALTA: SI NO HAY RAYCAST Y PULSAS QUE CUENTE EL FALLO
                else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    failClick++;
                    Debug.Log("Numero de fallos: " + failClick);
                }
            }
            //Termina la prueba
            else
            {
                startEvent.endTestText();
                Debug.Log("La prueba ha terminado.");
            }
        }
    }

    //Hace aparecer ua nueva diana y reinicia su tiempo de desaparicion FALTA AUMENTAR SU TAMAÑO SI ES NECESARIO
    void  newObjetive()
    {
        manageObjetives.RandomGOPosition(actualObjetive);
        actualObjetiveTime = 0.0f;
    }
}


