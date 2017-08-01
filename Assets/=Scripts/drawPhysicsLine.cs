using UnityEngine;
using System.Collections;

public class drawPhysicsLine : MonoBehaviour
{

    public GameObject linePrefab;
    public float lineLength = 0.1f;
    public float lineWidth = 0.1f;

    private Vector3 touchPos;

    void Start()
    {

    }

    void Update()
    {
        drawLine();
    }

    void drawLine()
    {

        if (Time.timeScale == 1) { 
            if (Input.GetMouseButtonDown(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 10;
        }

            if (Input.GetMouseButton(0))
            {

                Vector3 startPos = touchPos;
                Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPos.z = 10;

                if ((endPos - startPos).magnitude > lineLength)
                {
                    GameObject obj = Instantiate(linePrefab, transform.position, transform.rotation) as GameObject;
                    obj.transform.position = (startPos + endPos) / 2;
                    obj.transform.right = (endPos - startPos).normalized;

                    obj.transform.localScale = new Vector3((endPos - startPos).magnitude, lineWidth, lineWidth);

                    obj.transform.parent = this.transform;

                    touchPos = endPos;
                }
            }
        }
    }
}