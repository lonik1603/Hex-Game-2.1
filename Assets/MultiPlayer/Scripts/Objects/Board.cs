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
    [SerializeField] private GameObject corruption;

    public static GameObject passButton;

    private static List<GameObject> myMarks;
    private static List<GameObject> enemyMarks;


    private static List<GameObject> myCorruptions;
    private static List<GameObject> enemyCorruptions;



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

            case 45:
                GameManeger.enemyGottenCur++;
                enemyCorruptions[GameManeger.enemyGottenCur - 1].SetActive(true);
                curCheck();
                break;

            case 46:
                GameManeger.myGottenCur++;
                myCorruptions[GameManeger.myGottenCur - 1].SetActive(true);

                break;
        }
    }
    private void Start()
    {
        myMana = new List<GameObject>();
        enemyMana = new List<GameObject>();

        myMarks = new List<GameObject>();
        enemyMarks = new List<GameObject>();

        myCorruptions = new List<GameObject>();
        enemyCorruptions = new List<GameObject>();


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
                myMarks.Add(Instantiate(Mark, new Vector3(-12, -19.5f - 0.9f * i, 0), Quaternion.identity));
                enemyMarks.Add(Instantiate(Mark, new Vector3(12, 19.5f + 0.9f * i, 0), Quaternion.identity));

                myCorruptions.Add(Instantiate(corruption, new Vector3(-8, -19.5f - 0.9f * i, 0), Quaternion.identity));
                enemyCorruptions.Add(Instantiate(corruption, new Vector3(8, 19.5f + 0.9f * i, 0), Quaternion.identity));
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
                myMarks.Add(Instantiate(Mark, new Vector3(12, 19.5f + 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));
                enemyMarks.Add(Instantiate(Mark, new Vector3(-12, -19.5f - 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));

                myCorruptions.Add(Instantiate(corruption, new Vector3(8, 19.5f + 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));
                enemyCorruptions.Add(Instantiate(corruption, new Vector3(-8, -19.5f - 0.9f * i, 0), Quaternion.Euler(0, 0, 180)));
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

            myCorruptions[i].SetActive(false);
            enemyCorruptions[i].SetActive(false);
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

    public static void giveMeCur()
    {
        GameManeger.myGottenCur++;
        myCorruptions[GameManeger.myGottenCur - 1].SetActive(true);
        PhotonNetwork.RaiseEvent(45, null, SF.OtherEventOptions, SF.StandatSendOptions);

    }
    public static void giveOtherPlayerCur()
    {
        GameManeger.enemyGottenCur++;
        enemyCorruptions[GameManeger.enemyGottenCur - 1].SetActive(true);
        PhotonNetwork.RaiseEvent(46, null, SF.OtherEventOptions, SF.StandatSendOptions);
        curCheck();
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
    public static void curCheck()
    {
        if(GameManeger.enemyGottenCur == 3)
        {
            SF.sf.GetComponent<SF>().iWon();
        }
    }
}
