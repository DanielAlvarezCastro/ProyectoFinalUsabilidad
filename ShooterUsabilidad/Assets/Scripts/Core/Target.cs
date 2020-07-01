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
        public float speed;
    }
    //Game Manager de la escena
    GameObject testManager;

    /// <summary>
    /// Para la prueba de precisión
    /// </summary>
    //Profundidad a la que aparece el objetivo
    float deepOffset;
    //Multiplicador al tamaño de la diana (máximo 1)
    float sizeScale;

    /// <summary>
    /// Para la prueba de velocidad
    /// </summary>
    //Tiempo de vida máximo del objetivo
    float maxLifeTime;

    float remainingLifeTime;

    protected TargetInfo info;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        testManager = GameObject.Find("TestManager");
        //Rellenamos el struct con la infrmación del objetivo
        info = new TargetInfo();
    }

    public void setTargetInfo(float _sizeScale, float _deepOffset, float _lifeTime)
    {
        sizeScale = _sizeScale;
        info.size = sizeScale;
        gameObject.transform.localScale *= sizeScale;
        deepOffset = _deepOffset;
        info.deepOffset = deepOffset;
        gameObject.transform.position += new Vector3(0, 0, deepOffset);
        maxLifeTime = _lifeTime;
        info.LifeTime = maxLifeTime;
        remainingLifeTime = maxLifeTime;
        info.speed = 1;
    }

    // Update is called once per frame
    protected virtual void Update()
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
