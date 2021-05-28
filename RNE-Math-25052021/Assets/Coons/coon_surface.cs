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
    public Material pointRed;
    public Material pointBlue;
    public Material pointGreen;
    public Material matSurface;
    public GameObject pointPre;
    public GameObject pointSurface;

    void OnPostRender()
    {
        DrawLines();
        DrawInterpolation();
    }

    private void DrawInterpolation()
    {
        //rouge
        for(int i = 0; i < courbes[0].Count; i++)
        {
            GL.Begin(GL.LINES);
            interpolation1Mat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(courbes[0][i].x, courbes[0][i].y, courbes[0][i].z);
            GL.Vertex3(courbes[1][i].x, courbes[1][i].y, courbes[1][i].z);
            GL.End();
        }

        //bleu
        for (int i = 0; i < courbes[2].Count; i++)
        {
            GL.Begin(GL.LINES);
            interpolation2Mat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(courbes[2][i].x, courbes[2][i].y, courbes[2][i].z);
            GL.Vertex3(courbes[3][i].x, courbes[3][i].y, courbes[3][i].z);
            GL.End();
        }

        //vert
        for(int i = 0; i < erreurInterpol1.Count; i++)
        {
            GL.Begin(GL.LINES);
            erreurMat.SetPass(0);
            GL.Color(new Color(1f, 0f, 0f, 1f));
            GL.Vertex3(erreurInterpol1[i].Item1.x, erreurInterpol1[i].Item1.y, erreurInterpol1[i].Item1.z);
            GL.Vertex3(erreurInterpol1[i].Item2.x, erreurInterpol1[i].Item2.y, erreurInterpol1[i].Item2.z);
            GL.End();
        }

        //vert
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
        //d1.Add(new Vector3(0.0f, 0.25f, 0.75f));
        d1.Add(new Vector3(0.0f, 0.0f, 1.0f));

        d2.Add(new Vector3(1.0f, -0.0f, 0.0f));
        d2.Add(new Vector3(1.0f, -0.25f, 0.25f));
        d2.Add(new Vector3(1.0f, -0.3f, 0.5f));
        //d2.Add(new Vector3(1.0f, -0.25f, 0.75f));
        d2.Add(new Vector3(1.0f, -0.3f, 1.0f));

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

        //première interpolation
        {
            Vector3 seg1 = interpol1[0].Item2 - interpol1[0].Item1;
            Vector3 seg2 = interpol1[interpol1.Count - 1].Item2 - interpol1[interpol1.Count - 1].Item1;

            for (float i = 0.0f; i <= 1.0f; i += (1.0f / (float)(interpol2.Count - 1)))
            {
                Vector3 posA = interpol1[0].Item1 + (i * seg1);
                Vector3 posB = interpol1[interpol1.Count - 1].Item1 + (i * seg2);
                erreurInterpol1.Add((posA, posB));
            }
        }

        //seconde interpolation
        {
            Vector3 seg1 = interpol2[0].Item2 - interpol2[0].Item1;
            Vector3 seg2 = interpol2[interpol2.Count - 1].Item2 - interpol2[interpol2.Count - 1].Item1;

            for (float i = 0.0f; i <= 1.0f; i += (1.0f / (float)(interpol1.Count - 1)))
            {
                Vector3 posA = interpol2[0].Item1 + (i * seg1);
                Vector3 posB = interpol2[interpol2.Count - 1].Item1 + (i * seg2);
                erreurInterpol2.Add((posA, posB));
            }
        }      
        
        List<Vector3> posR = new List<Vector3>();
        List<Vector3> posBl = new List<Vector3>();
        List<Vector3> posG = new List<Vector3>();

        //calcul des spheres rouges
        for(int i = 0; i < interpol1.Count; i++)
        {
            Vector3 seg = interpol1[i].Item2 - interpol1[i].Item1;

            for (int j = 0; j < interpol2.Count; j++)
            {
                Vector3 pos = interpol1[i].Item1 + (j * (1.0f / (interpol2.Count - 1))) * seg;

                GameObject go = Instantiate<GameObject>(pointPre);
                go.transform.position = pos;
                go.GetComponent<MeshRenderer>().material = pointRed;

                posR.Add(pos);
            }
        }

        //calcul des spheres bleues
        for (int i = 0; i < interpol2.Count; i++)
        {
            Vector3 seg = interpol2[i].Item2 - interpol2[i].Item1;

            for (int j = 0; j < interpol1.Count; j++)
            {
                Vector3 pos = interpol2[i].Item1 + (j * (1.0f / (interpol1.Count - 1))) * seg;

                GameObject go = Instantiate<GameObject>(pointPre);
                go.transform.position = pos;
                go.GetComponent<MeshRenderer>().material = pointBlue;

                posBl.Add(pos);
            }
        }

        //calcule des spheres vertes
        for (int i = 0; i < erreurInterpol1.Count; i++)
        {
            Vector3 seg = erreurInterpol1[i].Item2 - erreurInterpol1[i].Item1;

            for (int j = 0; j < erreurInterpol2.Count; j++)
            {
                Vector3 pos = erreurInterpol1[i].Item1 + (j * (1.0f / (erreurInterpol2.Count - 1))) * seg;

                GameObject go = Instantiate<GameObject>(pointPre);
                go.transform.position = pos;
                go.GetComponent<MeshRenderer>().material = pointGreen;

                posG.Add(pos);
            }
        }

        //calcul des points de la surface
        for(int i = 0; i < posR.Count; i++)
        {
            Vector3 p = posR[i] + posBl[i] - posG[i];

            GameObject go = Instantiate<GameObject>(pointPre);
            go.transform.position = p;
            go.GetComponent<MeshRenderer>().material = matSurface;
        }
    }
}
