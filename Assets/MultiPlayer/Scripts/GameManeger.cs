using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManeger : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static bool isBlueTurn;

    public static List<GameObject> myCards;
    public static List<GameObject> enemyCards;

    public static List<GameObject> myActButtons;
    public static List<GameObject> enemyActButtons;

    public static List<GameObject> myAbilityCards;
    public static List<GameObject> enemyAbilityCards;


    public static int myMana;
    public static int enemyMana;

    public static int myGottenMarks;
    public static int enemyGottenMarks;

    private void Start()
    {
        isBlueTurn = true;
        myMana = 2;
        enemyMana = 2;
        myCards = new List<GameObject>();
        enemyCards = new List<GameObject>();
        myActButtons = new List<GameObject>();
        enemyActButtons = new List<GameObject>();
        myAbilityCards = new List<GameObject>();
        enemyAbilityCards = new List<GameObject>();
        Crown.myActivatedClasses = new List<string>();
        Crown.enemyActivatedClasses = new List<string>();

    }


    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has connected");

    }

}
