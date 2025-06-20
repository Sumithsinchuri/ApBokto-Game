using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRotation : MonoBehaviour
{
    public int Xvalue;
    public int Zvalue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void FixedUpdate()
    {
        transform.Rotate(Xvalue, 0, Zvalue);
    }
}
