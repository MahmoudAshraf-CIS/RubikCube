using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewCmdIdel : ICommand
{
    RubikCubeView View { get; }
    UnityAction<ICommand> onfinish;
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

    public void SetOnCmdFinish(UnityAction<ICommand> onfinish)
    {
        this.onfinish = onfinish;
    }

    public bool ToBeRemembered()
    {
        return false;
    }

    public void SetToBeRemembered(bool b)
    {
        
    }
}
