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
        if (this.gameObject.tag == "remove" &&
        (GameController.instance.phase != 3 && GameController.instance.phase != 4))
        {
            SFXManagerScript.instance.PlaySFX(3);
            Destroy(this.gameObject);
        }
    }

    void OnMouseDown()
    {
        if (player == 1)
        {
            if (GameController.instance.phase == 4 && !GameController.instance.removePiecesOnSameTurn)
            {
                GameController.instance.one_removed = true;
                GameController.instance.phase = 3;
                SFXManagerScript.instance.PlaySFX(3);
                Destroy(this.gameObject);
            }
            else
            {
                GameController.instance.one_removed = true;
                GameController.instance.phase = 3;
                this.gameObject.tag = "remove";

                int children = transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                    Color spriteColor = child.color;
                    spriteColor.a = .5f;
                    child.color = spriteColor;
                }
            }
        }
        else if (player == 2)
        {
            if (GameController.instance.phase == 3 && !GameController.instance.removePiecesOnSameTurn)
            {
                GameController.instance.two_removed = true;
                GameController.instance.phase = 4;
                SFXManagerScript.instance.PlaySFX(3);
                Destroy(this.gameObject);
            }
            else
            {
                GameController.instance.two_removed = true;
                GameController.instance.phase = 4;
                this.gameObject.tag = "remove";

                int children = transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                    Color spriteColor = child.color;
                    spriteColor.a = .5f;
                    child.color = spriteColor;
                }
            }
        }
    }

    void OnMouseOver()
    {
        if (player == 1)
        {
            if (GameController.instance.phase == 4)
            {
                int children = transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                    Color spriteColor = child.color;
                    spriteColor.a = .5f;
                    child.color = spriteColor;
                }
            }
        }
        else if (player == 2)
        {
            if (GameController.instance.phase == 3)
            {
                int children = transform.childCount;
                for (int i = 0; i < children; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                    Color spriteColor = child.color;
                    spriteColor.a = .5f;
                    child.color = spriteColor;
                }
            }
        }
    }

    void OnMouseExit()
    {
        if (gameObject.tag != "remove")
        {
            int children = transform.childCount;
            for (int i = 0; i < children; i++)
            {
                SpriteRenderer child = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                        Color spriteColor = child.color;
                        spriteColor.a = 1f;
                        child.color = spriteColor;
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
