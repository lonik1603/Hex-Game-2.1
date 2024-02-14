using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ExplosionPoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        PhotonNetwork.Instantiate("Explosion", gameObject.transform.position, Quaternion.identity);
        SF.changeMana(-1);
        if (GameManeger.myMana == 0)
        {
            SF.pass();
        }
        SF.tmpObjListClear();
    }
}
