using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public int player_num;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.tag == "Block")
        {
            if (player_num == 1)
            {
                GameController.instance.player1_lives -= 1;
            }
            else if (player_num == 2)
            {
                GameController.instance.player2_lives -= 1;
            }
            SFXManagerScript.instance.PlaySFX(1);
            Destroy(other.gameObject);
        }
    }

}
