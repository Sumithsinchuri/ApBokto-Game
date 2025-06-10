using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public Animator coinAnimator;
  
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            coinAnimator.SetBool("Scale", true);

        }
       if (Input.GetKeyUp(KeyCode.Space))
        {
            coinAnimator.SetBool("Scale", false);
        }
        
    }
}
