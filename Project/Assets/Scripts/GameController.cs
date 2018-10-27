using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class GameController : MonoBehaviour {

    public float tiempoPartida = 30f;
    public float tiempoExtra;
    public Text timeRemaining;
    public Text countDownText;
    public Text lapsText;
    public GameObject timeUpText;
    public int completedLaps;
    public Button startBtn;
    public int secondsToStart = 3;
    public GameObject[] pickUps;

    private GameObject[] enemies;
    private MonoBehaviour[] enemiesScripts;
    private MonoBehaviour[] player;
    private bool isGameOn;
    private Vector3 playerInitialPos;
    private Quaternion playerInitialRot;
    private Vector3 enemy1InitialPos;
    private Quaternion enemy1InitialRot;
    private Vector3 enemy2InitialPos;
    private Quaternion enemy2InitialRot;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponents<MonoBehaviour>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        isGameOn = false;
    }

    // Use this for initialization
    void Start () {
        timeUpText.SetActive(false);
        startBtn.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(false);

        foreach (GameObject enemy in enemies)
        {
            enemiesScripts = enemy.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in enemiesScripts)
            {
                m.enabled = false;
            }
        }

        //player.enabled = false;
        foreach (MonoBehaviour m in player)
        {
            m.enabled = false;
        }

    }

    public void StartGame()
    {
        startBtn.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(true);

        InvokeRepeating("CountDown", 1, 1);

    }

    void GameStarted()
    {
        countDownText.gameObject.SetActive(false);

        isGameOn = true;

        foreach (MonoBehaviour m in player)
        {
            m.enabled = true;
        }

        foreach (GameObject enemy in enemies)
        {
            enemiesScripts = enemy.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in enemiesScripts)
            {
                m.enabled = true;
            }
        }
    }

    private void CountDown()
    {
        secondsToStart--;
        if (secondsToStart <= 0)
        {
            CancelInvoke();
            GameStarted();

        }
        else
        {
            countDownText.text = secondsToStart.ToString();
        }
    }


    // Update is called once per frame
    void Update () {

        if (tiempoPartida > 0 && isGameOn)
        {
            tiempoPartida -= 1 * Time.deltaTime;
            timeRemaining.text = tiempoPartida.ToString("##.##" + "s");
        }

        else if (tiempoPartida <= 0)
        {
            GameOver();
        }

        lapsText.text = completedLaps.ToString();
        
	}

    void GameOver()
    {
        isGameOn = false;

        foreach (MonoBehaviour m in player)
        {
            m.enabled = false;
        }

        foreach (GameObject enemy in enemies)
        {
            enemiesScripts = enemy.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in enemiesScripts)
            {
                m.enabled = false;
            }
        }

        timeRemaining.text = "00.00s";
        timeUpText.SetActive(true);
        Invoke("ReloadScene", 2);

    }

    void ReloadScene()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        SceneFader.instance.LoadLevel(currentLevel);
    }

    public void SetPickUpsActive()
    {
        foreach (GameObject pickUp in pickUps)
        {
            pickUp.SetActive(true);
        }
    } 



}// GameController

