using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameInterface : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject pauseWin;

    public void openClosePause()
    {
        pauseWin.SetActive(!pauseWin.activeInHierarchy);
    }


    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    public void newGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.RandomMatching, null, null, null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
    }
}
