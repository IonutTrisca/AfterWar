using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public TMPro.TextMeshProUGUI health;
    public TMPro.TextMeshProUGUI armor;
    public TMPro.TextMeshProUGUI magazine;
    public TMPro.TextMeshProUGUI reserve;

    public int selectedWeapon = 0;

    public List<WeaponTypes> weapons = new List<WeaponTypes>();

    public bool paused = false;

    public GameObject pauseMenu;
    public GameObject hud;
    public GameObject deathScreen;

    public List<Image> weaponImages;
    public List<Sprite> weaponSprites;
    public List<Image> weaponBackgrounds;

    private bool gameEnded = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            for (int i = 0; i < 4; i++)
                weapons.Add(WeaponTypes.NoWeapon);
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
            HUDManager.instance.health.SetText(GameManager.players[Client.instance.gameId].stats.health.ToString());
            HUDManager.instance.armor.SetText(GameManager.players[Client.instance.gameId].stats.armor.ToString());
            HUDManager.instance.magazine.SetText(GameManager.players[Client.instance.gameId].stats.magazine.ToString());
            HUDManager.instance.reserve.SetText(GameManager.players[Client.instance.gameId].stats.reserve.ToString());
        } catch (Exception)
        {
            Debug.LogError("Player not spawned");
        }

        SelectCurrentWeapon();
        ShowWeapons();

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

        if (gameEnded && Input.anyKey)
        {
            Client.instance.Disconnect();
            SceneManager.LoadScene((int)Scenes.UI);
            SceneManager.UnloadSceneAsync((int)Scenes.GAME);
        }
    }

    private void SelectCurrentWeapon()
    {
        int i = 0;

        foreach(Image bg in weaponBackgrounds)
        {
            if (i == selectedWeapon)
            {
                Color tmp = bg.color;
                tmp.a = 0.192f;
                bg.color = tmp;
            }
            else
            {
                Color tmp = bg.color;
                tmp.a = 0;
                bg.color = tmp;
            }
            i++;
        }

    }

    private void ShowWeapons()
    {
        for(int i = 0; i < 4; i++)
        {
            if (weapons[i] == WeaponTypes.NoWeapon)
            {
                weaponImages[i].sprite = weaponSprites[3];
            }
            else
            {
                weaponImages[i].sprite = weaponSprites[(int)weapons[i]];
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


    public void GameEnded()
    {
        gameEnded = true;
        hud.SetActive(false);
        pauseMenu.SetActive(false);
        deathScreen.SetActive(true);
        CameraController.instance.ShowCursor();
    }
}
