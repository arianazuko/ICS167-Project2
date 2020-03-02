﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;

    [SerializeField] protected Camera player_camera;
    
    protected float speed = 5;
    public int lives = 3;

    public GameObject currentPiece = null;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        minScreenBounds = player_camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = player_camera.ViewportToWorldPoint(new Vector3(1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.phase == 1)
        {
            Move();
            if (currentPiece != null)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Drop();
                }
            }
        }
        if (GameController.instance.phase == 3)
        {
            //insert remove phase functions
            GameController.instance.one_removed = true;
        }
    }

    void Move()
    {
        float x_movement = Input.GetAxis("Horizontal");

        //player_rigidbody.velocity = new Vector2(x_movement * speed, 0);
        this.transform.position = new Vector3(this.transform.position.x + x_movement, this.transform.position.y, this.transform.position.z);

        if (this.transform.position.x < minScreenBounds.x)
        {
            this.transform.position = new Vector3(minScreenBounds.x, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > maxScreenBounds.x)
        {
            this.transform.position = new Vector3(maxScreenBounds.x, this.transform.position.y, this.transform.position.z);
        }
    }

    void Drop()
    {
        currentPiece.GetComponent<Rigidbody2D>().gravityScale = 0.65f;
        currentPiece.transform.parent = null;
        currentPiece = null;
        GameController.instance.phase = 2;
        GameController.instance.turn_counter += 1;
    }
}
