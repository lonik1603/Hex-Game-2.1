using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CardsConroller : MonoBehaviour
{
    private PhotonView pView;
    GameObject card;


    void Start()
    {
        pView = GetComponent<PhotonView>();
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
    IEnumerator MoveToCur(GameObject card, Vector3 finPos)
    {
        LocalGameManager.canClick = false;
        card.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f);
        Vector3 startPos = gameObject.transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime * 10)
        {
            card.transform.position = Vector3.Lerp(startPos, finPos, i);
            yield return null;
        }
        card.transform.position = finPos;
        LocalGameManager.canClick = true;
        card.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
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

}
