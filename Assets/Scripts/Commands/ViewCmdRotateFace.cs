using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCmdRotateFace : ICommand
{
    public RubikCubeView View { get; }
    public string priveousName;
    string facename;
    float degree;
    Quaternion start;
    GameObject face;
    IEnumerator rotateCor;
    public ViewCmdRotateFace(ref RubikCubeView view, string facename, float degree, Quaternion start)
    {
        View = view;
        this.facename = facename;
        this.degree = degree;
        this.start = start;
    }

    public ViewCmdRotateFace(ref RubikCubeView view, string facename, float degree)
    {
        View = view;
        this.facename = facename;
        this.degree = degree;
        face = View.GetFaceRoot(facename);
        this.start = face.transform.rotation;
    }

    public void Execute()
    {
        Debug.Log("view " + facename + " " + (degree >0 ? "+ve":"-ve"));
        face = View.GetFaceRoot(facename);
        List<GameObject> neghbors = View.GetFaceNeighborCells(facename);
        Debug.Log(neghbors.Count);  
        foreach (var item in neghbors)
        {
            item.transform.parent = face.transform;
        }
        face.transform.rotation = start * Quaternion.Euler(Vector3.up * degree);
        /// rotating over time requres the executer to wait for the command to be finished then execute another cmd
        //Debug.LogError(
        //    "from "+
        //    start.eulerAngles
        //    +"to "+
        //    (start * Quaternion.Euler(Vector3.up * degree)).eulerAngles
        //    , face.transform);
        //rotateCor = Rotate(1.0f);
        //View.StartCoroutine(rotateCor);
    }


    IEnumerator Rotate(float duration)
    {
        //face.transform.rotation = originalRotation * Quaternion.Euler(Vector3.up * degree);
        //yield return null;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            //Debug.Log(start.eulerAngles);
            face.transform.rotation = Quaternion.Lerp(start, start * Quaternion.Euler(Vector3.up * degree), t);
            yield return null;
        }
    }

    public void Undo()
    {
        //Debug.Log("undo "+ facename + " " + (degree > 0 ? "+ve" : "-ve"));
        degree *= -1;
        face = View.GetFaceRoot(facename);
        List<GameObject> neghbors = View.GetFaceNeighborCells(facename);
        foreach (var item in neghbors)
        {
            item.transform.parent = face.transform;
        }
        face.transform.rotation = face.transform.rotation * Quaternion.Euler(Vector3.up * degree);
        //start = face.transform.rotation;
        //View.StartCoroutine(Rotate(1.0f));
    }

    public void Finish()
    {
        //Debug.Log("finish " +facename + " "+ degree);
        //View.StopCoroutine(rotateCor);
        View.StopAllCoroutines();
        face.transform.rotation = start * Quaternion.Euler(Vector3.up * degree);
        //Debug.LogWarning(face.transform.rotation.eulerAngles);
        //face.transform.rotation = Quaternion.Lerp(originalRotation, originalRotation * Quaternion.Euler(Vector3.up * degree), 0);
    }

    public void Update()
    {
        //Debug.Log("rotate face update");
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }
}
