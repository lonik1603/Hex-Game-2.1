using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChoiceButton : CardsChoiseStage
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        choisenClass = "Bomb";
        pressedButton = gameObject;
        activateCardPlaces();
    }
}
