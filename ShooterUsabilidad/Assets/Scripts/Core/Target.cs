using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Struct con ls información de un objetivo
    public struct TargetInfo
    {
        public float deepOffset;
        public float size;
        public float LifeTime;
    }
    //Game Manager de la escena
    GameObject testManager;

    /// <summary>
    /// Para la prueba de precisión
    /// </summary>
    //Profundidad a la que aparece el objetivo
    public float deepOffset;
    //Multiplicador al tamaño de la diana (máximo 1)
    public float sizeScale;

    /// <summary>
    /// Para la prueba de velocidad
    /// </summary>
    //Tiempo de vida máximo del objetivo
    public float maxLifeTime;

    /// <summary>
    /// Para la prueba de objetivos móviles
    /// </summary>
    //Indica si es un objetivo móvil
    public bool itMoves;

    float remainingLifeTime;
    TargetInfo info;
    // Start is called before the first frame update
    void Start()
    {
        testManager = GameObject.Find("TestManager");
        remainingLifeTime = maxLifeTime;
        gameObject.transform.position += new Vector3(0,0,deepOffset);
        gameObject.transform.localScale *= sizeScale;
        //Rellenamos el struct con la infrmación del objetivo
        info = new TargetInfo();
        info.size = sizeScale;
        info.LifeTime = maxLifeTime;
        info.deepOffset = deepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0) TargetLost();
    }

    //Una bala ha alcanzado el objetivo
    public void TargetHit()
    {
        testManager.SendMessage("targetDestroyed", info);
        GameObject.Destroy(gameObject);
    }
    //El tiempoi de vida del objetivo ha terminado
    public void TargetLost()
    {
        testManager.SendMessage("targetMissed", info);
        GameObject.Destroy(gameObject);
    }
}
