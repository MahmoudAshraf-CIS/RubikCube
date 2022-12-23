using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontModelCommand : RubikCubeOperation ,ICommand<RubikCubeModel>
{
    public RubikCubeModel model;
    string priveousCube;
    public FrontModelCommand(ref RubikCubeModel model)
    {
        this.model = model;
    }

    public void Execute()
    {
        priveousCube = model.testState;
        
        model.testState = "Front";
    }

    public void Stop()
    {
        
    }

    public void Undo()
    {
        model.testState = priveousCube;
    }

    public void Update()
    {
         
    }
}
