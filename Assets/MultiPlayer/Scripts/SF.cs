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

    public static List<string> cardClassList = new List<string> { "Bomb", "Shild", "Boots", "Bow", "Knife" };

    public const float hexUp = 3.37666f / 2;
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
                GameManeger.enemyMana = (int)photonEvent.CustomData;
                Board.updateEnemyMana();

                break;
            case 11:
                SF.tmpObjListClear();

                break;
            case 15:
                foreach (GameObject card in GameManeger.enemyActButtons)
                {
                    card.GetComponent<ActButton>().checkThisActButtton();
                    brighterSprite(Board.passButton);
                }
                Board.passButton.GetComponent<BoxCollider>().enabled = true;
                GameManeger.isBlueTurn = !GameManeger.isBlueTurn;
                TextManager.activateThisText(TextManager.yourTurn);

                break;
            case 31:
                GameManeger.gameIsOver = true;
                GameInterface.showEndPanel("defeat");
                showCardsAndTraps();

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
        PhotonNetwork.RaiseEvent(10, GameManeger.myMana, OtherEventOptions, StandatSendOptions);
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
        LocalGameManager.canClick = false;
        darkerSprite(Board.passButton);
        Board.passButton.GetComponent<BoxCollider>().enabled = false;
        sf.GetComponent<SF>().StartCoroutine("passCur");
        tmpObjListClear();
        TextManager.activateThisText(TextManager.waitFor);
    }

    IEnumerator passCur()
    {
        yield return new WaitForSeconds(0.3f);
        GameManeger.isBlueTurn = !GameManeger.isBlueTurn;
        LocalGameManager.canClick = true;
        PhotonNetwork.RaiseEvent(15, null, OtherEventOptions, StandatSendOptions);
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
        if (manaBoostTurns > 0)
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
            case "Bow":
                script = card.GetComponent<Bow>();
                break;
            case "Knife":
                script = card.GetComponent<Knife>();
                break;
        }
        return script;
    }

    public void iWon()
    {
        PhotonNetwork.RaiseEvent(31, null, OtherEventOptions, StandatSendOptions);
        GameManeger.gameIsOver = true;
        GameInterface.showEndPanel("victory");
        showCardsAndTraps();
    }

    private static void showCardsAndTraps()
    {
        foreach (GameObject card in GameManeger.enemyCards)
        {
            if (SF.getCardScript(card).isMarked)
            {
                SF.getCardScript(card).GetComponent<Renderer>().material = SF.getCardScript(card).markedMterial;
            }
            else if (SF.getCardScript(card).isCorrupted)
            {
                SF.getCardScript(card).GetComponent<Renderer>().material = SF.getCardScript(card).corruptedMterial;
            }
        }
        foreach (GameObject trap in GameManeger.traps)
        {
            if (trap != null)
            {
                trap.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
}