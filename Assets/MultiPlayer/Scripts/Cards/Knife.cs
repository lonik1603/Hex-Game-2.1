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
}
