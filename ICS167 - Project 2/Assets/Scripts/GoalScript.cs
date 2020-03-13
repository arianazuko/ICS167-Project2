using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField] protected GameObject player;

    private bool timer_started = false;
    private float elapsed = 0.0f;
    private float required = 2.0f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Block")
            timer_started = true;
        /*if (other.tag == "Block")
        {
            if (player.name == "Player1")
            {
                if (player.GetComponent<Player1Controller>().currentPiece != other.gameObject)
                {
                    timer_started = true;
                }
            }
            else if (player.name == "Player2")
            {
                if (player.GetComponent<Player2Controller>().currentPiece != other.gameObject)
                {
                    timer_started = true;
                }
            }
        }
        */
    }

    void OnTriggerExit2D(Collider2D other)
    {
        timer_started = false;
        elapsed = 0.0f;
    }

    void Update()
    {
        if (timer_started)
        {
            elapsed += Time.deltaTime;
        }
        if (elapsed >= required)
        {
            if (player.name == "Player1")
            {
                GameController.instance.one_goal = true;
            }
            else if (player.name == "Player2")
            {
                GameController.instance.two_goal = true;
            }
        }
    }
}
