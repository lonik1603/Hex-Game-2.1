using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Card
{
    [SerializeField] private GameObject bowPoint;

    private GameObject newBowPoint;
    private void Awake()
    {
        canUseAbility = false;
        cardClass = "Bow";

    }
    public override void spawnPoints()
    {
        if (isActivated)
        {
            LocalGameManager.tmpGameObjects.Add(Instantiate(boarderLine, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4), Quaternion.identity));
            if (LocalGameManager.isBlue)
            {
                newBowPoint = Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
                Quaternion.identity);
                LocalGameManager.tmpGameObjects.Add(newBowPoint);
                newBowPoint.GetComponent<BowPoint>().bowPointCount = 1;

            }
            else
            {
                LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
                Quaternion.identity));
            }
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            if (LocalGameManager.isBlue)
            {
                LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                Quaternion.identity));
            }
            else
            {
                newBowPoint = Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                Quaternion.identity);
                LocalGameManager.tmpGameObjects.Add(newBowPoint);
                newBowPoint.GetComponent<BowPoint>().bowPointCount = 1;
            }
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(point,
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
