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
        if(gameObject.transform.position.x > 14 || gameObject.transform.position.x < -14 || gameObject.transform.position.y < -13 || gameObject.transform.position.x > 13)
        {
            gameObject.SetActive(false);
        }
        activeCardScript = LocalGameManager.activeCard.GetComponent<Bomb>();
    }
    private void OnMouseDown()
    {
        SF.tmpObjListDisable();
        SF.changeMana(-1);
        LocalGameManager.activeCard.transform.position = new Vector3(LocalGameManager.activeCard.transform.position.x, LocalGameManager.activeCard.transform.position.y, -1);
        StartCoroutine(MoveTo(LocalGameManager.activeCard, LocalGameManager.activeCard.transform.position, gameObject.transform.position));
    }

    IEnumerator MoveTo(GameObject card, Vector3 startPos, Vector3 finPos)
    {
        for (float i = 0; i < 1; i += Time.deltaTime * 5)
        {
            card.transform.position = Vector3.Lerp(startPos, finPos, i);
            yield return null;
        }
        LocalGameManager.activeCard.transform.position = finPos;
        card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, 0);
        yield return new WaitForSeconds(0.1f);

        if (GameManeger.myMana > 0)
        {
            SF.getCardScript(LocalGameManager.activeCard).tryToSpawnPoints();
        }
        else
        {
            SF.tmpObjListClear();
        }
    }


}
