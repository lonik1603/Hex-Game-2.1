using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Boots : Card
{
    private bool canUseAbility;
    private void Awake()
    {
        canUseAbility = false;
        cardClass = "Boots";
    }    
    public override void cardCheck()
    {
        canUseAbility = true;
        base.cardCheck();
    }

    protected override void spawnPoints()
    {
        if (isActivated && canUseAbility)
        {
            if (canMove)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(boarderLine, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4), Quaternion.identity));

                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * 4, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp * 3, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x + 6, gameObject.transform.position.y + SF.hexUp * 2, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x + 6, gameObject.transform.position.y + SF.hexUp * 0, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x + 6, gameObject.transform.position.y + SF.hexUp * -2, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y + SF.hexUp * -3, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + SF.hexUp * -4, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp * -3, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x - 6, gameObject.transform.position.y + SF.hexUp * -2, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x - 6, gameObject.transform.position.y + SF.hexUp * 0, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x - 6, gameObject.transform.position.y + SF.hexUp * 2, -1),
                    Quaternion.identity));
                LocalGameManager.tmpGameObjects.Add
                    (Instantiate(point,
                    new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + SF.hexUp * 3, -1),
                    Quaternion.identity));
                if (canGiveMark)
                {
                    LocalGameManager.tmpGameObjects.Add(Instantiate(giveMarkPoint,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity));
                }
            }
        }
        else
        {
            base.spawnPoints();
        }
    }
    public override void MoveTo(Vector3 finPos)
    {
        canUseAbility = false;
        base.MoveTo(finPos);
    }
}
