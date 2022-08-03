using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CardPlace : CardsChoiseStage
{
    private void OnMouseDown()
    {
        if(LocalGameManager.isBlue)
        {
            if(gameObject.transform.position.x != 0)
            {
                PhotonNetwork.Instantiate("Blue" + choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (-7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (-7)), Quaternion.Euler(-90,0,180));

            }
            else
            {
                PhotonNetwork.Instantiate("Blue" + choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (-7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + choisenClass,
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));
            }


            if(gameObject.transform.position.x == -9)
            {
                PhotonNetwork.Instantiate("Blue" + choisenClass + "Button",
                new Vector3(-12, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }
            else if(gameObject.transform.position.x == 0)
            {
                PhotonNetwork.Instantiate("Blue" + choisenClass + "Button",
                new Vector3(-8, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }
            else if (gameObject.transform.position.x == 9)
            {
                PhotonNetwork.Instantiate("Blue" + choisenClass + "Button",
                new Vector3(-4, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }

        }
        else
        {
            if(gameObject.transform.position.x != 0)
            {
                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

            }
            else
            {
                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));
            }

            if (gameObject.transform.position.x == -9)
            {
                PhotonNetwork.Instantiate("Red" + choisenClass + "Button",
                new Vector3(4, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }
            else if (gameObject.transform.position.x == 0)
            {
                PhotonNetwork.Instantiate("Red" + choisenClass + "Button",
                new Vector3(8, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }
            else if (gameObject.transform.position.x == 9)
            {
                PhotonNetwork.Instantiate("Red" + choisenClass + "Button",
                new Vector3(12, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = choisenClass;

            }
        }



        disableCardPlaces();

        Destroy(pressedButton);
        createdChoiceButtons.Remove(pressedButton);

        diactivateClassChoiceButtons();
        SF.tmpObjListClear();

        SF.choiceStagePass();
        turnCount++;
        if (!LocalGameManager.isBlue && turnCount == 3)
        {
            Debug.Log("end of class choise stage");
            nextStage();
        }
        else
        {
            CardsChoiseStage.nextTurn();
        }

        cardPlaces.Remove(gameObject);
        Destroy(gameObject);

    }
}
