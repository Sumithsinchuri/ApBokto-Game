using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gamecomponents;
    public GameObject homecanavas;
    public GameObject GameoverUi;
    public GameObject pauseUi;
    public static bool isRestarting;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isRestarting == true)
        {
            homecanavas.SetActive(false);
            gamecomponents.SetActive(true);
            GameoverUi.SetActive(false);

        }
        else
        {
            homecanavas.SetActive(true);
            gamecomponents.SetActive(false);
            GameoverUi.SetActive(false);
            pauseUi.SetActive(false);

        }

           

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void start()
    {
        homecanavas.SetActive(false);
        gamecomponents.SetActive(true);
    }
    public void home()
    {
        isRestarting = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void restart()
    {
        Time.timeScale = 1.0f;
        isRestarting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void pause()
    {
        pauseUi.SetActive(true);
        Time.timeScale = 0;
    }
    public void resume()
    {
        pauseUi.SetActive(false);
        Time.timeScale = 1f;
    }
}
