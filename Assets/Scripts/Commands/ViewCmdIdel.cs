using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCmdIdel : ICommand<RubikCubeView>
{
    RubikCubeView View { get; }

    public ViewCmdIdel(ref RubikCubeView view)
    {
        View = view;
    }


    public void Execute()
    {
        View.Cube.gameObject.name = "idle";
    }

    public void Undo()
    {
        
    }

    public void Stop()
    {
        
    }

    public void Update()
    {
        
    }
}
