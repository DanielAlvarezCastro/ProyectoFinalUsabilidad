using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProcessing : MonoBehaviour
{
    public float score = 0;
    public virtual void Process(string sessionName) { }

    public virtual float GetScore() { return score; }
}
