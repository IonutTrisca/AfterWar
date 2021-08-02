﻿using UnityEngine;

class ClientSend
{
    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.udp.SendData(packet);
    }

    private static void SendGameServerTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.gameTcp.SendData(packet);
    }

    private static void SendLoginServerData(Packet packet)
    {
        packet.WriteLength();
        LoginClient.instance.tcp.SendData(packet);
    }

    public static void Register(string name, string username, string email, string password)
    {
        using (Packet packet = new Packet((int)ClientPackets.registerRequest))
        {
            packet.Write(Client.instance.id);
            packet.Write(name);
            packet.Write(email);
            packet.Write(username);
            packet.Write(password);

            SendLoginServerData(packet);
        }
    }

    public static void Login(string username, string password)
    {
        using (Packet packet = new Packet((int)ClientPackets.loginRequest))
        {
            packet.Write(Client.instance.id);
            packet.Write(username);
            packet.Write(password);

            SendLoginServerData(packet);
        }
    }

    public static void FindGame()
    {
        using (Packet packet = new Packet((int)ClientPackets.playGame))
        {
            packet.Write(Client.instance.id);
            SendLoginServerData(packet);
        }
    }

    public static void WelcomeReceived(int gameId, string username)
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(gameId);
            packet.Write(username);
            SendGameServerTCPData(packet);
        }
    }

    public static void PlayerMovement(PlayerMovement playerMovement)
    {
        using (Packet packet = new Packet((int)ClientPackets.playerMovement))
        {
            packet.Write(playerMovement.transform.position);
            packet.Write(playerMovement.transform.rotation);
            packet.Write(playerMovement.camTransform.rotation);
            packet.Write(playerMovement.spine.xRotation);
            packet.Write(playerMovement.keysPressed.Length);
            packet.Write(playerMovement.isGrounded);

            foreach (bool key in playerMovement.keysPressed)
            {
                packet.Write(key);
            }

            SendUDPData(packet);
        }
    }
}