using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    public static bool isBlue;
    public static List<GameObject> tmpGameObjects;
    public static List<GameObject> cantMoveCards;
    public static int marksCount;

    [SerializeField] private GameObject BoardPref;

    public static GameObject Board;
    public static GameObject activeCard;

    public static bool gameStarted;


    private void Start()
    {
        tmpGameObjects = new List<GameObject>();
        cantMoveCards = new List<GameObject>();
        gameStarted = false;
        isBlue = true;
        marksCount = 0;

        Board = Instantiate(BoardPref, new Vector3(0, 0, 0), Quaternion.identity);


    }

}
