using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Draw a bezier curve line.
 *  This is used to show enemy trajectory path.
 */
public class CurveScript : MonoBehaviour
{
    public LineRenderer lr;
    const int nrPoints = 2;
    Vector3[] positions = new Vector3[nrPoints];

    // Start is called before the first frame update
    void Start()
    {

        lr.useWorldSpace = true;
    }

    public void draw(Vector3 startPos, Vector3 endPos)
    {

        positions[0] = startPos;
        //positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        positions[1] = endPos;

        lr.positionCount = nrPoints;
        lr.SetPositions(positions);
        lr.widthMultiplier = 0.1f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void updatePositions(Vector3 startPos, Vector3 endPos)
    {
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        //Debug.Log("hej");
    }
}
