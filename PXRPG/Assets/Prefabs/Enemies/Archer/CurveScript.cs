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
    const int nrPoints = 20;
    Vector3[] positions = new Vector3[nrPoints];
    Vector3[] ctrPts = new Vector3[3];

    // Start is called before the first frame update
    void Start()
    {
        lr.useWorldSpace = true;
    }

    public void draw(Vector3 startPos, Vector3 endPos)
    {
        ctrPts[0] = startPos;
        //The middle point should be between the first and last point. And put it higher up in y-pos.
        ctrPts[1] = (endPos-startPos)/2.0f + new Vector3(0.0f, 2.0f, 0.0f) + GameObject.FindGameObjectWithTag("Player").transform.position;
        ctrPts[2] = endPos;

        lr.positionCount = nrPoints;
        lr.widthMultiplier = 0.1f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        //drawBezier();
    }

    public void updatePositions(Vector3 startPos, Vector3 endPos)
    {
        //lr.useWorldSpace = false;
        ctrPts[0] = startPos;
        //The middle point should be between the first and last point. And put it higher up in y-pos.
        ctrPts[1] = startPos + new Vector3(0.0f, Mathf.Abs(startPos.y-endPos.y)*1.5f, 0.0f);
        ctrPts[2] = endPos;
        drawBezier();
        //lr.SetPosition(0, startPos);
        //lr.SetPosition(1, endPos);
        //lr.SetPositions(positions);
        //Debug.Log("hej");
    }

    /*
     *  Draw a bezier curve using the controlPoints and t from 0 to nrPoints.
     */
    private void drawBezier()
    {
        lr.enabled = false;
        Vector3 startPos = ctrPts[0];
        Vector3 midPos = ctrPts[1];
        Vector3 endPos = ctrPts[2];
        float t = 0.0f;
        float segment = 1.0f / nrPoints;
        t = segment;
        for(int i = 0; i < nrPoints; i++)
        {
            Vector3 l0 = (1 - t) * startPos + t * midPos;
            Vector3 l1 = (1 - t) * midPos + t * endPos;
            positions[i] = (1 - t) * l0 + t * l1;
            t += segment;
        }
        lr.SetPositions(positions);
        lr.enabled = true;
    }
}
