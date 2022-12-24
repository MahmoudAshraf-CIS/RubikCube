using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCmdIdel : ICommand
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

    public void Finish()
    {
        
    }

    public void Update()
    {
        
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }
}
