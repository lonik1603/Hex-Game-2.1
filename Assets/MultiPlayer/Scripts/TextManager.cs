using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject cardsChoseStage;

    [SerializeField] private GameObject ChooseCardClassBluePref;
    [SerializeField] private GameObject ChooseCardClassRedPref;
    [SerializeField] private GameObject waitForBluePref;
    [SerializeField] private GameObject waitForRedPref;
    [SerializeField] private GameObject mark3ofCardsPref;
    [SerializeField] private GameObject yourTurnBluePref;
    [SerializeField] private GameObject yourTurnRedPref;

    public static GameObject ChooseCardClass;
    public static GameObject waitFor;
    public static GameObject mark3ofCards;
    public static GameObject yourTurn;


    public static List<GameObject> textList;
    private Vector3 textCoord; 


    void Start()
    {
        textList = new List<GameObject>();

        if (LocalGameManager.isBlue)
        {
            textCoord = new Vector3(-4, -22, 0);
            ChooseCardClass = Instantiate(ChooseCardClassBluePref, textCoord, Quaternion.Euler(0, 0, 0));
            waitFor = Instantiate(waitForBluePref, textCoord, Quaternion.Euler(0, 0, 0));
            yourTurn =Instantiate(yourTurnBluePref, textCoord, Quaternion.Euler(0, 0, 0));
            mark3ofCards = Instantiate(mark3ofCardsPref, textCoord, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            textCoord = new Vector3(4, 22, 0);
            ChooseCardClass =Instantiate(ChooseCardClassRedPref, textCoord, Quaternion.Euler(0, 0, 180));
            waitFor = Instantiate(waitForRedPref, textCoord, Quaternion.Euler(0, 0, 180));
            yourTurn = Instantiate(yourTurnRedPref, textCoord, Quaternion.Euler(0, 0, 180));
            mark3ofCards = Instantiate(mark3ofCardsPref, textCoord, Quaternion.Euler(0, 0, 180));
        }
        textList.Add(ChooseCardClass);
        textList.Add(waitFor);
        textList.Add(yourTurn);
        textList.Add(mark3ofCards);

        disableAllText();
        cardsChoseStage.SetActive(true);
    }

    public static void disableAllText()
    {
        foreach(GameObject obj in textList)
        {
            obj.SetActive(false);
        }
    }
    public static void activateThisText(GameObject obj)
    {
        disableAllText();
        obj.SetActive(true);
    }
}
