using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : AbilityCard
{
    [SerializeField] private GameObject relayPoint1;

    public override void onClick()
    {
        if (GameManeger.myMana >= 2)
        {
            LocalGameManager.activeCard = gameObject;
            foreach (GameObject card in GameManeger.myCards)
            {
                if (SF.getCardScript(card).isMarked)
                {
                    LocalGameManager.tmpGameObjects.Add(Instantiate(relayPoint1,
                        new Vector3(card.transform.position.x, card.transform.position.y, -1), Quaternion.identity));
                }
            }
        }
    }
    public override void useThisCard()
    {
        flipThisAbilityCard();
        if (isMine)
        {
            Board.useThisAblityCard(0);
        }
    }
}
