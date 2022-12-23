using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontViewCommand : ICommand<RubikCubeView>
{
    public RubikCubeView View { get; }
    public string priveousName;
    public FrontViewCommand(ref RubikCubeView view)
    {
        View = view;
    }


    public void Execute()
    {
        priveousName = View.Cube.gameObject.name;
        View.Cube.gameObject.name = "front";
    }

    public void Undo()
    {
        View.Cube.gameObject.name = priveousName;
    }

    public void Stop()
    {
        
    }

    public void Update()
    {
        
    }
}
