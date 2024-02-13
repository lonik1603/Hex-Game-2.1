using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : AbilityCard
{
    [SerializeField] private GameObject checkerPoint;

    public override void onClick()
    {
        if (GameManeger.myMana > 0)
        {
            LocalGameManager.activeCard = gameObject;
            foreach (GameObject card in GameManeger.enemyCards)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(checkerPoint,
                    new Vector3(card.transform.position.x, card.transform.position.y, -1), Quaternion.identity));
            }
        }
    }
    public override void useThisCard()
    {
        flipThisAbilityCard();
        if (isMine)
        {
            Board.useThisAblityCard(1);
        }
    }
    public void showThisCard(GameObject card)
    {
        if (SF.getCardScript(card).isMarked)
        {
            SF.getCardScript(card).GetComponent<Renderer>().material = SF.getCardScript(card).markedMterial;
            StartCoroutine(defaultMaterialCur(card));
        }
        if (SF.getCardScript(card).isCorrupted)
        {
            SF.getCardScript(card).GetComponent<Renderer>().material = SF.getCardScript(card).corruptedMterial;
            StartCoroutine(defaultMaterialCur(card));
        }
    }
    IEnumerator defaultMaterialCur(GameObject card)
    {
        yield return new WaitForSeconds(4);
        SF.getCardScript(card).GetComponent<Renderer>().material = SF.getCardScript(card).defaultMaterial;
    }
}
