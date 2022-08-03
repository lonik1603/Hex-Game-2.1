using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsChoiceButton : CardsChoiseStage
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        choisenClass = "Boots";
        pressedButton = gameObject;
        activateCardPlaces();
    }
}
