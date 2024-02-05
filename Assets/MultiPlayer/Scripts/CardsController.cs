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




    public void moveThisCard(Vector3 coords, GameObject thisCard)
    {
        int cardId = thisCard.GetPhotonView().ViewID;


        pView.RPC("moveThisCardRPC", RpcTarget.All, cardId, coords);
    }
    public void moveThisCard(Vector3 coord, GameObject thisCard, GameObject otherCard)
    {
        moveThisCard(coord, thisCard);
 
        int otherCardId = otherCard.GetPhotonView().ViewID;
        pView.RPC("eatThisCardRPC", RpcTarget.All, otherCardId);

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
        SF.getCardScript(card).canGiveMark = false;
        yield return new WaitForSeconds(0.05f);

        if (pView.IsMine)
        {
            if (GameManeger.myMana > 0)
            {
                SF.getCardScript(card).spawnPoints();
                LocalGameManager.activeCard = card;
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
            SF.getCardScript(LocalGameManager.activeCard).hasEaten = true;
            SF.getCardScript(LocalGameManager.activeCard).canMove = false;
            if (SF.getCardScript(card).isMarked)
            {
                Board.giveMeMark();
            }
        }
        yield return new WaitForSeconds(0.1f);

        Destroy(card);


    }

}
