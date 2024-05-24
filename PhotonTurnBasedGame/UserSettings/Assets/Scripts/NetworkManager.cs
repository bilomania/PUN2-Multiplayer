﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // instance
    public static NetworkManager instance;

    void Awake ()
    {
        // set the instance to this script
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        // connect to the master server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster ()
    {
        Debug.Log("Connected to master server");
        // set the player name
        //PhotonNetwork.NickName = "Player " + Random.Range(1000, 10000);
    }

    // joins a random room or creates a new room
    public void CreateOrJoinRoom ()
    {
        // if there are available rooms, join a random one
        if(PhotonNetwork.CountOfRooms > 0)
            PhotonNetwork.JoinRandomRoom();
        // otherwise, create a new room
        else
        {
            // set the max players to 2
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;

            PhotonNetwork.CreateRoom(null, options);
        }
    }

    // changes the scene using Photon's system
    [PunRPC]
    public void ChangeScene (string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}