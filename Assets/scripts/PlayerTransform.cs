using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public GameObject player;
    public GameObject transformedPlayer;
    [SerializeField] CameraMovement cameraMovement;
    // Start is called before the first frame update


    private void Awake()
    {
        player = GameObject.Find("Player");
        Debug.Log(player);
        transformedPlayer = GameObject.Find("BARRELPlayer");
       // transformedPlayer.SetActive(false);
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        Debug.Log(transformedPlayer);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transformedPlayer.GetComponent<Transform>().position = player.transform.position;
            player.SetActive(false);
            transformedPlayer.SetActive(true);
            cameraMovement.target = transformedPlayer.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
