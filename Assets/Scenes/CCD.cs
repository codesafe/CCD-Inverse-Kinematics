using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCD : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject [] nodes;
    
    Vector2 [] line0 = new Vector2[2];
    Vector2 [] line1 = new Vector2[2];

#if true

    void CCD_Solver()
    {
        for(int i= nodes.Length-2; i>=0; i--)
        {
            Vector3 endNode = nodes[nodes.Length - 1].transform.position;

            Vector2 v1 = (target.transform.position - nodes[i].transform.position).normalized;
            Vector2 v2 = (endNode - nodes[i].transform.position).normalized;
            
            float angle = Vector2.Angle(v1, v2);
            float cross = Vector3.Cross(v1, v2).z;
            if(cross > 0)
                angle = -angle;

            // add rotation to node i
            nodes[i].transform.Rotate(0, 0, angle);
        }

    }

    void Update() 
    {
         CCD_Solver();
    }

#else

    void CCD_Solver(int i)
    {
        //int i= nodes.Length-2;
        //for(int i= nodes.Length-2; i>=0; i--)
        {
            Vector3 endNode = nodes[nodes.Length - 1].transform.position;

            Vector2 v1 = (target.transform.position - nodes[i].transform.position).normalized;
            Vector2 v2 = (endNode - nodes[i].transform.position).normalized;
            
            // draw line target to node i
            line0[0] = nodes[i].transform.position;
            line0[1] = target.transform.position;

            // draw line node i to end node
            line1[0] = nodes[i].transform.position;
            line1[1] = endNode;

            float angle = Vector2.Angle(v1, v2);
            // detect clockwise rotation or counterclockwise rotation between v1 and v2
            float cross = Vector3.Cross(v1, v2).z;
            if(cross > 0)
            {
                angle = -angle;
            }

            //Debug.Log("Angle: " + angle);

            // add rotation to node i
            nodes[i].transform.Rotate(0, 0, angle);
            
            // rotate node localrotaion to angle
            //nodes[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }

    float t = 0;
    int index = 2;
    void Update() 
    {
        t += Time.deltaTime;        
        if(t >0.02f)
        {
            CCD_Solver(index);
            index = index > 0 ? index-1 : nodes.Length-2;
            t = 0;
        }

        // draw line0 
        Debug.DrawLine(line0[0], line0[1], Color.red);
        // draw line1
        Debug.DrawLine(line1[0], line1[1], Color.blue);
    }
#endif    

}
