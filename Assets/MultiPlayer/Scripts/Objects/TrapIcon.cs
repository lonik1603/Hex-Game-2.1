using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TrapIcon : MonoBehaviour
{
    [SerializeField] private GameObject redStun;
    [SerializeField] private GameObject blueStun;

    public static List<GameObject> myStuns;
    private PhotonView pView;
    void Start()
    {
        pView = GetComponent<PhotonView>();
        if (LocalGameManager.isBlue == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if(pView.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {        
        if(SF.cardClassList.Contains(other.tag))
        {
            if(SF.getCardScript(other.gameObject).pView.IsMine != pView.IsMine && SF.getCardScript(other.gameObject).pView.IsMine)
            {
                if(SF.getCardScript(other.gameObject).cardIsBlue)
                {
                    SF.getCardScript(other.gameObject).stunThisCard("redStun");
                }
                else
                {
                    SF.getCardScript(other.gameObject).stunThisCard("blueStun");
                }
            }
        }
    }

}
