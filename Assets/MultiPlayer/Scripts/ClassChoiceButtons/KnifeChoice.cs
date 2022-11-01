using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeChoice : CardsChoiseStage
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        choisenClass = "Knife";
        pressedButton = gameObject;
        activateCardPlaces();
    }
}
