using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Card
{
    [SerializeField] private GameObject bowPoint;
    private bool canUseAbility;
    private void Awake()
    {
        canUseAbility = false;
        cardClass = "Bow";
    }
    protected override void spawnPoints()
    {
        if (isActivated)
        {
            LocalGameManager.tmpGameObjects.Add
              (Instantiate(bowPoint,
              new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 2, -1),
              Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - SF.hexUp * 2, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(bowPoint,
                new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y - SF.hexUp, -1),
                Quaternion.identity));
            LocalGameManager.tmpGameObjects.Add
                (Instantiate(bowPoint,
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
