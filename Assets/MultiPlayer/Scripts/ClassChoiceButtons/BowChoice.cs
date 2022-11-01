using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowChoice : CardsChoiseStage
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        choisenClass = "Bow";
        pressedButton = gameObject;
        activateCardPlaces();
    }
}
