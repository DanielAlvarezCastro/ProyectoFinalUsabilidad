using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public GameObject dianaPeq;
    public GameObject bulletHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitPoint(Vector3 point)
    {
        Vector3 pos = (new Vector3(0, point.y, point.z) - new Vector3(0, transform.position.y, transform.position.z))/transform.lossyScale.x;
        spawnAtSecond(pos);
    }
    void spawnAtSecond(Vector3 pos)
    {
        pos *= dianaPeq.transform.lossyScale.x;
        GameObject aux = Instantiate(bulletHit);
        aux.transform.parent = dianaPeq.transform;
        aux.transform.eulerAngles = new Vector3(0, 0, 90);
        //aux.transform.localPosition = pos;
        aux.transform.position = dianaPeq.transform.position+pos;
    }
}
