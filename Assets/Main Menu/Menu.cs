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
    [SerializeField] private GameObject version;


    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject basisPanel;
    [SerializeField] private GameObject classesPanel;
    [SerializeField] private GameObject additinalPanel;
    [SerializeField] private GameObject CreditslPanel;
    [SerializeField] private GameObject rules;

    const string gameVersion = "0.3.1a";
    private static bool menuIsActive;
    private void Start()
    {
        menuIsActive = false;
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
        if (PhotonNetwork.IsConnected == false)
        {
            loading.SetActive(true);
            menu.SetActive(false);
            menuIsActive = false;

            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            if (menuIsActive == false)
            {
                menuIsActive = true;
                menu.SetActive(true);
                loading.SetActive(false);
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        menuIsActive = true;
        Debug.Log("Connected to master");
        menu.SetActive(true);
        loading.GetComponent<Image>().enabled = false;
    }

    public void matchmaking()
    {
        if (PhotonNetwork.IsConnected)
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
        if (PhotonNetwork.IsConnected)
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

    public void backToMenu()
    {
        menu.SetActive(true);
        rulesPanel.SetActive(false);
        CreditslPanel.SetActive(false);
        version.SetActive(true);
    }


    public void toRulesChose()
    {
        menu.SetActive(false);
        rulesPanel.SetActive(true);
        version.SetActive(false);

    }

    public void toBasis()
    {
        rulesPanel.SetActive(false);
        basisPanel.SetActive(true);

        rules.SetActive(true);
    }
    public void toClasses()
    {
        rulesPanel.SetActive(false);
        classesPanel.SetActive(true);
        rules.SetActive(true);
    }
    public void toAdditional()
    {
        rulesPanel.SetActive(false);
        additinalPanel.SetActive(true);

        rules.SetActive(true);
    }
    public void backToRulesChose()
    {
        rulesPanel.SetActive(true);

        basisPanel.SetActive(false);
        classesPanel.SetActive(false);
        additinalPanel.SetActive(false);

        rules.SetActive(false);

    }

    public void toCredits()
    {
        CreditslPanel.SetActive(true);
        menu.SetActive(false);
    }

}