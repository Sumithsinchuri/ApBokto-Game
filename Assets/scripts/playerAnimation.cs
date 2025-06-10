using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerAnimation : MonoBehaviour
{
    public Animator playeranimator;
    
    private Rigidbody player;
    float Xdirection;
    public int speed;
    public int rotationspeed;
    bool playergrounded;
    bool hasplayerjump;
    public int jumpForce;
    private Vector3 moveDirection = Vector3.forward;
    public Vector3 startpos;
    public Vector3 endpos;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Xdirection = Input.GetAxis("Horizontal");

        player.velocity = moveDirection * speed;

        float input = moveDirection.magnitude;
;
        playeranimator.SetFloat("isrunning",input);
     

        var lookDirection = moveDirection.normalized;
        if (lookDirection.magnitude >= 0.1f)
        {
            var targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
        }
        //if (Input.GetKeyDown(KeyCode.Space) && playergrounded)
        //{
        //    playergrounded = false;
        //    playeranimator.SetBool("jump", true);
        //    var jump = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
        //    player.AddForce(jump, ForceMode.Impulse);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;   
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endpos = Input.mousePosition;
            Swipe();
        }
       
    }
    public void Swipe()
    {
        var xdisp = endpos.x - startpos.x;
        var ydisp = endpos.y - startpos.y;

        if (Mathf.Abs(xdisp) > Mathf.Abs(ydisp))
        {
            if (endpos.x - startpos.x > 0)
            {
                Debug.Log("swipe right");
                player.transform.eulerAngles = new Vector3(0, 90, 0);
                moveDirection = Quaternion.Euler(0f,90f,0f) * moveDirection;
            }
            else
            {
                Debug.Log("swipe left");
                player.transform.eulerAngles = new Vector3(0, -90, 0);
                moveDirection = Quaternion.Euler(0f, -90f, 0f) * moveDirection;

            }

        }
        else
        {
            if (endpos.y - startpos.y > 0)
            {
                Debug.Log("jump");
                if (playergrounded)
                {
                    playergrounded = false;
                    playeranimator.SetBool("jump", true);
                    player.velocity = new Vector3(player.velocity.x, 0f, player.velocity.z);
                    var jump = Vector3.up * jumpForce;
                    player.AddForce(jump, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.Log("slide");
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            //hasplayerjump = true;
            playergrounded = true;
            playeranimator.SetBool("jump", false);
        }
        
    }
   
    
}
