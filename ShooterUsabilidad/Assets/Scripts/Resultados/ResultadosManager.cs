using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultadosManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        GameSessionManager.Instance.EndGame();
    }
}
