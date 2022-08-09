using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : MonoBehaviour
{
    protected bool canBeUsed;
    protected bool isMine;

    void Start()
    {
        isMine = false;
        canBeUsed = true;
        if(LocalGameManager.isBlue == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(90, 0, 180);
        }

        if (LocalGameManager.isBlue && gameObject.transform.position.y < 0 
            || !LocalGameManager.isBlue && gameObject.transform.position.y > 0)
        {
            isMine = true;
        }
    }
    protected void OnMouseDown()
    {
        if (SF.isMyTurn() && canBeUsed && isMine)
        {
            SF.tmpObjListClear();
            onClick();
        }
    }
    public virtual void onClick()
    {

    }
    public virtual void useThisCard()
    {

    }

    public void flipThisAbilityCard()
    {
        canBeUsed = false;
        StartCoroutine(flipThisAbilityCardCur());
    }

    IEnumerator flipThisAbilityCardCur()
    {
        float x = Mathf.Abs(gameObject.transform.rotation.eulerAngles.x);
        float y = Mathf.Abs(gameObject.transform.rotation.eulerAngles.y);
        float z = Mathf.Abs(gameObject.transform.rotation.eulerAngles.z);
        if (LocalGameManager.isBlue)
        {
            if (z == 0f)
            {
                while (z != 180f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    z += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, -180f);
            }
            else if (z == 180f)
            {
                while (z != 360f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    yield return new WaitForSeconds(0.01f);
                    z += 5f;
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, 0f);
            }
        }
        else
        {
            if (z == 0)
            {
                while (z != 180)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    z += 5;
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, -180);
            }
            else if (z == 180f)
            {
                while (z != 360)
                {
                    gameObject.transform.rotation = Quaternion.Euler(x, y, z);
                    z += 5;
                    yield return new WaitForSeconds(0.01f);
                }
                gameObject.transform.rotation = Quaternion.Euler(x, y, 0);
            }

        }
    }

}
