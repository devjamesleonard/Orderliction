using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    public Text p;
    public char c;
    Vector3 start, end;
    void Start()
    {
        start = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
 
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(
            Camera.main.pixelWidth, Camera.main.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(Mathf.Clamp(end.x, cameraRect.xMin, cameraRect.xMax), Mathf.Clamp(end.y, cameraRect.yMin, cameraRect.yMax), gameObject.transform.position.z);

        p.text = c + "";
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }

    
}
