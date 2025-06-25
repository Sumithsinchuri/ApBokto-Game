using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerAnimation : MonoBehaviour
{
    public static playerAnimation instance;
    public Animator playeranimator;
    public bool playonce;
    
    private Rigidbody player;
    private CapsuleCollider playerCollider;
    float Xdirection;
    public float speed;
    public Vector3 playerVelocity;
    public int rotationspeed;
    bool playergrounded;
    bool hasplayerjump;
    public float jumpForce;
    public Vector3 startpos;
    public Vector3 endpos;
    [SerializeField] private int playerHealth;
    public ScoreManager scoreManager;
    public GameManager gameManager;


  
    public float maxSpeed;
    public float speedIncreaseRate; // How fast to ramp up
    public float scoreThreshold;    // Score at which to start speeding up
    //public float horizontalSpeed = 5f; // Speed for left/right movement
    private bool shouldIncreaseSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        playonce = true;
        player = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerVelocity = new Vector3(player.velocity.x,player.velocity.y,speed);
        playerHealth = 2;
       
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(Vector3.forward * speed * Time.deltaTime);




        //Vector3 horizontalVel = transform.forward * speed;
        //player.velocity = new Vector3(horizontalVel.x, player.velocity.y, horizontalVel.z);

        float input = new Vector2(Xdirection,speed).magnitude;
;
        playeranimator.SetFloat("isrunning",input);
     
        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;   
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endpos = Input.mousePosition;
          
            Swipe();
        }
        if (player.transform.position.y <= -3)
        {
           
            StartCoroutine(fallGameover());
        }


        int score = scoreManager.Score;
        if (score >= scoreThreshold && speed < maxSpeed)
        {
            shouldIncreaseSpeed = true;
        }


        // Gradually increase speed
        if (shouldIncreaseSpeed)
        {
            speed = Mathf.MoveTowards(speed, maxSpeed, speedIncreaseRate * Time.deltaTime);
        }

       
       Vector3 horizontalVel = transform.forward * speed;

        // Move the player forward
        Vector3 moveDirection = new Vector3(horizontalVel.x,0,horizontalVel.z);
        transform.Translate(moveDirection * Time.deltaTime, Space.World);

    }
    public void Swipe()
    {
       
        var xdisp = endpos.x - startpos.x;
        var ydisp = endpos.y - startpos.y;

        if (Mathf.Abs(xdisp) > Mathf.Abs(ydisp))
        {
            if (xdisp >= 0)
            {
                Debug.Log("swipe right");
               transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 90f, 0f);
                

            }
            else
            {
                Debug.Log("swipe left");
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y - 90f, 0f);
            }

        }
        else
        {
            if (ydisp >= 0)
            {
                Debug.Log("jump");
                if (playergrounded)
                {
                    playergrounded = false;
                    playeranimator.SetBool("jump", true);
                    //player.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
                    AudioManager.instance.jumpsound();
                    player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
                }
            }
            else
            {
                Debug.Log("slide");
                playeranimator.SetTrigger("sliding");
                //StartCoroutine(test());
            }
        }


    }
    //IEnumerator test()
    //{
    //    playerCollider.height = 0.5f;
    //    playerCollider.center = new Vector3(0, 0.32f, 0);
    //    yield return new WaitForSeconds(2);
    //    playerCollider.height = 1.6f;
    //    playerCollider.center = new Vector3(0, 1f, 0);
    //}
    IEnumerator playerdead()
    {
        playerHealth -= 1;
        playeranimator.SetTrigger("Slip");
        yield return new WaitForSeconds(5);
        playerHealth = 2;
    }

    IEnumerator fallGameover()
    {
        playeranimator.SetTrigger("fall");
        if (playonce)
        {
            AudioManager.instance.fallingsound();
            playonce = false;
           
        }

        //AudioManager.instance.fallingsound();
        yield return new WaitForSeconds(2);
        gameManager.GameoverUi.SetActive(true);
    }



    IEnumerator gameoverstate()
    {
        speed = 0;
        playeranimator.SetTrigger("Dead");
      
        yield return new WaitForSeconds(2);
        gameManager.GameoverUi.SetActive(true);
    }

    IEnumerator wallhit()
    {
        playeranimator.SetTrigger("wallhit");
        speed = 0;
        yield return new WaitForSeconds(2);
        gameManager.GameoverUi.SetActive(true);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            playergrounded = true;
            playeranimator.SetBool("jump", false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
           
            //playeranimator.SetTrigger("Dead");
            //gameManager.GameoverUi.SetActive(true);
            StartCoroutine(gameoverstate());
        }
        if (collision.gameObject.CompareTag("oil"))
        {
            StartCoroutine(playerdead());
            if (playerHealth <= 0)
            {
              
                // playeranimator.Play("Dead 0");
                playeranimator.SetTrigger("Dead");
                StartCoroutine(gameoverstate());
            }
        }
        if (collision.gameObject.CompareTag("wall"))
        {
          
            StartCoroutine(wallhit());
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        { 
            other.gameObject.SetActive(false);
            scoreManager.playerScoreUpdate(10);
        }
    }
    public void colliderAdjust()
    {
            playerCollider.height = 0.5f;
            playerCollider.center = new Vector3(0, 0.32f, 0);
    }
    public void colliderBackToNormal()
    {
        playerCollider.height = 1.6f;
        playerCollider.center = new Vector3(0, 1f, 0);

    }
    public void collAdjust()
    {
        playerCollider.height = 0.8f;
        playerCollider.center = new Vector3(0, 1.32f, 0);
    }
    public void BackToNormal()
    {
        playerCollider.height = 1.8f;
        playerCollider.center = new Vector3(0, 0.83f, 0);
    }


}
