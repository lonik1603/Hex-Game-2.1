using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Board : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private GameObject blueMana;
    [SerializeField] private GameObject redMana;
    [SerializeField] private GameObject bluePassButtun;
    [SerializeField] private GameObject redPassButtun;
    [SerializeField] private GameObject Mark;

    public static GameObject passButton;

    private static List<GameObject> myMarks;
    private static List<GameObject> enemyMarks;


    private static List<GameObject> myMana;
    private static List<GameObject> enemyMana;

    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 18:
                GameManeger.enemyGottenMarks++;
                enemyMarks[GameManeger.enemyGottenMarks - 1].SetActive(true);
                break;
            case 19:
                GameManeger.myGottenMarks++;
                myMarks[GameManeger.myGottenMarks - 1].SetActive(true);
                markCheck();
                break;
            case 20:
                GameManeger.enemyAbilityCards[(int)photonEvent.CustomData].GetComponent<AbilityCard>().useThisCard();
                break;
        }
    }
    private void Start()
    {
        myMana = new List<GameObject>();
        enemyMana = new List<GameObject>();

        myMarks = new List<GameObject>();
        enemyMarks = new List<GameObject>();


        if (LocalGameManager.isBlue)
        {
            passButton = Instantiate(bluePassButtun, new Vector3(10, -22, 0), Quaternion.identity);
            for (int i = 0; i < 4; i++)
            {
                myMana.Add(Instantiate(blueMana, new Vector3(-1.353f + i * 0.9f, -14.6f, 0), Quaternion.identity));
                enemyMana.Add(Instantiate(redMana, new Vector3(-1.353f + i * 0.9f, 14.6f, 0), Quaternion.identity));
            }
            for (int i = 0; i < 3; i++)
            {
                myMarks.Add(Instantiate(Mark, new Vector3(0, -15.73f - 0.9f * i, 0), Quaternion.identity));
                enemyMarks.Add(Instantiate(Mark, new Vector3(0, 15.73f + 0.9f * i, 0), Quaternion.identity));
            }


        }
        else
        {
            passButton = Instantiate(redPassButtun, new Vector3(-10, 22, 0), Quaternion.Euler(0, 0, 180));
            for (int i = 0; i < 4; i++)
            {
                myMana.Add(Instantiate(redMana, new Vector3(1.353f - i * 0.9f, 14.6f, 0), Quaternion.identity));
                enemyMana.Add(Instantiate(blueMana, new Vector3(1.353f - i * 0.9f, -14.6f, 0), Quaternion.identity));
            }
            for (int i = 0; i < 3; i++)
            {
                myMarks.Add(Instantiate(Mark, new Vector3(0, 15.73f + 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));
                enemyMarks.Add(Instantiate(Mark, new Vector3(0, -15.73f - 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            myMana[i].SetActive(true);
            enemyMana[i].SetActive(true);
        }

        for (int i = 0; i < 3; i++)
        {
            myMarks[i].SetActive(false);
            enemyMarks[i].SetActive(false);
        }
    }
    public static void updateMyMana()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < GameManeger.myMana)
            {
                myMana[i].SetActive(true);
            }
            else
            {
                myMana[i].SetActive(false);
            }
        }
    }
    public static void updateEnemyMana()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < GameManeger.enemyMana)
            {
                enemyMana[i].SetActive(true);
            }
            else
            {
                enemyMana[i].SetActive(false);
            }
        }
    }

    public static void giveMeMark()
    {
        GameManeger.myGottenMarks++;
        myMarks[GameManeger.myGottenMarks - 1].SetActive(true);
        PhotonNetwork.RaiseEvent(18, null, SF.OtherEventOptions, SF.StandatSendOptions);
        markCheck();
    }
    public static void giveOtherPlayerMark()
    {
        GameManeger.enemyGottenMarks++;
        enemyMarks[GameManeger.enemyGottenMarks - 1].SetActive(true);
        PhotonNetwork.RaiseEvent(19, null, SF.OtherEventOptions, SF.StandatSendOptions);
    }




    public static void useThisAblityCard(int id)
    {
        PhotonNetwork.RaiseEvent(20, id, SF.OtherEventOptions, SF.StandatSendOptions);
    }

    public static void markCheck()
    {
        if(GameManeger.myGottenMarks == 3)
        {
            SF.sf.GetComponent<SF>().iWon();
        }
    }
}
