using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexTest : MonoBehaviour
{
    private ManageObjetives manageObjetives;

    private GameObject actualObjetive;

    //Maximo y minimo tiempo en el que puede aparecer un objetivo
    public float minTimeInterval;
    public float maxTimeInterval;

    //Numero de objetivos que hay que destruir para que finalice la prueba
    public int maxNumObj;

    //Suma 1 por cada pulsacion buena
    private int actualNumObj = 0;
    //Para solo contar los clicks cuando hay objetivo en pantalla
    private bool objetiveActive = false;

    //Cuenta las pulsaciones que son erroneas
    private int failClick = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Guardamos referencia al script para poder hacer spawn cuando se necesite
        manageObjetives = GetComponent<ManageObjetives>();

        objetiveActive = true;

        //Se spawnea la primera esfera
        actualObjetive = manageObjetives.Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //Mientras queden objetivos por salir
        if(actualNumObj < maxNumObj)
        {
            //Si hay objetivo en pantalla y se pulsa
            if (objetiveActive & (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                actualNumObj++;
                actualObjetive.SetActive(false);

                if (actualNumObj < maxNumObj) { 
                    objetiveActive = false;
                    RandomTimeSpawn();
             
                    //Para comprobar la puntuacion
                    Debug.Log("Se han acertado: " + actualNumObj);
                }
                //Cuando se han destruido todos los objetivos
                else
                {
                    Debug.Log("La prueba ha terminado.");
                }
            }
            //Si el probador pulsa pero no hay objetivo se le penaliza
            else if(!objetiveActive && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                failClick++;
                Debug.Log("Numero de fallos: " + failClick);
            }
        }
    }

    //Hace que se muestre el objetivo tras un tiempo aleatorio
    void RandomTimeSpawn()
    {
        float newRandomTime = Random.Range(minTimeInterval, maxTimeInterval);
   
        Invoke("NextObjetive", newRandomTime);
    }

    //Activa el objetivo en una nueva posicion
    void NextObjetive()
    {
        objetiveActive = true;
        actualObjetive.SetActive(true);
        manageObjetives.RandomGOPosition(actualObjetive);
    }
}

