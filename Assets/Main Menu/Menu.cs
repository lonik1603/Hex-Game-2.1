using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    private static bool conected;
    private void Awake()
    {
        conected = false;
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.2";
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        conected = true;
        Debug.Log("Connected to master");
    }

    public void matchmaking()
    {
        if (conected)
        {

            Debug.Log("Trying to join random room");
            PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.RandomMatching, null, null, null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2}, null);
        Debug.Log("New room created");
    }
    public override void OnJoinedRoom()
    {

        Debug.Log("Connected to room");
    }

    public void createRoom()
    {
        if (conected)
        {
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
            Debug.Log("New room created");
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.RemovedFromList = true;
            PhotonNetwork.LoadLevel("Game");
        }

    }
}