using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PilumThrowerController : MonoBehaviour
{
    public static PilumThrowerController instance { get { return _instance; } }
    private static PilumThrowerController _instance;
    private Animator _anim;

    // Bullseye score
    public static int highTargetScore = 20;
    public static int midTargetScore = 10;
    public static int lowTargetScore = 5;

    [SerializeField] private GameObject[] startObjects;
    [SerializeField] private GameObject infoPanel;
    public GameObject pilumPrefab;
    [SerializeField] private List<GameObject> Pilums = new List<GameObject>();
    [SerializeField] private Transform[] pilumSpawnPos;

    [Header("Audio Settings")]
    private AudioSource _audio;
    public AudioClip countDownAudio;
    public AudioClip startAudio;

    public int currentScore = 0;
    public int remainingPilums = 0;

    private bool oneTime = false;
    private bool startGame = false;
    public bool pressed { get; set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;

    [Header("Game Area")]
    public GameObject defaultArea;
    public GameObject gameArea;

    private void Awake()
    {
        // Singleton for PilumThrowerController
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
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        DisableChild();
        scoreText.text = currentScore.ToString();
        infoPanel.SetActive(false);
        gameArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Remaining: " + remainingPilums);
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            pressed = true;
        }
        
        // Start the game if pressed is true.
        if (pressed)
        {
            StartCoroutine("StartDelay");
            pressed = false;
        }

        if (startGame && !oneTime)
        {
            // If startGame is true and oneTime is false, do the following things 
            Debug.Log("GameStart");
            oneTime = true;
            _anim.SetBool("isStart", true);
            gameArea.SetActive(true);
            defaultArea.SetActive(false);
            remainingPilums = 8;
            for(int i = 0; i < startObjects.Length; i++)
            {
                startObjects[i].SetActive(false);
            }

            for (int i = 0; i < pilumSpawnPos.Length; i++)
            {
                GameObject pilum = Instantiate(pilumPrefab, pilumSpawnPos[i].transform.position, pilumSpawnPos[i].transform.rotation);
                Pilums.Add(pilum);
                remainingPilums++;
            }
            infoPanel.SetActive(true);
            if (remainingPilums != 8)
            {
                remainingPilums = 8;
            }
        }

        // End the game if remainig pilums is 0 and the game is currently being played.
        if (remainingPilums <= 0 && startGame)
        {
            Debug.Log("End Game");
            // Insert End Game Logic Here.
            StartCoroutine("EndGame", 0f);
            _audio.clip = startAudio;
            _audio.volume = 0.3f;
            _audio.Play();
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);
        startGame = true;
    }

    // End game procedure once it was called
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        _anim.SetBool("isStart", false);
        foreach (GameObject pilum in Pilums)
        {
            Destroy(pilum);
        }
        for (int i = 0; i < startObjects.Length; i++)
        {
            startObjects[i].SetActive(true);
        }
        remainingPilums = 0;
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        yield return new WaitForSeconds(2f);
        startGame = false;
        oneTime = false;
        gameArea.SetActive(false);
        defaultArea.SetActive(true);
        infoPanel.SetActive(false);
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

    public void DisableChild()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    public void EnableChild()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
}
