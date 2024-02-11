using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MarkPoint : MonoBehaviour
{

    private GameObject card;


    private void OnTriggerEnter(Collider other)
    {
        if(SF.cardClassList.Contains(other.gameObject.tag))
        card = other.gameObject;
    }

    private void OnMouseDown()
    {
        SF.getCardScript(card).markCard();

        LocalGameManager.marksCount++;
        if (LocalGameManager.marksCount == 3)
        {
            SF.tmpObjListClear();
            MarksChoseStage.playerHasMarked();
        }
        Destroy(gameObject);
    }
}
