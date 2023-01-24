using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letterbox : MonoBehaviour
{
    public GamestateHandler Game;
    public TextMesh Piece;
    // Start is called before the first frame update
    void Start()
    {
   
        Piece.text = "" + Game.choices[(Game.count)];
        Game.count++;
    }


    public void pressed()
    {
       // Debug.Log("a");
       

    }
    private void OnMouseDown()
    {
        GameObject a = Instantiate(Game.token, Game.gameObject.transform);
        Token t = a.GetComponent<Token>();
        t.c = Piece.text[0];
    }
    
    // Update is called once per frame
    void Update()
    {

        if(Game.deleteLetterBoxes){
            Destroy(gameObject);
        }
        
    }
}
