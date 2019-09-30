using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PolygonRenderer))]
public class PolygonRendererInspector : Editor
{
    PolygonRenderer renderer;

    void OnEnable()
    {
        renderer = (PolygonRenderer)target;
    }

    void OnSceneGUI()
    {
        if (renderer)
        {
            Vector3[] vertices = new Vector3[renderer.points.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(renderer.points[i].x, renderer.points[i].y, 0);
            }
            Handles.DrawAAPolyLine(vertices);

            for (int i = 0; i < vertices.Length; i++)
            {
                Handles.color = Color.red;
                Handles.Label(vertices[i], "P:" + i);
                renderer.points[i] = Handles.PositionHandle(vertices[i], Quaternion.identity);
            }
            
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            if (renderer)
            {
                renderer.UpdateMesh();
            }
        }
    }
}
