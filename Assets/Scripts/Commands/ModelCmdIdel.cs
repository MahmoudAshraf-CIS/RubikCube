using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelCmdIdel : ICommand
{
    RubikCubeModel state;
    public UnityAction<ICommand> onfinish { get; set; }
    public ModelCmdIdel(ref RubikCubeModel state)
    {
        this.state = state;
    }

    public void Execute()
    {
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
