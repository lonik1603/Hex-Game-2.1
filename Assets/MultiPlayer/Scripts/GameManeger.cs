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

    public static List<string> myActivatedClasses;
    public static List<string> enemyActivatedClasses;


    public static int myMana;
    public static int enemyMana;

    public static int myGottenMarks;
    public static int enemyGottenMarks;

    public static int myGottenCur;
    public static int enemyGottenCur;

    public static bool gameIsOver;

    private void Start()
    {
        isBlueTurn = true;
        myMana = 4;
        enemyMana = 4;
        myGottenMarks = 0;
        enemyGottenMarks = 0;
        myGottenCur = 0;
        enemyGottenCur = 0;
        myCards = new List<GameObject>();
        enemyCards = new List<GameObject>();
        myActButtons = new List<GameObject>();
        enemyActButtons = new List<GameObject>();
        myAbilityCards = new List<GameObject>();
        enemyAbilityCards = new List<GameObject>();
        myActivatedClasses = new List<string>();
        enemyActivatedClasses = new List<string>();
        gameIsOver = false;
    }


    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {

    }
    public override void OnLeftRoom()
    {
        SF.tmpObjListClear();
        foreach(GameObject obj in CardsChoiseStage.cardPlaces)
        {
            Destroy(obj);
        }
        CardsChoiseStage.cardPlaces.Clear();
        SceneManager.LoadScene(0);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        SF.sf.GetComponent<SF>().iWon();
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has connected");

    }

}
