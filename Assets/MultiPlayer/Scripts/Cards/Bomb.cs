using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bomb : Card
{
    [SerializeField] private GameObject explosionPoint;
    private void Awake()
    {
        cardClass = "Bomb";
    }
    protected override void spawnPoints()
    {
        base.spawnPoints();
        if(isActivated)
        {
            if(!canGiveMark)
            {
                LocalGameManager.tmpGameObjects.Add(Instantiate(explosionPoint,
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity));
            }
        }
    }
}
