using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float dmg = 0;
    public float timeToDestroy = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyItself", timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void destroyItself()
    {
        Destroy(gameObject);
    }
    public void setDamage(float dam)
    {
        dmg = dam;
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy en = other.GetComponent<Enemy>();
        if (en != null)
        {
            //Calculos de daño
            en.getDamage(dmg/Mathf.Max((other.transform.position-transform.position).magnitude-1, 1));
        }
    }
}
