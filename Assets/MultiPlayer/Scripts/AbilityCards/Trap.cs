using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : AbilityCard
{
    [SerializeField] private GameObject trapPoint;
    public override void onClick()
    {
        if (GameManeger.myMana > 1)
        {
            LocalGameManager.activeCard = gameObject;
            for (int y = -3; y <= 4; y++)
            {
                for(int x = -2; x <= 2; x++)
                {
                    LocalGameManager.tmpGameObjects.Add(Instantiate(trapPoint, new Vector3(x * 6 , y * 2 *SF.hexUp - SF.hexUp, -1), Quaternion.identity));
                }
            }
            for (float y = -3; y <= 3; y++)
            {
                for (int x = -1; x <= 2; x++)
                {
                    LocalGameManager.tmpGameObjects.Add(Instantiate(trapPoint, new Vector3(x * 6 - 3, y * 2 * SF.hexUp, -1), Quaternion.identity));
                }
            }
        }
    }
    public override void useThisCard()
    {
        flipThisAbilityCard();
        if (isMine)
        {
            Board.useThisAblityCard(2);
        }
    }
}
