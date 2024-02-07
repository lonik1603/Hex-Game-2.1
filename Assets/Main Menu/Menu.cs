using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Menu : MonoBehaviourPunCallbacks
{
    public InputField nickname;
    [SerializeField] private Text gameVersionText;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject loading;

    const string gameVersion = "0.2.4b";
    private static bool conected;
    private void Start()
    {
        conected = false;
        if (PlayerPrefs.HasKey("Nickname"))
        {
            nickname.text = PlayerPrefs.GetString("Nickname");
            PhotonNetwork.NickName = nickname.text;
        }
        else
        {
            PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        gameVersionText.text = "V" + gameVersion;

    }

    private void Update()
    {
        if (conected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        conected = true;
        Debug.Log("Connected to master");
        menu.SetActive(true);
        loading.GetComponent<Image>().enabled = false;
    }

    public void matchmaking()
    {
        if (conected)
        {

            Debug.Log("Trying to join random room");
            PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.RandomMatching, null, null, null, new Photon.Realtime.RoomOptions { MaxPlayers = 2, CleanupCacheOnLeave = false }, null);
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2, CleanupCacheOnLeave = false }, null);
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
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2, CleanupCacheOnLeave = false});
            Debug.Log("New room created");
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.RemovedFromList = true;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("Game");
        }

    }

    public void saveNickname()
    {
        PlayerPrefs.SetString("Nickname", nickname.text);
        PhotonNetwork.NickName = nickname.text;
    }

}