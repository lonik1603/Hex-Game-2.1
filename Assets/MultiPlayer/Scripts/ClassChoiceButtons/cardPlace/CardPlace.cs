using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CardPlace : MonoBehaviour
{
    private void OnMouseDown()
    {

        if(LocalGameManager.isBlue)
        {
            if(gameObject.transform.position.x != 0)
            {
                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (-7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (-7)), Quaternion.Euler(-90,0,180));

            }
            else
            {
                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (-7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass,
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (-6)), Quaternion.Euler(-90,0,-180));
            }


            if(gameObject.transform.position.x == -9)
            {
                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(-12, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }
            else if(gameObject.transform.position.x == 0)
            {
                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(-8, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }
            else if (gameObject.transform.position.x == 9)
            {
                PhotonNetwork.Instantiate("Blue" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(-4, -16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }

        }
        else
        {
            if(gameObject.transform.position.x != 0)
            {
                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

            }
            else
            {
                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x - 3, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x, SF.hexUp * (7)), Quaternion.Euler(-90,0,-180));

                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass, 
                new Vector3(gameObject.transform.position.x + 3, SF.hexUp * (6)), Quaternion.Euler(-90,0,-180));
            }

            if (gameObject.transform.position.x == -9)
            {
                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(4, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }
            else if (gameObject.transform.position.x == 0)
            {
                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(8, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }
            else if (gameObject.transform.position.x == 9)
            {
                PhotonNetwork.Instantiate("Red" + CardsChoiseStage.choisenClass + "Button",
                new Vector3(12, 16, 0), Quaternion.Euler(0, 180, -90)).GetComponent<ActButton>().cardClass = CardsChoiseStage.choisenClass;

            }
        }



        CardsChoiseStage.disableCardPlaces();

        Destroy(CardsChoiseStage.pressedButton);
        CardsChoiseStage.createdChoiceButtons.Remove(CardsChoiseStage.pressedButton);

        CardsChoiseStage.diactivateClassChoiceButtons();
        SF.tmpObjListClear();

        SF.choiceStagePass();
        CardsChoiseStage.turnCount++;
        if (!LocalGameManager.isBlue && CardsChoiseStage.turnCount == 3)
        {
            Debug.Log("end of class choise stage");
            CardsChoiseStage.nextStage();
        }
        else
        {
            CardsChoiseStage.nextTurn();
        }

        CardsChoiseStage.cardPlaces.Remove(gameObject);
        Destroy(gameObject);

    }
}
