using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LocalGameManager : MonoBehaviour
{
    public static bool isBlue;
    public static List<GameObject> tmpGameObjects;
    public static List<GameObject> cantMoveCards;

    public static int marksCount;
    public static int corruptionCount;

    public static bool canClick;

    [SerializeField] private GameObject BoardPref;

    public static GameObject Board;
    public static GameObject activeCard;
    public static GameObject cardsController;

    public static bool gameStarted;


    private void Start()
    {
        
        canClick = true;
        tmpGameObjects = new List<GameObject>();
        cantMoveCards = new List<GameObject>();
        gameStarted = false;
        isBlue = true;
        marksCount = 0;
        corruptionCount = 0;

        Board = Instantiate(BoardPref, new Vector3(0, 0, 0), Quaternion.identity);
        cardsController = PhotonNetwork.Instantiate("CardsController", new Vector3(0,0,0), Quaternion.identity);


    }

}
