using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool thisCardIsBlue;
    public string thisCardClass;
    public bool thisCardIsActivated;
    private GameObject otherCard;
    private Object activeCardScript;

    [SerializeField] private GameObject point;

    private void Start()
    {
        if (gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.y > 13)
        {
            gameObject.SetActive(false);
        }
        activeCardScript = LocalGameManager.activeCard.GetComponent<Bomb>();
    }
    private void OnMouseDown()
    {
        LocalGameManager.activeCard.transform.position = new Vector3(LocalGameManager.activeCard.transform.position.x, LocalGameManager.activeCard.transform.position.y, -1);
        SF.changeMana(-1);
        SF.getCardScript(LocalGameManager.activeCard).MoveTo(gameObject.transform.position);
        SF.tmpObjListClear();
    }
}
