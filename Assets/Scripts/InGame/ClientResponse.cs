using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ClientResponse : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        LoginClient.instance.id = packet.ReadInt();
        Debug.Log($"Received login id {LoginClient.instance.id}");
    }

    public static void WelcomeToGame(Packet packet)
    {
        Client.instance.gameId = packet.ReadInt();
        Debug.Log($"Received game id {Client.instance.gameId}");
        ClientSend.WelcomeReceived(Client.instance.gameId, Client.instance.username);
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.gameTcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        Debug.Log($"Spawning player");
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        int deaths = packet.ReadInt();
        int score = packet.ReadInt();
        int kills = packet.ReadInt();
        bool hasWeapon = packet.ReadBool();
        float health = packet.ReadFloat();
        Debug.Log($"Spawning player {id} at position {position}");
        GameManager.instance.SpawnPlayer(id, username, position, rotation, deaths, score, kills);
        if (hasWeapon)
        {
            GameManager.players[id].ItemPickedUp();
            GameManager.players[id].SetHealth(health);
        }
    }

    public static void PlayerMovement(Packet packet)
    {
        try
        {
            int id = packet.ReadInt();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();

            bool[] keysPressed = new bool[packet.ReadInt()];

            for (int i = 0; i < keysPressed.Length; i++)
            {
                keysPressed[i] = packet.ReadBool();
            }

            GameManager.players[id].transform.position = position;
            GameManager.players[id].transform.rotation = rotation;
            GameManager.players[id].SetKeysPressed(keysPressed);
            GameManager.players[id].isGrounded = packet.ReadBool();
            GameManager.players[id].spineAngle = packet.ReadFloat();
        } catch (Exception)
        {
            Debug.LogError("Received Movement from unknown player");
        }

    }

    public static void PlayerDisconnected(Packet packet)
    {
        int id = packet.ReadInt();
        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
    }

    public static void CreateItemSpawner(Packet packet)
    {
        int spawnerId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        bool hasItem = packet.ReadBool();

        GameManager.instance.CreateItemSpawner(spawnerId, hasItem, position);
    }

    public static void ItemSpawned(Packet packet)
    {
        int spawnerId = packet.ReadInt();
        GameManager.spawners[spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet packet)
    {
        int spawnerId = packet.ReadInt();
        int byPlayer = packet.ReadInt();

        GameManager.spawners[spawnerId].ItemPickedUp();
        GameManager.players[byPlayer].ItemPickedUp();
    }

    public static void PlayerHealth(Packet packet)
    {
        int id = packet.ReadInt();
        float health = packet.ReadFloat();

        GameManager.players[id].SetHealth(health);
    }
}