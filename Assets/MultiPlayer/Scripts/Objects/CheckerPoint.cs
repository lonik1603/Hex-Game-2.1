using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPoint : MonoBehaviour
{
    private static int checkerCount;
    private GameObject thisCard;

    private void Start()
    {
        checkerCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SF.cardClassList.Contains(other.gameObject.tag))
        {
            thisCard = other.gameObject;
        }
    }
    private void OnMouseDown()
    {
        checkerCount += 1;
        LocalGameManager.activeCard.GetComponent<Checker>().showThisCard(thisCard);
        if (checkerCount == 1)
        {
            gameObject.SetActive(false);
            LocalGameManager.canClick = false;
        }
        else if (checkerCount == 2)
        {
            LocalGameManager.canClick = true;
            LocalGameManager.activeCard.GetComponent<Checker>().useThisCard();
            SF.changeMana(-2);
            if (GameManeger.myMana == 0)
            {
                SF.pass();
            }
            SF.tmpObjListClear();
        }
    }
}
