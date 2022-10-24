using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
    public ScreenFader screenFader = null;

    private bool isLoading = false;

    [Header("Locations")]
    public Transform playerLocation;
    public Transform menuLocation;
    public Transform romeHallLocation;
    public Transform mgAreaLocation;
    public Transform templeOutLocation;
    public Transform templeInLocation;
    public Transform coloOutLocation;
    public Transform coloInLocation;
    public Transform cdLocation;

    // Scene name string
    const string menu = "Menu";
    const string romeHall = "Rome Exhibit Hall";
    const string mgArea = "Minigame Area";
    const string templeOut = "Temple Outside";
    const string templeIn = "Temple Inside";
    const string coloOut = "Colosseum Outside";
    const string coloIn = "Colosseum Inside";
    const string credits = "Credits";

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadNewScene(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    // Load new scene based on the value on parameter
    IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;

        OnLoadBegin?.Invoke();

        // Starting fade-in effect and unload current scene, load next scene afterward
        yield return screenFader.StartFadeIn();
        yield return StartCoroutine(UnloadCurrent());

        yield return StartCoroutine(LoadNew(sceneName));

        // Set player's current position to new position based on which scene is called
        switch (sceneName)
        {
            case menu:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = menuLocation.position;
                break;
            case romeHall:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = romeHallLocation.position;
                break;
            case templeOut:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = templeOutLocation.position;
                break;
            case templeIn:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = templeInLocation.position;
                break;
            case coloOut:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = coloOutLocation.position;
                break;
            case coloIn:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = coloInLocation.position;
                break;
            case mgArea:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = mgAreaLocation.position;
                break;
            case credits:
                Debug.Log("Teleport to " + sceneName);
                playerLocation.position = cdLocation.position;
                break;
        }

        // Call fade-out effect and end loading procedure
        yield return screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();

        isLoading = false;
    }

    // Unloading scene player currently at
    IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }

    // Load a new scene
    IEnumerator LoadNew(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
}
