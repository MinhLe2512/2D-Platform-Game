using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRoute : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints;
    [SerializeField] private float step;
    private Vector2 gizmoPositions;
    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t+= step)
        {
            gizmoPositions = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmoPositions, 0.2f);
        }

       
    }
}
