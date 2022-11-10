using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Card
{
    [SerializeField] private GameObject KnifePoint;
    private void Awake()
    {
        cardClass = "Knife";
        hasEaten = false;
    }
    protected override void spawnPoints()
    {
        if (isActivated && hasEaten)
        {
            LocalGameManager.tmpGameObjects.Add(Instantiate(boarderLine, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1), Quaternion.identity));

            LocalGameManager.tmpGameObjects.Add
              (Instantiate(KnifePoint,
              new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
              Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(KnifePoint,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(KnifePoint,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(KnifePoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(KnifePoint,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(KnifePoint,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            if (canGiveMark)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(giveMarkPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity));
            }
        }
        else
        {
            base.spawnPoints();
        }
    }
    public override void MoveTo(Vector3 finPos)
    {
        if (hasEaten && isActivated)
        {
            StartCoroutine(moveToKnife(finPos));
        }
        else
        {
            base.MoveTo(finPos);
        }
    }
    IEnumerator moveToKnife(Vector3 finPos)
    {
        LocalGameManager.canClick = false;
        Vector3 startPos = gameObject.transform.position;
        for (float i = 0; i < 1; i += Time.deltaTime * 10)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, finPos, i);
            yield return null;
        }
        LocalGameManager.activeCard.transform.position = finPos;
        LocalGameManager.canClick = true;
        spawnPoints();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.1f);
        canGiveMark = false;

        yield return new WaitForSeconds(0.3f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f);
        bool thereIsPoint = false;
        foreach(GameObject obj in LocalGameManager.tmpGameObjects)
        {
            if (obj.tag == "Point")
            {
                thereIsPoint = true;
                break;
            }
        }
        if (thereIsPoint == false && GameManeger.myMana == 0)
        {
            SF.pass();
        }
    }
}
