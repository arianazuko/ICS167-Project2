using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private int player;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.name == "Player1")
        {
            this.transform.parent.GetComponent<Player1Controller>().currentPiece = this.gameObject;
            player = 1;
        }
        else if (this.transform.parent.name == "Player2")
        {
            this.transform.parent.GetComponent<Player2Controller>().currentPiece = this.gameObject;
            player = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (player == 1)
        {
            if (GameController.instance.phase == 4)
            {
                GameController.instance.one_removed = true;
                GameController.instance.phase = 3;
                SFXManagerScript.instance.PlaySFX(3);
                Destroy(this.gameObject);
            }
        }
        else if (player == 2)
        {
            if (GameController.instance.phase == 3)
            {
                GameController.instance.two_removed = true;
                GameController.instance.phase = 4;
                SFXManagerScript.instance.PlaySFX(3);
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            SFXManagerScript.instance.PlaySFX(0);
        }
    }
}
