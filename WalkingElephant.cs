using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingElephant : MonoBehaviour
{
    public bool x;
    public bool z;
    private float m_distanceTraveled = 0f;
    public float distance;
    public bool negative;
    private Vector3 position;
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        speed = Random.Range(1, 10);
    }
    public void RepositionStraight(float n, float y)
    {
        if (z)
        {
            if (m_distanceTraveled > n)
            {

                Vector3 oldPosition = transform.position;

                transform.Translate(0, 0, 4 * y * Time.deltaTime);

                m_distanceTraveled += Vector3.Distance(oldPosition, transform.position);



            }
            else
            {
                Vector3 oldPosition = transform.position;

                transform.Translate(0, 0, -4 * y * Time.deltaTime);

                m_distanceTraveled -= Vector3.Distance(oldPosition, transform.position);
            }
        }
        else if (!z)
        {
            if (m_distanceTraveled > n)
            {

                Vector3 oldPosition = transform.position;

                transform.Translate(4 * y * Time.deltaTime, 0, 0);

                m_distanceTraveled += Vector3.Distance(oldPosition, transform.position);



            }
            else
            {
                Vector3 oldPosition = transform.position;

                transform.Translate(-4 * y * Time.deltaTime, 0, 0);

                m_distanceTraveled -= Vector3.Distance(oldPosition, transform.position);
            }
        }



    }
    public void refresh()
    {
        m_distanceTraveled = 0f;
        transform.position = position;
        speed = Random.Range(0, 10);
    }
    // Update is called once per frame
    void Update()
    {
        if (negative)
        {
            RepositionStraight(-30, speed);
        }
        else
        {
            RepositionStraight(30, -1 * speed);
        }
        if (!x)
        {
            if (negative)
            {
                if (transform.position.y < distance)
                {
                    refresh();
                }
            }
            else
            {
                if (transform.position.y > distance)
                {

                    refresh();
                }
            }
        }
        else if (x)
        {
            if (negative)
            {
                if (transform.position.x < distance)
                {

                    refresh();
                }
            }
            else
            {
                if (transform.position.x > distance)
                {

                    refresh();
                }
            }
        }
    }
}
