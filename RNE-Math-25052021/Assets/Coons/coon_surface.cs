using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coon_surface : MonoBehaviour
{
    private List<Vector3> c1;
    private List<Vector3> c2;
    private List<Vector3> d1;
    private List<Vector3> d2;
    private List<List<Vector3>> courbes;
    private List<(Vector3, Vector3)> interpol1 = new List<(Vector3, Vector3)>();
    private List<(Vector3, Vector3)> interpol2 = new List<(Vector3, Vector3)>();
    private List<(Vector3, Vector3)> erreurInterpol1 = new List<(Vector3, Vector3)>();
    private List<(Vector3, Vector3)> erreurInterpol2 = new List<(Vector3, Vector3)>();

    public Material lineMat;
    public Material interpolation1Mat;
    public Material interpolation2Mat;
    public Material erreurMat;
    public GameObject pointPre;

    bool interpolation1 = true;
    bool interpolation2 = false;
    bool interpolation3 = false;

    void OnPostRender()
    {
        DrawLines();
        DrawInterpolation();
    }

    private void DrawInterpolation()
    {
        for(int i = 0; i < courbes[0].Count; i++)
        {
            GL.Begin(GL.LINES);
            interpolation1Mat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(courbes[0][i].x, courbes[0][i].y, courbes[0][i].z);
            GL.Vertex3(courbes[1][i].x, courbes[1][i].y, courbes[1][i].z);
            GL.End();
        }

        for (int i = 0; i < courbes[2].Count; i++)
        {
            GL.Begin(GL.LINES);
            interpolation2Mat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(courbes[2][i].x, courbes[2][i].y, courbes[2][i].z);
            GL.Vertex3(courbes[3][i].x, courbes[3][i].y, courbes[3][i].z);
            GL.End();
        }

        for(int i = 0; i < erreurInterpol1.Count; i++)
        {
            GL.Begin(GL.LINES);
            erreurMat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(erreurInterpol1[i].Item1.x, erreurInterpol1[i].Item1.y, erreurInterpol1[i].Item1.z);
            GL.Vertex3(erreurInterpol1[i].Item2.x, erreurInterpol1[i].Item2.y, erreurInterpol1[i].Item2.z);
            GL.End();
        }

        for (int i = 0; i < erreurInterpol2.Count; i++)
        {
            GL.Begin(GL.LINES);
            erreurMat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(erreurInterpol2[i].Item1.x, erreurInterpol2[i].Item1.y, erreurInterpol2[i].Item1.z);
            GL.Vertex3(erreurInterpol2[i].Item2.x, erreurInterpol2[i].Item2.y, erreurInterpol2[i].Item2.z);
            GL.End();
        }
    }

    private void DrawLines()
    {
        for(int i = 0; i < c1.Count -1; i++)
        {
            GL.Begin(GL.LINES);
            lineMat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(c1[i].x, c1[i].y, c1[i].z);
            GL.Vertex3(c1[i + 1].x, c1[i + 1].y, c1[i + 1].z);
            GL.End();
        }

        for (int i = 0; i < c2.Count - 1; i++)
        {
            GL.Begin(GL.LINES);
            lineMat.SetPass(0);
            GL.Color(new Color(0f, 1f, 0f, 1f));
            GL.Vertex3(c2[i].x, c2[i].y, c2[i].z);
            GL.Vertex3(c2[i + 1].x, c2[i + 1].y, c2[i + 1].z);
            GL.End();
        }

        for (int i = 0; i < d1.Count - 1; i++)
        {
            GL.Begin(GL.LINES);
            lineMat.SetPass(0);
            GL.Color(new Color(0f, 0f, 1f, 1f));
            GL.Vertex3(d1[i].x, d1[i].y, d1[i].z);
            GL.Vertex3(d1[i + 1].x, d1[i + 1].y, d1[i + 1].z);
            GL.End();
        }

        for (int i = 0; i < d2.Count - 1; i++)
        {
            GL.Begin(GL.LINES);
            lineMat.SetPass(0);
            GL.Color(new Color(0f, 0f, 0f, 1f));
            GL.Vertex3(d2[i].x, d2[i].y, d2[i].z);
            GL.Vertex3(d2[i + 1].x, d2[i + 1].y, d2[i + 1].z);
            GL.End();
        }
    }

    private void Start()
    {
        c1 = new List<Vector3>();
        c2 = new List<Vector3>();
        d1 = new List<Vector3>();
        d2 = new List<Vector3>();

        courbes = new List<List<Vector3>>(4);

        c1.Add(new Vector3(0.0f, 0.0f, 0.0f));
        c1.Add(new Vector3(0.25f, 0.25f, 0.0f));
        c1.Add(new Vector3(0.5f, 0.3f, 0.0f));
        c1.Add(new Vector3(0.75f, 0.25f, 0.0f));
        c1.Add(new Vector3(1.0f, 0.0f, 0.0f));

        c2.Add(new Vector3(0.0f, 0.0f, 1.0f));
        c2.Add(new Vector3(0.25f, -0.25f, 1.0f));
        c2.Add(new Vector3(0.5f, -0.3f, 1.0f));
        c2.Add(new Vector3(0.75f, -0.25f, 1.0f));
        c2.Add(new Vector3(1.0f, -0.3f, 1.0f));

        d1.Add(new Vector3(0.0f, 0.0f, 0.0f));
        d1.Add(new Vector3(0.0f, 0.25f, 0.25f));
        d1.Add(new Vector3(0.0f, 0.3f, 0.5f));
        d1.Add(new Vector3(0.0f, 0.25f, 0.75f));
        d1.Add(new Vector3(0.0f, 0.0f, 1.0f));

        d2.Add(new Vector3(1.0f, -0.0f, 0.0f));
        d2.Add(new Vector3(1.0f, -0.25f, 0.25f));
        d2.Add(new Vector3(1.0f, -0.3f, 0.5f));
        d2.Add(new Vector3(1.0f, -0.25f, 0.75f));
        d2.Add(new Vector3(1.0f, -0.0f, 1.0f));

        courbes.Add(c1);
        courbes.Add(c2);
        courbes.Add(d1);
        courbes.Add(d2);

        foreach(List<Vector3> l in courbes)
        {
            for(int i = 0; i < l.Count; i++)
            {
                GameObject go = Instantiate<GameObject>(pointPre);
                go.transform.position = l[i];
            }
        }

        for (int i = 0; i < courbes[0].Count; i++)
        {
            interpol1.Add((courbes[0][i], courbes[1][i]));
        }

        for (int i = 0; i < courbes[2].Count; i++)
        {
            interpol2.Add((courbes[2][i], courbes[3][i]));
        }

        //création des points du premier segment d'erreur
        //segment 1
        {
            Vector3 seg1 = interpol1[0].Item2 - interpol1[0].Item1;
            Vector3 pos1 = interpol1[0].Item1 + (0.25f * seg1);
            Vector3 pos2 = interpol1[0].Item1 + (0.5f * seg1);
            Vector3 pos3 = interpol1[0].Item1 + (0.75f * seg1);

            Vector3 seg2 = interpol1[interpol1.Count - 1].Item2 - interpol1[interpol1.Count - 1].Item1;
            Vector3 pos1b = interpol1[interpol1.Count - 1].Item1 + (0.25f * seg2);
            Vector3 pos2b = interpol1[interpol1.Count - 1].Item1 + (0.5f * seg2);
            Vector3 pos3b = interpol1[interpol1.Count - 1].Item1 + (0.75f * seg2);

            erreurInterpol1.Add((interpol1[0].Item1, interpol1[interpol1.Count - 1].Item1));
            erreurInterpol1.Add((pos1, pos1b));
            erreurInterpol1.Add((pos2, pos2b));
            erreurInterpol1.Add((pos3, pos3b));
            erreurInterpol1.Add((interpol1[0].Item2, interpol1[interpol1.Count - 1].Item2));
        }

        {
            Vector3 seg1 = interpol2[0].Item2 - interpol2[0].Item1;
            Vector3 pos1 = interpol2[0].Item1 + (0.25f * seg1);
            Vector3 pos2 = interpol2[0].Item1 + (0.5f * seg1);
            Vector3 pos3 = interpol2[0].Item1 + (0.75f * seg1);

            Vector3 seg2 = interpol2[interpol2.Count - 1].Item2 - interpol2[interpol2.Count - 1].Item1;
            Vector3 pos1b = interpol2[interpol2.Count - 1].Item1 + (0.25f * seg2);
            Vector3 pos2b = interpol2[interpol2.Count - 1].Item1 + (0.5f * seg2);
            Vector3 pos3b = interpol2[interpol2.Count - 1].Item1 + (0.75f * seg2);

            erreurInterpol2.Add((interpol2[0].Item1, interpol2[interpol2.Count - 1].Item1));
            erreurInterpol2.Add((pos1, pos1b));
            erreurInterpol2.Add((pos2, pos2b));
            erreurInterpol2.Add((pos3, pos3b));
            erreurInterpol2.Add((interpol2[0].Item2, interpol2[interpol2.Count - 1].Item2));
        }      
        
        for(int i = 1; i < 4; i++)
        {
            for(int j = 1; j < 4; j++)
            {
                Vector3 pos1 = interpol1[i].Item1 + ((float)j / 4 * (interpol1[i].Item2 - interpol1[i].Item1));
                Vector3 pos2 = interpol2[i].Item1 + ((float)j / 4 * (interpol2[i].Item2 - interpol2[i].Item1));

                Vector3 err1 = erreurInterpol1[i].Item1 + ((float)j / 4 * (erreurInterpol1[i].Item2 - erreurInterpol1[i].Item1));
                Vector3 err2 = erreurInterpol2[i].Item1 + ((float)j / 4 * (erreurInterpol2[i].Item2 - erreurInterpol2[i].Item1));

                Vector3 res = pos1 + pos2 - (err1 + err2);

                GameObject go = Instantiate<GameObject>(pointPre);
                go.transform.position = res;
            }           
        }
    }
}
