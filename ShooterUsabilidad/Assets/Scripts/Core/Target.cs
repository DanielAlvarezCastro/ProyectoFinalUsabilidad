using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public struct TargetInfo
    {
        public float size;
        public float LifeTime;
    }
    //Game Manager de la escena
    GameObject testManager;

    //Multiplicador al tamaño de la diana (máximo 1)
    public float sizeScale;
    //Tiempo de vida máximo del objetivo
    public float maxLifeTime;
    //Indica si es un objetivo móvil
    public bool itMoves;

    float remainingLifeTime;
    TargetInfo info;
    // Start is called before the first frame update
    void Start()
    {
        testManager = GameObject.Find("TestManager");
        remainingLifeTime = maxLifeTime;
        info = new TargetInfo();
        info.size = sizeScale;
        info.LifeTime = maxLifeTime;
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
