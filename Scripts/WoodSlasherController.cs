using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WoodSlasherController : MonoBehaviour
{
    public static WoodSlasherController instance { get { return _instance; } }
    private static WoodSlasherController _instance;
    private Animator _anim;

    public static int gladiusScore = 20;
    public static int scutumScore = 10;

    public GameObject gladiusPrefab;
    public GameObject scutumPrefab;
    public GameObject woodPrefab;
    [SerializeField] private GameObject[] startObjects;
    [SerializeField] private List<GameObject> SpawnedObjects = new List<GameObject>();
    [SerializeField] private Transform glaSpawnPos;
    [SerializeField] private Transform scuSpawnPos;
    [SerializeField] private GameObject[] woodLogSpawnPos;

    [Header("Audio Settings")]
    private AudioSource _audio;
    public AudioClip countDownAudio;
    public AudioClip startAudio;
    [SerializeField] private AudioClip launchAudio; 

    public int currentScore = 0;
    private float timeLeft = 0f;
    public float gameTime = 180f;

    private bool oneTime = false;
    private bool startGame = false;
    private bool startTimer = false;
    private bool gameOver = false;
    private bool isSpawn = false;
    public bool pressed { get; set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerText2;

    [Header("Game Area")]
    public GameObject defaultArea;
    public GameObject gameArea;

    private void Awake()
    {
        // Singleton for WoodSlasherController
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = gameTime;
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        scoreText.text = "Score: " + currentScore.ToString();
        scoreText2.text = "Score: " + currentScore.ToString();
        timerText.text = "Time Left: " + gameTime;
        timerText2.text = "Time Left: " + gameTime;
        gameArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isSpawn: " + isSpawn);
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            pressed = true;
        }

        // Start the game if pressed is true.
        if (pressed)
        {
            pressed = false;
            StartCoroutine("StartDelay");
            _anim.SetBool("isStart", true);
        }

        if (startGame && !oneTime)
        {
            StopCoroutine("StartDelay");
            Debug.Log("Game Start");
            startTimer = true;
            oneTime = true;
        }

        // Start the timer and update timer text
        if (startTimer && startGame)
        {
            if (timeLeft >= 0)
            {
                scoreText.text = "Score: " + currentScore.ToString();
                scoreText2.text = "Score: " + currentScore.ToString();
                timeLeft -= Time.deltaTime;
                timerText.text = "Time Left: " + Mathf.Round(timeLeft);
                timerText2.text = "Time Left: " + Mathf.Round(timeLeft);
                Debug.Log("Timer is Running");

                // End the game if time has ran out
                if (timeLeft <= 0)
                {
                    Debug.Log("Timer is Stopped");
                    _audio.clip = startAudio;
                    _audio.volume = 0.3f;
                    _audio.Play();
                    timeLeft = 0;
                    timerText.text = "Game Over!";
                    startTimer = false;
                    gameOver = true;
                }
            }
        }

        // Randomly pick a spawn point and launch it through its relative position
        if (isSpawn && startGame && !gameOver && timeLeft >= 0)
        {
            GameObject wood;
            StopCoroutine("SpawnDelay");
            int index = Random.Range(0, woodLogSpawnPos.Length);
            switch (index)
            {
                case 0:
                    _audio.clip = launchAudio;
                    _audio.pitch = Random.Range(0.8f, 1.1f);
                    _audio.volume = Random.Range(0.8f, 1.1f);
                    _audio.Play();
                    wood = Instantiate(woodPrefab, woodLogSpawnPos[index].transform.position, woodLogSpawnPos[index].transform.rotation);
                    wood.GetComponent<Rigidbody>().AddRelativeForce(-transform.forward * Random.Range(6.5f, 8.5f), ForceMode.Impulse);
                    Destroy(wood, 5f);
                    Debug.Log("Spawn Pos: Top");
                    break;
                case 1:
                    _audio.clip = launchAudio;
                    _audio.pitch = Random.Range(0.8f, 1.1f);
                    _audio.volume = Random.Range(0.8f, 1.1f);
                    _audio.Play();
                    wood = Instantiate(woodPrefab, woodLogSpawnPos[index].transform.position, woodLogSpawnPos[index].transform.rotation);
                    wood.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * Random.Range(6.5f, 8.5f), ForceMode.Impulse);
                    Destroy(wood, 5f);
                    Debug.Log("Spawn Pos: Bottom");
                    break;
                case 2:
                    _audio.clip = launchAudio;
                    _audio.pitch = Random.Range(0.8f, 1.1f);
                    _audio.volume = Random.Range(0.8f, 1.1f);
                    _audio.Play();
                    wood = Instantiate(woodPrefab, woodLogSpawnPos[index].transform.position, woodLogSpawnPos[index].transform.rotation);
                    wood.GetComponent<Rigidbody>().AddRelativeForce(transform.right * Random.Range(6.5f, 8.5f), ForceMode.Impulse);
                    Destroy(wood, 5f);
                    Debug.Log("Spawn Pos: Left");
                    break;
                case 3:
                    _audio.clip = launchAudio;
                    _audio.pitch = Random.Range(0.8f, 1.1f);
                    _audio.volume = Random.Range(0.8f, 1.1f);
                    _audio.Play();
                    wood = Instantiate(woodPrefab, woodLogSpawnPos[index].transform.position, woodLogSpawnPos[index].transform.rotation);
                    wood.GetComponent<Rigidbody>().AddRelativeForce(-transform.right * Random.Range(6.5f, 8.5f), ForceMode.Impulse);
                    Destroy(wood, 5f);
                    Debug.Log("Spawn Pos: Right");
                    break;

            }
            isSpawn = false;
        }

        if (!isSpawn && startGame)
        {
            StartCoroutine("SpawnDelay");
        }

        // Call EndGame
        if (gameOver && oneTime)
        {
            StartCoroutine("EndGame");
        }
    }

    IEnumerator StartDelay()
    {
        StartCoroutine("SpawnDelay");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < startObjects.Length; i++)
        {
            startObjects[i].SetActive(false);
        }
        gameArea.SetActive(true);
        defaultArea.SetActive(false);
        GameObject gladius = Instantiate(gladiusPrefab, glaSpawnPos.transform.position, glaSpawnPos.transform.rotation);
        GameObject scutum = Instantiate(scutumPrefab, scuSpawnPos.transform.position, scuSpawnPos.transform.rotation);
        gladius.gameObject.tag = "Gladius";
        scutum.gameObject.tag = "Scutum";
        SpawnedObjects.Add(gladius);
        SpawnedObjects.Add(scutum);
        pressed = false;
        yield return new WaitForSeconds(5f);
        startGame = true;
    }

    // End game procedure once it was called
    IEnumerator EndGame()
    {
        Debug.Log("Game Over!");
        StopCoroutine("SpawnDelay");
        isSpawn = false;
        yield return new WaitForSeconds(5f);
        _anim.SetBool("isStart", false);
        yield return new WaitForSeconds(5f);
        foreach (GameObject obj in SpawnedObjects)
        {
            Destroy(obj);
        }
        currentScore = 0;
        timeLeft = gameTime;
        scoreText.text = "Score: " + currentScore.ToString();
        scoreText2.text = "Score: " + currentScore.ToString();
        timerText.text = "Time Left: " + gameTime;
        timerText2.text = "Time Left: " + gameTime;
        SpawnedObjects.Clear();
        for (int i = 0; i < startObjects.Length; i++)
        {
            startObjects[i].SetActive(true);
        }
        startGame = false;
        oneTime = false;
        gameOver = false;
        gameArea.SetActive(false);
        defaultArea.SetActive(true);
    }

    // Pick a random spawn point for wood log 
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(5f);
        isSpawn = true;
    }

    public void PlayCountDownAudio()
    {
        _audio.clip = countDownAudio;
        _audio.volume = 1f;
        _audio.Play();
    }

    public void PlayStartAudio()
    {
        _audio.clip = startAudio;
        _audio.volume = 0.3f;
        _audio.Play();
    }
}
