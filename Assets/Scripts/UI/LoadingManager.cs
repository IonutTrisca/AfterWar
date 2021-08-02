using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;
    public GameObject loadingScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadSceneAsync((int)Scenes.UI, LoadSceneMode.Additive);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)Scenes.UI);
        SceneManager.LoadSceneAsync((int)Scenes.GAME, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
