using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint2 : RelayPoint1
{
    private GameObject thisCard;
    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            thisCard = other.gameObject;
        }
    }
    private void OnMouseDown()
    {
        SF.getCardScript(cardToSwap).isMarked = false;
        SF.getCardScript(thisCard).isMarked = true;

        SF.getCardScript(cardToSwap).GetComponent<Renderer>().material = SF.getCardScript(cardToSwap).defaultMaterial;
        SF.getCardScript(thisCard).GetComponent<Renderer>().material = SF.getCardScript(thisCard).markedMterial;

        LocalGameManager.activeCard.GetComponent<Relay>().useThisCard();
        SF.changeMana(-1);
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }
}
