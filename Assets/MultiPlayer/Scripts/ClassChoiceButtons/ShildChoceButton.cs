using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildChoceButton : CardsChoiseStage
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        choisenClass = "Shild";
        pressedButton = gameObject;
        activateCardPlaces();
    }
}
