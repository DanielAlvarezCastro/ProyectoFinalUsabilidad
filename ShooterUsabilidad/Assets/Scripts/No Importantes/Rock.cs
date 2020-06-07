using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float rotSpeed = 10;
    public float timeFade = 3;
    Vector3 randVec;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randVec = new Vector3(Random.Range(0f, 1000f), Random.Range(0f, 1000f), Random.Range(0f, 1000f)).normalized;
        Invoke("DestroyObj", timeFade);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(randVec*rotSpeed);
    }
    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
