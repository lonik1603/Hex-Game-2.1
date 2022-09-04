using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SF : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private GameObject sfPref;
    public static GameObject sf;

    [SerializeField] private GameObject youWon;
    [SerializeField] private GameObject youLost;

    public static RaiseEventOptions StandertEventOptions = new RaiseEventOptions
    { Receivers = ReceiverGroup.All };

    public static RaiseEventOptions OtherEventOptions = new RaiseEventOptions
    { Receivers = ReceiverGroup.Others };
    public static ExitGames.Client.Photon.SendOptions StandatSendOptions = new ExitGames.Client.Photon.SendOptions
    { Reliability = true };
    public static int manaBoostTurns; 

    public static List<string> cardClassList = new List<string> { "Bomb", "Shild", "Boots", "Crown" };

    public const float hexUp = 1.75f;
    private static Card script;
    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 5:
                GameManeger.isBlueTurn = !GameManeger.isBlueTurn;
                Debug.Log("next turn");
                break;

            case 10:
                GameManeger.enemyMana += (int)photonEvent.CustomData;
                Board.updateEnemyMana();

                break;
            case 11:
                SF.tmpObjListClear();
                GameManeger.isBlueTurn = !GameManeger.isBlueTurn;

                if (isMyTurn())
                {
                    TextManager.activateThisText(TextManager.yourTurn);
                    brighterSprite(Board.passButton);
                    Board.passButton.GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    TextManager.activateThisText(TextManager.waitFor);

                }
                break;
            case 15:
                foreach(GameObject card in GameManeger.enemyActButtons)
                {
                    card.GetComponent<ActButton>().checkThisActButtton();
                }
                break;
            case 31:
                GameManeger.gameIsOver = true;
                youLost.SetActive(true);

                break;
            case 32:

                break;
        }
    }

    private void Start()
    {
        script = new Card();
        sf = sfPref;
        manaBoostTurns = 0;
    }
    public static void tmpObjListClear()
    {
        TextManager.disableAllText();
        foreach (GameObject obj in LocalGameManager.tmpGameObjects)
        {
            Destroy(obj);
        }
        LocalGameManager.tmpGameObjects.Clear();
    }
    public static void tmpObjListDisable()
    {
        foreach (GameObject obj in LocalGameManager.tmpGameObjects)
        {
            obj.GetComponent<BoxCollider>().enabled = false;
            obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }

    public static void choiceStagePass()
    {

        PhotonNetwork.RaiseEvent(5, null, StandertEventOptions, StandatSendOptions);

    }

    public static void darkerSprite(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
    public static void brighterSprite(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1f, 1f, 1f);
    }

    public static void changeMana(int mana)
    {
        GameManeger.myMana += mana;
        if (GameManeger.myMana > 4)
        {
            GameManeger.myMana = 4;
        }
        Board.updateMyMana();
        PhotonNetwork.RaiseEvent(10, mana, OtherEventOptions, StandatSendOptions);
    }

    public static bool isMyTurn()
    {
        if (((LocalGameManager.isBlue && GameManeger.isBlueTurn) || (!LocalGameManager.isBlue && !GameManeger.isBlueTurn)) && LocalGameManager.gameStarted && LocalGameManager.canClick)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void pass()
    {
        GameManeger.isBlueTurn = !GameManeger.isBlueTurn;
        darkerSprite(Board.passButton);
        Board.passButton.GetComponent<BoxCollider>().enabled = false;
        sf.GetComponent<SF>().StartCoroutine("passCur");
        tmpObjListClear();
    }

    IEnumerator passCur()
    {
        yield return new WaitForSeconds(0.3f);
        PhotonNetwork.RaiseEvent(15, null, OtherEventOptions, StandatSendOptions);
        GameManeger.isBlueTurn = !GameManeger.isBlueTurn;
        PhotonNetwork.RaiseEvent(11, null, StandertEventOptions, StandatSendOptions);
        foreach (GameObject card in GameManeger.myActButtons)
        {
            card.GetComponent<ActButton>().checkThisActButtton();
        }
        foreach (GameObject card in GameManeger.myCards)
        {
            getCardScript(card).cardCheck();
        }
        changeMana(2);
        if(manaBoostTurns > 0)
        {
            changeMana(1);
            manaBoostTurns -= 1;
        }
    }
    private static void cardsCanMove()
    {
        foreach (GameObject obj in LocalGameManager.cantMoveCards)
        {
            getCardScript(obj).canMove = true;
        }
        LocalGameManager.cantMoveCards.Clear();
    }

    public static Card getCardScript(GameObject card)
    {
        
        switch (card.tag)
        {
            case "Bomb":
                script = card.GetComponent<Bomb>();
                break;
            case "Boots":
                script = card.GetComponent<Boots>();
                break;
            case "Shild":
                script = card.GetComponent<Shild>();
                break;
            case "Crown":
                script = card.GetComponent<Crown>();
                break;
        }
        return script;
    }

    public void iWon()
    {
        PhotonNetwork.RaiseEvent(31, null, OtherEventOptions, StandatSendOptions);
        GameManeger.gameIsOver = true;
        youWon.SetActive(true);
    }

}
