using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPoint : MonoBehaviour
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
        LocalGameManager.activeCard.GetComponent<Checker>().useThisCard();
        LocalGameManager.activeCard.GetComponent<Checker>().showThisCard(thisCard);
        SF.changeMana(-1);
        if(GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }
}
