using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    public Transform _cam;

    [SerializeField] public GamestateHandler _game;

    private Dictionary<Vector2, Tile> _tiles;
    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for(int x=0; x < _width; x++)
        {
            for(int y = 0; y<=_height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity,gameObject.transform);
                spawnedTile.name = $"Tile {x} {y}";
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                _tiles[new Vector2(x, y)] = spawnedTile;
                
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height - 5.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
    public void StartGame(){
      while (transform.childCount > 0) {
    DestroyImmediate(transform.GetChild(0).gameObject);
}

GenerateGrid();

        _game.tiles = new Tile[_game.words.Length / 2][];
      //  Debug.Log(_game.tiles.Length + "hello");
        /*
        _game.tiles[0] = new Tile[6];
        Debug.Log(_game.tiles.Length);
       */
        for (int i = 0; i<_game.words.Length; i+=2)
        {
            
            if (_game.words[i].x != _game.words[i + 1].x)
            {
                //Debug.Log("gay + " + ((int)_game.words[i + 1].x - (int)_game.words[i].x));
                int temp = (int)_game.words[i + 1].x - (int)_game.words[i].x;
             
                _game.tiles[i/2] = new Tile[temp];
                //Debug.Log("porn " + _game.tiles[i/2].Length);

            }
            else
            {
                //Debug.Log("gay + " + ((int)_game.words[i + 1].y - (int)_game.words[i].y));
                int temp = (int)_game.words[i + 1].y - (int)_game.words[i].y;

                _game.tiles[i] = new Tile[temp];
                //Debug.Log("porn "+_game.tiles[i].Length);

            }
        }
   
        
        for (int i = 0; i < _game.words.Length; i += 2)
        {
       
            //Debug.Log(i / 2);
            if (_game.words[i].x != _game.words[i + 1].x)
            {
              
                for (int j = 0; j < _game.words[i + 1].x - _game.words[i].x; j++)
                {
                    
                    _game.tiles[i / 2][j] = _game.grid.GetTileAtPosition(new Vector2(_game.words[i].x + j, _game.words[i].y));
                     _game.tiles[i / 2][j].openword();
                   // Debug.Log(new Vector2(_game.words[i].x, _game.words[i].y + j));
                  //  GetTileAtPosition(new Vector2(_game.words[i].x + j, _game.words[i].y )).openword();
                    //Debug.Log("a");
                }
            }
            else
            {
                for (int j = 0; j < _game.words[i + 1].y - _game.words[i].y; j++)
                {
                     _game.tiles[i / 2][j] = _game.grid.GetTileAtPosition(new Vector2(_game.words[i].x, _game.words[i+1].y - j-1));
                    // GetTileAtPosition(new Vector2(_game.words[i].x, _game.words[i].y + j)).openword();
                    _game.tiles[i / 2][j].openword();
                   // Debug.Log(new Vector2(_game.words[i].x, _game.words[i].y + j));
                   /*
                    * for (int j = (int)(_game.words[i + 1].y - _game.words[i].y)-1; j >= 1; j--)
                {
                     _game.tiles[i / 2][j] = _game.grid.GetTileAtPosition(new Vector2(_game.words[i].x, _game.words[i].y + (_game.words[i + 1].y - _game.words[i].y - j)));
                    // GetTileAtPosition(new Vector2(_game.words[i].x, _game.words[i].y + j)).openword();
                    _game.tiles[i / 2][j].openword();
                   // Debug.Log(new Vector2(_game.words[i].x, _game.words[i].y + j));

                } 
                    */ 
                }
            }
        }
    }
    private void Start()
    {
        
        
    }
}
