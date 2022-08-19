using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickArc : MonoBehaviour
{
    public float MaxRadius = 1.5f;
    public float MaxHeight = 3f;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(float radius)
    {
        var ring = MakeRing(gameObject.transform.position + Vector3.up * MaxHeight, radius, 40, false);
        RenderDebugLines(ring);
    }

    private Vector3[] MakeRing(Vector3 centerLocation, float radius, int resolution, bool includeCenter = true)
    {
        if (resolution < 3)
        {
            Debug.LogWarning("Resolution must be 3 or greater");
            resolution = 3;
        }
        List<Vector3> verts = new List<Vector3>();
        if (includeCenter)
        {
            verts.Add(centerLocation);
        }

        int stepSize = (int)Mathf.Floor(360 / resolution);
        for (var i = resolution; i > 0; i--)
        {
            var angle = stepSize * i;
            var v = new Vector3
            {
                x = (radius * Mathf.Cos(angle * Mathf.Deg2Rad)) + centerLocation.x,
                y = centerLocation.y,
                z = (radius * Mathf.Sin(angle * Mathf.Deg2Rad)) + centerLocation.z
            };
            verts.Add(v);

        }
        return verts.ToArray();
    }

    private void RenderDebugLines(Vector3[] verts)
    {
        lineRenderer.positionCount = verts.Length;
        lineRenderer.SetPositions(verts);
    }
}
