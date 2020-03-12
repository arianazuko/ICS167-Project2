using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;

    [SerializeField]
    protected Camera player_camera;
    private List<GameObject> tower = new List<GameObject>();

    protected float speed = 5;
    public float spinSpeed = 5f;
    public bool rigidSpinning = true;

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
        if (GameController.instance.phase == 2)
        {
            AdjustHeight();
            Move();
            if (currentPiece != null)
            {
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    Drop();
                }
            }
        }
        if (GameController.instance.phase == 3)
        {
            //insert remove phase functions
            GameController.instance.two_removed = true;
        }
    }

    void Move()
    {
        float x_movement = Input.GetAxis("Horizontal2");

        //continuous spinning
        if (Input.GetAxisRaw("Vertical2") != 0 && !rigidSpinning)
        {
            this.transform.Rotate(0, 0, spinSpeed * Input.GetAxisRaw("Vertical2"));
        }

        //rigid spinning
        if (Input.GetKeyDown(KeyCode.UpArrow) && rigidSpinning)
        {
            this.transform.Rotate(0, 0, 45);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && rigidSpinning)
        {
            this.transform.Rotate(0, 0, -45);
        }

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
        tower.Add(currentPiece);
        currentPiece = null;
        this.transform.rotation = Quaternion.identity;
        GameController.instance.phase = 2;
        GameController.instance.turn_counter += 1;
    }

    void AdjustHeight()
    {
        if (tower.Count != 0)
        {
            GameObject tallest = tower[0];
            foreach (GameObject piece in tower)
            {
                if (piece.transform.position.y > tallest.transform.position.y)
                {
                    tallest = piece;
                }
            }
            if ((this.transform.position.y - tallest.transform.position.y) < 2.0f)
            {
                this.transform.position += Vector3.up;
                player_camera.transform.position += Vector3.up;
            }
        }
    }

}