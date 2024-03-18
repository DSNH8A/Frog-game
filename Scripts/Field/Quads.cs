using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quads : MonoBehaviour
{
    private Mesh mesh;
    private int width = 4;
    private int height = 4;
    private int vIndex;
    private int tIndex;

    void Start()
    {
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[4 * width * height];
        Vector2[] uv = new Vector2[4 * width * height];
        int[] triangles = new int[6 * width * height];

        for(int i = 0; i< width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int index = i * height + j;
                float tilesize = 10f;

                vertices[index * 4 + 0] = new Vector3(tilesize * i, tilesize *j);
                vertices[index * 4 + 1] = new Vector3(tilesize * +i, tilesize * (j + 1));
                vertices[index * 4 + 2] = new Vector3(tilesize * (i + 1), tilesize * (j + 1));
                vertices[index * 4 + 3] = new Vector3(tilesize * (i + 1), tilesize * j);

                uv[index + 0] = new Vector2(0, 0);
                uv[index + 1] = new Vector2(0, 1);
                uv[index + 2] = new Vector2(1, 1);
                uv[index + 3] = new Vector2(1, 0);

                triangles[index * 6] = 0 + index * 4;
                triangles[index * 6 + 1] = 1 + index * 4;
                triangles[index * 6 + 2] = 2 + index * 4;

                triangles[index * 6 + 3] = 0 + index * 4;
                triangles[index * 6 + 4] = 2 + index * 4;
                triangles[index * 6 + 5] = 3 + index * 4;
            }
        }

        //vertices[0] = new Vector3(0, 0);
        //vertices[1] = new Vector3(0, 1);
        //vertices[2] = new Vector3(1, 1);
        //vertices[3] = new Vector3(1, 0);

        //uv[0] = new Vector2(0, 0);
        //uv[1] = new Vector2(0, 1);
        //uv[2] = new Vector2(1, 1);
        //uv[3] = new Vector2(1, 0);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        //triangles[3] = 0;
        //triangles[4] = 2;
        //triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
