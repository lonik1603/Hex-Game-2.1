using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.2";
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public void matchmaking()
    {
        Debug.Log("Trying to join random room");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2});
        Debug.Log("New room created");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        Debug.Log("Connected to room");
    }

    public void createRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2});
        Debug.Log("New room created");
    }
}