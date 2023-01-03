using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewCmdIdel : ICommand
{
    RubikCubeView View { get; }
    public UnityAction<ICommand> onfinish { get; set; }
    public ViewCmdIdel(ref RubikCubeView view)
    {
        View = view;
    }

    public void Execute()
    {
        View.Cube.gameObject.name = "idle";
        onfinish.Invoke(this);
    }

    public ICommand GetUndoCmd()
    {
        return this;
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }

   

    public bool ToBeRemembered()
    {
        return false;
    }

    public void SetToBeRemembered(bool b)
    {
        
    }

    
}
