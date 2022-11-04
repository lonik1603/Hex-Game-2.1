using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChoiceButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        CardsChoiseStage.choisenClass = gameObject.tag;
        CardsChoiseStage.pressedButton = gameObject;
        CardsChoiseStage.activateCardPlaces();
    }
}
