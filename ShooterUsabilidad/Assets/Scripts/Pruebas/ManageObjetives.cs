using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageObjetives : MonoBehaviour
{
    //El objetivo a instanciar
    public GameObject objetivo;

    //Posicion minima y maxima en la que pueden aparecer los objetivos
    public Transform maxTransform;
    public Transform minTransform;
    private Vector3 maxPosition;
    private Vector3 minPosition;


    // Start is called before the first frame update
    void Awake()
    {
        //Espacio entre el que aparecen los objetivos
        maxPosition = maxTransform.position;
        minPosition = minTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
    }

    //Coloca el objetivo argumento en una posicion random entre el max y min dados
    public void RandomGOPosition(GameObject obj)
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        float randomZ = Random.Range(minPosition.z, maxPosition.z);
        Vector3 newPosition = new Vector3(randomX, randomY, randomZ);

        obj.transform.position = newPosition;
    }


    //Spawnea el objetivo entre las posiciones dadas
    public GameObject Spawn()
    {
        Debug.Log(minPosition.x);
        Debug.Log(maxPosition.x);
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        float randomZ = Random.Range(minPosition.z, maxPosition.z);

        //Crea el objeto y lo devuelve para poder llevar su tracking desde otros scripts
        Vector3 newPosition = new Vector3(randomX, randomY, randomZ);
        GameObject newObj = Instantiate(objetivo, newPosition, objetivo.transform.rotation);
        return newObj;
    }
}

