using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceButtonScript : MonoBehaviour
{
    [SerializeField] protected GameObject piece;

    public void MakePiece()
    {
        if (GameController.instance.phase == 1)
        {
            GameObject p = (GameObject)Instantiate(piece);
            GameObject player = GameObject.Find("Player1");
            p.transform.SetParent(player.transform);
            p.transform.localPosition = new Vector3(0, 0, 0);

            GameController.instance.pieces_left -= 1;
            Destroy(this.gameObject);
        }
        else if (GameController.instance.phase == 2)
        {
            GameObject p = (GameObject)Instantiate(piece);
            GameObject player = GameObject.Find("Player2");
            p.transform.SetParent(player.transform);
            p.transform.localPosition = new Vector3(0, 0, 0);

            GameController.instance.pieces_left -= 1;
            Destroy(this.gameObject);
        }
        else
            return;
    }

    public void mouseEnter()
    {
        Color button = gameObject.GetComponent<Image>().color;
        button.a = 0.8f;
        gameObject.GetComponent<Image>().color = button;
    }

    public void mouseExit()

    {
        Color button = gameObject.GetComponent<Image>().color;
        button.a = 1f;
        gameObject.GetComponent<Image>().color = button;
    }
}
