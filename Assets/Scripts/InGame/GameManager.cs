using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> spawners = new Dictionary<int, ItemSpawner>();
    public List<GameObject> spawnerPrefabs;
    public List<GameObject> weaponPrefabs;
    public List<Vector3> weaponPosLocal;
    public List<Quaternion> weaponRotLocal;

    public List<Vector3> weaponPosRemote;
    public List<Quaternion> weaponRotRemote;

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

        weaponPosLocal.Add(new Vector3(0.32f, -0.48f, 0.67f));
        weaponPosLocal.Add(new Vector3(0.32f, -0.15f, 0.67f));
        weaponPosLocal.Add(new Vector3(0.32f, -0.75f, 0.67f));

        weaponRotLocal.Add(Quaternion.Euler(-0.788f, 85.889f, -3.845f));
        weaponRotLocal.Add(Quaternion.Euler(-5.78f, -7.7f, 4.25f));
        weaponRotLocal.Add(Quaternion.Euler(-0.788f, 85.889f, -8.24f));

        weaponPosRemote.Add(new Vector3(0.214f, 0.234f, 0.04f));
        weaponPosRemote.Add(new Vector3(-0.086f, 0.265f, 0.064f));
        weaponPosRemote.Add(new Vector3(0.352f, 0.142f, 0.021f));

        weaponRotRemote.Add(Quaternion.Euler(-8.99f, -180f, -84f));
        weaponRotRemote.Add(Quaternion.Euler(264f, -180f, -90f));
        weaponRotRemote.Add(Quaternion.Euler(-171.05f, 0f, 95.7f));

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

    public void CreateItemSpawner(int spawnerId, bool hasItem, Vector3 position, WeaponTypes type)
    {
        GameObject spawner = Instantiate(spawnerPrefabs[(int)type], position, itemSpawnerPrefab.transform.rotation);
        spawner.GetComponent<ItemSpawner>().Intitialize(spawnerId, hasItem, position, type);
        spawners.Add(spawnerId, spawner.GetComponent<ItemSpawner>());

        Debug.Log("Created Item spawner of type " + (int)type);
    }
}
