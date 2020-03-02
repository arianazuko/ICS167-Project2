using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.name == "Player1")
            this.transform.parent.GetComponent<Player1Controller>().currentPiece = this.gameObject;
        else if (this.transform.parent.name == "Player2")
            this.transform.parent.GetComponent<Player2Controller>().currentPiece = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
