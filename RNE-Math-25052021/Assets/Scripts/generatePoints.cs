using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class generatePoints : MonoBehaviour
{
    [SerializeField] private GameObject cubePoint;
    [SerializeField] private GameObject linePoint;
    
    [SerializeField] private Camera myCamera;

    [SerializeField] [Range(0.0f, 1.0f)] private float u;
    [SerializeField] [Range(0.0f, 1.0f)] private float v;

    private List<GameObject> _curvePoints = new List<GameObject>();
    private int _prevSize = 0;


    Color generateColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
    }
    void drawLine(Vector3 start, Vector3 end, Color color)
    {
        var yDiff = end.y - start.y;
        var xDiff = end.x - start.x;
        
        
        var m = (yDiff)/(xDiff);
        var b = -(m * start.x) + start.y;


        if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
        {
            print("X supérieur");
            if (start.x < end.x)
            {
                for (int i = (int) start.x; i < end.x; i++)
                {
                    var a = Instantiate(linePoint, new Vector3(i, i*m+b, start.z), Quaternion.identity);
                    a.GetComponent<Renderer>().material.color = color;
                }
            }
            else
            {
                for (int i = (int) start.x; i > end.x; i--)
                {
                    var a = Instantiate(linePoint, new Vector3(i, i*m+b, start.z), Quaternion.identity);
                    a.GetComponent<Renderer>().material.color = color;
                }
            }
        }
        else if(Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            
            print("Y supérieur");
            if (start.y < end.y)
            {
                for (int i = (int) start.y; i < end.y; i++)
                {
                    var a = Instantiate(linePoint, new Vector3((-b+i)/m, i, start.z), Quaternion.identity);
                    a.GetComponent<Renderer>().material.color = color;
                }
            }
            else
            {
                for (int i = (int) start.y; i > end.y; i--)
                {
                    var a = Instantiate(linePoint, new Vector3((-b+i)/m, i , start.z), Quaternion.identity);
                    a.GetComponent<Renderer>().material.color = color;
                }
            }
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (u > v)
        {
            var tmp = v;
            v = u;
            u = tmp;
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            cubePoint = Instantiate(cubePoint);
            print(Input.mousePosition);
            cubePoint.transform.position = myCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, 50.0f));
            _curvePoints.Add(cubePoint);
        }

        if (_curvePoints.Count != _prevSize && _curvePoints.Count > 1)
        {
            
            var start = _curvePoints[_curvePoints.Count - 2].transform.position;
            var end = _curvePoints[_curvePoints.Count - 1].transform.position;
            drawLine(start, end, Color.red);
            _prevSize = _curvePoints.Count;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Color usedColor = generateColor();
            var newPoints = new List<GameObject>();
            if(!(_curvePoints.Count > 1))
                return;

            for (int i = 0; i < _curvePoints.Count - 1; i++)
            {
                var start = _curvePoints[i].transform.position;
                var end = _curvePoints[i+1].transform.position;
                
                var yDiff = end.y - start.y;
                var xDiff = end.x - start.x;

                
                newPoints.Add(Instantiate(cubePoint, new Vector3(start.x + xDiff * u, start.y + yDiff * u, start.z),
                    Quaternion.identity));
                newPoints.Add(Instantiate(cubePoint, new Vector3(start.x + xDiff * v, start.y + yDiff * v, start.z),
                    Quaternion.identity));
            }

            _curvePoints = newPoints;

            for (int i = 0; i < _curvePoints.Count-2; i++)
            {
                var start = _curvePoints[i].transform.position;
                var end = _curvePoints[i+1].transform.position;
                drawLine(start, end, usedColor);
            }
            _prevSize = _curvePoints.Count;
            
        }
    }
}
