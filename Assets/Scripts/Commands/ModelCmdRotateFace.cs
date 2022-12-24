using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCmdRotateFace : RubikCubeOperation ,ICommand
{
    public RubikCubeModel model;
    string facename;
    float degree;

    public ModelCmdRotateFace(ref RubikCubeModel model,string facename,float degree)
    {
        this.model = model;
        this.facename = facename;
        this.degree = degree;

    }

    public void Execute()
    {
        //Debug.Log("Rotating the model face " + facename + " with " + degree + " degrees");   
    }

    public void Finish()
    {
        
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }

    public void Undo()
    {
        
    }

    public void Update()
    {
         
    }
}
