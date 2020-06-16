using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float destroyTime = 2;
    public float speed = 10;
    public bool collides = false;
    public GameObject collideObject;
    float damage = 0;
    //Mirror.NetworkIdentity net;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyObj", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);   
    }

    public void setDamage(float dam)
    {
        damage = dam;
    }
    void destroyObj()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collides)
        {
            GameObject aux = Instantiate(collideObject);
            aux.transform.position = transform.position;
            aux.GetComponent<Explosion>().setDamage(damage);
            Destroy(gameObject);
        }
    }
}
