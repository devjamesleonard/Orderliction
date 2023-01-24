using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] public SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight, opened;
    [SerializeField] public Text piece;
    [SerializeField] public char letter;
    [SerializeField] private GameObject token;
    [SerializeField] private bool over;
    [SerializeField] private Token hovert;
    [SerializeField] private GridManager Game;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Game = GetComponentInParent<GridManager>();
        letter = ' ';
    }
    public char getLetter()
    {
        return letter;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Contains("TokenG"))
        {
            over = true;
            hovert = collision.gameObject.GetComponent<Token>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("TokenG"))
        {
            over = false;
            hovert = new Token();

        }
    }
  
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TokenG")
        {
            Token t = collision.gameObject.GetComponent<Token>();
            letter = t.c;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name);
        if (collision.gameObject.name == "TokenG")
        {
            Token t = collision.gameObject.GetComponent<Token>();
            letter = t.c;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TokenG")
        {
            Token t = collision.gameObject.GetComponent<Token>();
            letter = t.c;
        }
    }
    */
    public void openword()
    {
        opened.SetActive(true);
    }
    private void OnMouseOver()
    {
        
    }
    public void OnMouseDown()
    {
        GameObject a = Instantiate(token, gameObject.transform);
        Token t = a.GetComponent<Token>();
        t.c = letter;
        letter = ' ';

    }
   
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        piece.text = letter + "";
        if(Input.GetMouseButtonUp(0) && over && Game._game.gameRunning)
        {
            over = false;
            letter = hovert.c;
            Game._game.checkWords();
            

        }
    }
}
