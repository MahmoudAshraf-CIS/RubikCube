using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewCmdRotateFace : ICommand
{
    public RubikCubeView View { get; }
    UnityAction<ICommand> onfinish;


 
    string facename;
    float degree;
    Quaternion start;
    GameObject face;
    bool useStartRotation = true;
    bool remember = true;
    public ViewCmdRotateFace(ref RubikCubeView view, string facename, float degree, Quaternion start)
    {
        View = view;
        this.facename = facename;
        this.degree = degree;
        this.start = start;
        useStartRotation = true;
        remember = true;
    }

    public ViewCmdRotateFace(ref RubikCubeView view, string facename, float degree)
    {
        View = view;
        this.facename = facename;
        this.degree = degree;
        useStartRotation = false;
        remember = true;
    }



    IEnumerator Rotate(float duration)
    {
        //face.transform.rotation = originalRotation * Quaternion.Euler(Vector3.up * degree);
        //yield return null;
        yield return new WaitForFixedUpdate();
        face = View.GetFaceRoot(facename);
        if (!useStartRotation)
            this.start = face.transform.rotation;

        List<GameObject> neghbors = View.GetFaceNeighborCells(facename);

        foreach (var item in neghbors)
        {
            item.transform.parent = face.transform;
        }
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            face.transform.rotation = Quaternion.Lerp(start, start * Quaternion.Euler(Vector3.up * degree), t);
            yield return null;
        }
        onfinish.Invoke(this);
    }
    void ICommand.Execute()
    {
        View.StartCoroutine(Rotate(3));
    }
    public ICommand GetUndoCmd()
    {
        useStartRotation = false;
        this.degree *= -1;
        return this;
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }
    public void SetOnCmdFinish(UnityAction<ICommand> onfinish)
    {
        this.onfinish = onfinish;
    }

   
    public bool ToBeRemembered()
    {
        return remember;
    }

    public void SetToBeRemembered(bool b)
    {
        remember = b;
    }
}
