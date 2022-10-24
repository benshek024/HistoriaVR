using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public static VideoPlayerController instance { get { return _instance; } }
    private static VideoPlayerController _instance;

    private Animator _anim;
    public GameObject videoPlayerObj;
    public VideoPlayer videoPlayer;
    [Range(0.0f, 1.0f)]
    public float volume;

    public bool pressed { get; set; }
    private bool show = false;

    void Awake()
    {
        // Singleton
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
        if (videoPlayerObj == null)
        {
            Debug.LogError("ERROR: No video player game object assigned!");
            return;
        }

        try 
        { 
            videoPlayer = videoPlayerObj.GetComponent<VideoPlayer>();
            _anim = GetComponent<Animator>();
            videoPlayer.SetDirectAudioVolume(0, volume);
        }
        catch 
        {
            if (videoPlayer == null)
            {
                Debug.LogError("ERROR: No Video Player component found!");
                return;
            }
            if (_anim == null)
            {
                Debug.LogError("ERROR: No Animator component found!");
                return;
            }
        }
        Debug.Log("Video volume: " + videoPlayer.GetDirectAudioVolume(0));
    }

    void Update()
    {
        if (pressed && !show)
        {
            show = true;
            _anim.SetBool("Show", true);
            pressed = false;
        }

        if (pressed && show)
        {
            show = false;
            _anim.SetBool("Show", false);
            pressed = false;
        }
    }

    public void ShowVideo()
    {
        videoPlayerObj.SetActive(true);
    }

    public void CloseVideo()
    {
        videoPlayerObj.SetActive(false);
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}
