using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : MonoBehaviour
{
    //Para representación visual, no necesario
    public TextMesh text;

    //Variables de enemigo
    public float life = 100;

    float damageReceived = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //No necesario
        text.text = Mathf.RoundToInt(damageReceived).ToString();
    }

    //Método para recibir daño
    public void getDamage(float dam)
    {
        damageReceived += dam;
        if(damageReceived >= life)
        {
            destroyEnemy();
        }
    }

    //Aquí se destruye el enemigo
    public void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
