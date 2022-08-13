using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CardsChoiseStage : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private List<GameObject> blueChoiceButtonsPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> redChoiceButtonsPrefabs = new List<GameObject>();
    [SerializeField] protected GameObject cardPlace;


    [SerializeField] private GameObject marksChoiceStage;

    protected static string choisenClass;
    protected static GameObject pressedButton;

    protected static List<GameObject> createdChoiceButtons = new List<GameObject>();
    public static List<GameObject> cardPlaces = new List<GameObject>();

    protected static int turnCount;
    public static int g;


    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 3:
                foreach (GameObject obj in createdChoiceButtons)
                {
                    TextManager.activateThisText(TextManager.ChooseCardClass);

                    SF.brighterSprite(obj);
                    obj.GetComponent<BoxCollider>().enabled = true;
                }
                break;

            case 6:
                foreach(GameObject obj in createdChoiceButtons)
                {
                    Destroy(obj);
                }
                TextManager.disableAllText();
                marksChoiceStage.SetActive(true);
                gameObject.SetActive(false);

            break;
        }
    }

    private void Start()
    {
        if (g == 0)
        {
            turnCount = 0;

            if (LocalGameManager.isBlue)
            {
                for (int i = 0; i < blueChoiceButtonsPrefabs.Count; i++)
                {
                    createdChoiceButtons.Add(Instantiate(blueChoiceButtonsPrefabs[i],
                    new Vector3(-10 + 5 * i, -19, 0), Quaternion.Euler(0, 0, 0)));
                }

                cardPlaces.Add(Instantiate(cardPlace, new Vector3(-9, -11.4f, -2), Quaternion.Euler(0, 0, 90)));
                cardPlaces.Add(Instantiate(cardPlace, new Vector3(0, -11.4f, -2), Quaternion.Euler(0, 0, -90)));
                cardPlaces.Add(Instantiate(cardPlace, new Vector3(9, -11.4f, -2), Quaternion.Euler(0, 0, 90)));

                TextManager.activateThisText(TextManager.ChooseCardClass);
            }
            else
            {
                for (int i = 0; i < redChoiceButtonsPrefabs.Count; i++)
                {
                    createdChoiceButtons.Add(Instantiate(redChoiceButtonsPrefabs[i],
                    new Vector3(10 - 5 * i, 19, 0), Quaternion.Euler(0, 0, 180)));
                }
                cardPlaces.Add(Instantiate(cardPlace, new Vector3(-9, 11.4f, -2), Quaternion.Euler(0, 0, -90)));
                cardPlaces.Add(Instantiate(cardPlace, new Vector3(0, 11.4f, -2), Quaternion.Euler(0, 0, 90)));
                cardPlaces.Add(Instantiate(cardPlace, new Vector3(9, 11.4f, -2), Quaternion.Euler(0, 0, -90)));
                diactivateClassChoiceButtons();
                TextManager.activateThisText(TextManager.waitFor);
            }
            g = 1;
        }

        disableCardPlaces();
    }
    protected void activateCardPlaces()
    {
        foreach (GameObject obj in cardPlaces)
        {
            obj.SetActive(true);
        }
    }
    protected void disableCardPlaces()
    {
        foreach (GameObject obj in cardPlaces)
        {
            obj.SetActive(false);
        }
    }
    protected static void activateClassChoiceButtons()
    {
        foreach (GameObject obj in createdChoiceButtons)
        {
            SF.brighterSprite(obj);
            obj.GetComponent<BoxCollider>().enabled = true;
        }
    }
    protected static void diactivateClassChoiceButtons()
    {
        foreach (GameObject obj in createdChoiceButtons)
        {
            SF.darkerSprite(obj);
            obj.GetComponent<BoxCollider>().enabled = false;
        }
    }
    protected static void nextTurn()
    {
        TextManager.activateThisText(TextManager.waitFor);


        PhotonNetwork.RaiseEvent(3, null, SF.OtherEventOptions, SF.StandatSendOptions);
    }
    protected static void nextStage()
    {
        PhotonNetwork.RaiseEvent(6, null, SF.StandertEventOptions, SF.StandatSendOptions);
    }
}
