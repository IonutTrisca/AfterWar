using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public TMPro.TextMeshProUGUI health;
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject hud;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        try
        {
            HUDManager.instance.health.SetText("HEALTH: " + GameManager.players[Client.instance.gameId].stats.health);
        } catch (Exception)
        {
            Debug.LogError("Player not spawned");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseMenu.SetActive(paused);
            if (paused)
            {
                CameraController.instance.ShowCursor();
                pauseMenu.SetActive(true);
                hud.SetActive(false);
            }
            else
            {
                CameraController.instance.HideCursor();
                pauseMenu.SetActive(false);
                hud.SetActive(true);
            }
        }
    }

    public void LeaveGame()
    {
        Client.instance.Disconnect();
        SceneManager.LoadScene((int)Scenes.UI);
    }

    public void Resume()
    {
        CameraController.instance.HideCursor();
        paused = false;
        pauseMenu.SetActive(false);
        hud.SetActive(true);
    }
}
