using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> spawners = new Dictionary<int, ItemSpawner>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;

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

    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation, int deaths, int score, int kills)
    {
        GameObject player;

        if (id == Client.instance.gameId)
        {
            Debug.Log("Spawning Local Player");
            player = Instantiate(localPlayerPrefab, position, rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, position, rotation);
        }

        player.GetComponent<PlayerManager>().Initialize(id, username, deaths, kills, score);

        players.Add(id, player.GetComponent<PlayerManager>());
    }

    public void CreateItemSpawner(int spawnerId, bool hasItem, Vector3 position)
    {
        GameObject spawner = Instantiate(itemSpawnerPrefab, position, itemSpawnerPrefab.transform.rotation);
        spawner.GetComponent<ItemSpawner>().Intitialize(spawnerId, hasItem, position);
        spawners.Add(spawnerId, spawner.GetComponent<ItemSpawner>());
    }
}
