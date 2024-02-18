using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CardsController : MonoBehaviour
{


    private PhotonView pView;
    GameObject card, otherCard;


    void Start()
    {
        pView = GetComponent<PhotonView>();
    }

    public static void moveThisCardTo(Vector3 coords, GameObject thisCard)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().moveThisCard(coords, thisCard);
    }
    public static void moveThisCardTo(Vector3 coords, GameObject thisCard, GameObject otherCard)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().moveThisCard(coords, thisCard, otherCard);
    }
    public static void eatThisCard(GameObject card)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().eatThisCard1(card);
    }

    private void eatThisCard1(GameObject card)
    {
        if (SF.getCardScript(card).cardClass == "Bomb" && SF.getCardScript(card).isActivated)
        {
            PhotonNetwork.Instantiate("Explosion", card.transform.position, Quaternion.identity);
        }
        else
        {
            int otherCardId = card.GetPhotonView().ViewID;
            pView.RPC("eatThisCardRPC", RpcTarget.All, otherCardId);
        }
    }



    private void moveThisCard(Vector3 coords, GameObject thisCard)
    {
        int cardId = thisCard.GetPhotonView().ViewID;


        pView.RPC("moveThisCardRPC", RpcTarget.All, cardId, coords);
    }
    private void moveThisCard(Vector3 coord, GameObject thisCard, GameObject otherCard)
    {
        moveThisCard(coord, thisCard);
        if (SF.getCardScript(otherCard).cardClass == "Bomb" && SF.getCardScript(otherCard).isActivated)
        {
            PhotonNetwork.Instantiate("Explosion", otherCard.transform.position, Quaternion.identity);
        }
        else
        {
            int otherCardId = otherCard.GetPhotonView().ViewID;
            pView.RPC("eatThisCardRPC", RpcTarget.All, otherCardId);
        }
    }





    [PunRPC]
    private void moveThisCardRPC(int cardId, Vector3 coords)
    {
        if (pView.IsMine)
        {
            card = LocalGameManager.activeCard;
        }
        else
        {
            foreach (GameObject obj in GameManeger.enemyCards)
            {
                if (cardId == obj.GetPhotonView().ViewID)
                {
                    card = obj;
                }

            }
        }
        StartCoroutine(MoveToCur(card, coords));

    }

    [PunRPC]
    private void eatThisCardRPC(int cardID)
    {
        foreach(GameObject obj in GameManeger.myCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                otherCard = obj;
            }
        }
        foreach (GameObject obj in GameManeger.enemyCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                otherCard = obj;
            }
        }

        StartCoroutine(eatThisCardCur(otherCard));
    }



    IEnumerator MoveToCur(GameObject card, Vector3 finPos)
    {
        LocalGameManager.canClick = false;
        card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -1f);
        Vector3 startPos = card.transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime * 5)
        {
            card.transform.position = Vector3.Lerp(startPos, finPos, i);
            yield return null;
        }
        card.transform.position = finPos;
        LocalGameManager.canClick = true;
        card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, 0);
        SF.getCardScript(card).canUseAbility = false;
        if (SF.getCardScript(card).isMarked)
        {
            if (SF.getCardScript(card).cardIsBlue)
            {
                if ((Mathf.Abs(Mathf.Abs(card.transform.position.x) - 6) < 0.5f) && Mathf.Abs(card.transform.position.y - 3 * SF.hexUp) < 0.5f)
                {
                    SF.getCardScript(card).canGiveMark = true;
                }
                else
                {
                    SF.getCardScript(card).canGiveMark = false;
                }
            }
            else
            {
                if ((Mathf.Abs(Mathf.Abs(card.transform.position.x) - 6) < 0.5f) && Mathf.Abs(card.transform.position.y + 3 * SF.hexUp) < 0.5f)
                {
                    SF.getCardScript(card).canGiveMark = true;
                }
                else
                {
                    SF.getCardScript(card).canGiveMark = false;
                }
            }
        }
        yield return new WaitForSeconds(0.05f);

        if (pView.IsMine)
        {
            if (GameManeger.myMana > 0)
            {
                if (SF.getCardScript(card).canMove)
                {
                    SF.getCardScript(card).spawnPoints();
                    LocalGameManager.activeCard = card;
                }
            }
            else
            {
                SF.pass();
                SF.tmpObjListClear();
            }
        }
    }

    IEnumerator eatThisCardCur(GameObject card)
    {
        if (SF.getCardScript(card).cardIsBlue == LocalGameManager.isBlue)
        {
            GameManeger.myCards.Remove(card);

        }
        else
        {
            GameManeger.enemyCards.Remove(card);

            if (SF.getCardScript(card).isMarked)
            {
                Board.giveMeMark();
            }
            if (SF.getCardScript(card).isCorrupted)
            {
                Board.giveMeCur();
            }
        }
        yield return new WaitForSeconds(0.1f);

        Destroy(card);

    }



    public static void activateThisCard(GameObject card)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().activateThisCard1(card);
    }
    private void activateThisCard1(GameObject card)
    {
        pView.RPC("activateThisCardRPC", RpcTarget.All, card.GetPhotonView().ViewID);
    }

    [PunRPC]
    private void activateThisCardRPC(int cardID)
    {
        GameObject cardToflip = findCard(cardID);
        if (!SF.getCardScript(cardToflip).isActivated)
        {
            SF.getCardScript(cardToflip).activateThisCard();
        }
    }





    public static void diactivateThisCard(GameObject card)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().diactivateThisCard1(card);
    }
    private void diactivateThisCard1(GameObject card)
    {
        pView.RPC("diactivateThisCardRPC", RpcTarget.All, card.GetPhotonView().ViewID);
    }
    
    [PunRPC]
    private void diactivateThisCardRPC(int cardID)
    {
        GameObject cardToflip = findCard(cardID);
        if (SF.getCardScript(cardToflip).isActivated)
        {
            SF.getCardScript(cardToflip).diactivateThisCard();
        }

    }




    static GameObject findCard(int cardID)
    {
        GameObject card = null;
        foreach (GameObject obj in GameManeger.myCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                card = obj;
            }
        }
        foreach (GameObject obj in GameManeger.enemyCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                card = obj;
            }
        }
        return card;
    }


    public static void activateThisClass(string cardClass)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().activateThisClass1(cardClass);
    }
    private void activateThisClass1(string cardClass)
    {
        pView.RPC("activateThisClassRPC", RpcTarget.All, cardClass);
    }

    [PunRPC]
    private void activateThisClassRPC(string cardClass)
    {
        GameObject actButton = null;
        if (pView.IsMine)
            foreach (GameObject obj in GameManeger.myActButtons)
            {
                if (obj.GetComponent<ActButton>().cardClass == cardClass)
                {
                    actButton = obj;
                }
            }
        else
        {
            foreach (GameObject obj in GameManeger.enemyActButtons)
            {
                if (obj.GetComponent<ActButton>().cardClass == cardClass)
                {
                    actButton = obj;
                }
            }
        }
        actButton.GetComponent<ActButton>().activateThis();
    }



    public static void diactivateThisClass(string cardClass)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().diactivateThisClass1(cardClass);
    }
    private void diactivateThisClass1(string cardClass)
    {
        pView.RPC("diactivateThisClassRPC", RpcTarget.All, cardClass);
    }

    [PunRPC]
    private void diactivateThisClassRPC(string cardClass)
    {
        GameObject actButton = null;
        if (pView.IsMine)
            foreach (GameObject obj in GameManeger.myActButtons)
            {
                if (obj.GetComponent<ActButton>().cardClass == cardClass)
                {
                    actButton = obj;
                }
            }
        else
        {
            foreach (GameObject obj in GameManeger.enemyActButtons)
            {
                if (obj.GetComponent<ActButton>().cardClass == cardClass)
                {
                    actButton = obj;
                }
            }
        }
        actButton.GetComponent<ActButton>().diactivateThis();
    }








    public static void destroyThisCard(GameObject card)
    {
        LocalGameManager.cardsController.GetComponent<CardsController>().destroyThisCard1(card);
    }

    private void destroyThisCard1(GameObject card)
    {
        int otherCardId = card.GetPhotonView().ViewID;
        pView.RPC("destroyThisCardRPC", RpcTarget.All, otherCardId);
    }


    [PunRPC]
    private void destroyThisCardRPC(int cardID)
    {
        foreach (GameObject obj in GameManeger.myCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                otherCard = obj;
            }
        }
        foreach (GameObject obj in GameManeger.enemyCards)
        {
            if (cardID == obj.GetPhotonView().ViewID)
            {
                otherCard = obj;
            }
        }

        StartCoroutine(destroyThisCardCur(otherCard));
    }


    IEnumerator destroyThisCardCur(GameObject card)
    {
        if (SF.getCardScript(card).cardIsBlue == LocalGameManager.isBlue)
        {
            GameManeger.myCards.Remove(card);

        }
        else
        {
            GameManeger.enemyCards.Remove(card);


        }
        yield return new WaitForSeconds(0.1f);

        card.SetActive(false);

    }
}
