using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int phase = 0;
    //0 = not playing, 1 = player 1, 2 = player 2, 3 = removal
    public int pieces_left = 0;
    public int turn_counter = 0;
    public int turns_to_removal = 8;

    public bool one_removed = false;
    public bool two_removed = false;

    public int player1_lives = 3;
    public int player2_lives = 3;

    [SerializeField] protected List<GameObject> pieces = null;
    [SerializeField] protected GameObject playerone, playertwo;

    [SerializeField] protected GameObject WinnerOneUI, WinnerTwoUI;

    public static GameController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        pieces_left = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pieces_left == 0)
        {
            UpdatePieces();
        }
        if (phase != 3)
        {
            if (turn_counter == turns_to_removal)
            {
                phase = 3;
                turn_counter = 0;
            }
            if (one_removed && two_removed)
            {
                phase = 1;
                one_removed = false;
                two_removed = false;
            }
        }

        if (player1_lives <= 0)
        {
            WinnerTwoUI.SetActive(true);
        }
        if (player2_lives == 0)
        {
            WinnerOneUI.SetActive(true);
        }
    }

    private void UpdatePieces()
    {
        while (pieces_left < 6)
        {
            GameObject a = (GameObject)Instantiate(pieces[Random.Range(0, 100) % pieces.Count]);
            a.transform.SetParent(this.transform);
            a.transform.localPosition = new Vector3(0, -200 + (pieces_left * 100), 0);
            pieces_left += 1;
        }
    }
}
