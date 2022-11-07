using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChoiceButton : MonoBehaviour
{
    [SerializeField] private GameObject boarderline;
    private void OnMouseDown()
    {
        SF.tmpObjListClear();
        LocalGameManager.tmpGameObjects.Add(Instantiate(boarderline, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4), Quaternion.identity));
        CardsChoiseStage.choisenClass = gameObject.tag;
        CardsChoiseStage.pressedButton = gameObject;
        CardsChoiseStage.activateCardPlaces();
    }
}
