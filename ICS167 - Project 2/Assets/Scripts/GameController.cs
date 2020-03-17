using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int phase = 0;
    private int saved_phase = 0;
    //0 = not playing, 1 = player 1, 2 = player 2, 3 = player 1 removal, 4 = player 2 removal
    public int pieces_left = 0;
    public int turn_counter = 0;
    public int turns_to_removal = 8;

    public bool one_removed = false;
    public bool two_removed = false;
    public bool removePiecesOnSameTurn = false;

    public bool one_goal = false;
    public bool two_goal = false;

    public int player1_lives = 3;
    public int player2_lives = 3;
    public List<GameObject> player1_lives_ui;
    public List<GameObject> player2_lives_ui;

    [SerializeField] protected List<GameObject> pieces = null;
    [SerializeField] protected GameObject playerone, playertwo;

    [SerializeField] protected GameObject WinnerOneUI, WinnerTwoUI, PauseUI;
    [SerializeField] protected TextMeshProUGUI PhaseText;

    public static GameController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        phase = Random.Range(1, 3);
        pieces_left = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (pieces_left == 0)
        {
            UpdatePieces();
        }
        if (phase != 3 || phase != 4)
        {
            if (turn_counter == turns_to_removal)
            {
                phase = Random.Range(3, 5);
                SFXManagerScript.instance.PlaySFX(3);
                turn_counter = 0;
            }
        }
        if (one_removed && two_removed)
        {
            phase = Random.Range(1, 3);
            SFXManagerScript.instance.PlaySFX(3);
            one_removed = false;
            two_removed = false;
        }

        if (player1_lives <= 0 || two_goal == true)
        {
            phase = 0;
            WinnerTwoUI.SetActive(true);
        }
        if (player2_lives == 0 || one_goal == true)
        {
            phase = 0;
            WinnerOneUI.SetActive(true);
        }
    }

    private void UpdatePieces()
    {
        while (pieces_left < 6)
        {
            GameObject a = (GameObject)Instantiate(pieces[Random.Range(0, 100) % pieces.Count]);
            a.transform.SetParent(this.transform);
            a.transform.localPosition = new Vector3(0, -160 + (pieces_left * 60), 0);
            a.GetComponent<RectTransform>().SetAsFirstSibling();
            pieces_left += 1;
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if ((i + 1) > player1_lives)
            {
                player1_lives_ui[i].SetActive(false);
            }
            else
            {
                player1_lives_ui[i].SetActive(true);
            }
            if ((i + 1) > player2_lives)
            {
                player2_lives_ui[i].SetActive(false);
            }
            else
            {
                player2_lives_ui[i].SetActive(true);
            }
        }
        switch(phase)
        {
            case 1:
                PhaseText.text = "Player 1 Drop!";
                break;
            case 2:
                PhaseText.text = "Player 2 Drop!";
                break;
            case 3:
                PhaseText.text = "Player 1 Remove!";
                break;
            case 4:
                PhaseText.text = "Player 2 Remove!";
                break;
            default:
                PhaseText.text = "";
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Pause()
    {
        saved_phase = phase;
        phase = 0;
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        phase = saved_phase;
        saved_phase = 0;
        PauseUI.SetActive(false);
    }
}
