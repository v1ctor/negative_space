using UnityEngine;

[ExecuteInEditMode]
public class PolygonRenderer : MonoBehaviour
{
    public Material material;
    public Vector2[] points;

    public void UpdateMesh() {
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(points);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[points.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(points[i].x, points[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh
        {
            vertices = vertices,
            triangles = indices
        };
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        if (filter == null)
        {
            filter = gameObject.AddComponent<MeshFilter>();
        }
        filter.mesh = msh;



        PolygonCollider2D polyCollider = gameObject.GetComponent<PolygonCollider2D>();
        if (polyCollider == null) {
            polyCollider = gameObject.AddComponent<PolygonCollider2D>();
        }
        polyCollider.points = points;
    }

    void Start()
    {
        UpdateMesh();   
    }
}
