using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerAnimation : MonoBehaviour
{
    public Animator playeranimator;
    
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

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerVelocity = new Vector3(player.velocity.x,player.velocity.y,speed);
        playerHealth = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //player.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 horizontalVel = transform.forward * speed;
        player.velocity = new Vector3(horizontalVel.x, player.velocity.y,horizontalVel.z);

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
        if (player.transform.position.y < -3)
        {
            gameManager.GameoverUi.SetActive(true);
        }
        

    }
    public void Swipe()
    {
        var xdisp = endpos.x - startpos.x;
        var ydisp = endpos.y - startpos.y;

        if (Mathf.Abs(xdisp) > Mathf.Abs(ydisp))
        {
            if (endpos.x - startpos.x >= 0)
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
            if (endpos.y - startpos.y >= 0)
            {
                Debug.Log("jump");
                if (playergrounded)
                {
                    playergrounded = false;
                    playeranimator.SetBool("jump", true);
                    player.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
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
    IEnumerator gameoverstate()
    {
        playeranimator.SetTrigger("Dead");
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
                playeranimator.SetTrigger("Dead");
                StartCoroutine(gameoverstate());
            }
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
