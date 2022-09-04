using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBoost : AbilityCard
{
    public override void onClick()
    {
        if (GameManeger.myMana > 0)
        {
            SF.manaBoostTurns = 3;
            useThisCard();
            SF.changeMana(-2);
            if(GameManeger.myMana == 0)
            {
                SF.pass();
            }
        }
    }
    public override void useThisCard()
    {
        flipThisAbilityCard();
        if (isMine)
        {
            Board.useThisAblityCard(3);
        }
    }
}
