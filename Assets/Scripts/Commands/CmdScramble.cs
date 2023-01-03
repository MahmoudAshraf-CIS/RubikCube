using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.Events;

public class CmdScramble : ICommand
{
    RubikCubeModel model;
    RubikCubeView view;
    List<ICommand> subcommands;
    public UnityAction<ICommand> onfinish { get; set; }

    public CmdScramble(ref RubikCubeModel model, ref RubikCubeView view, int length)
    {
        this.model = model;
        this.view = view;
        subcommands = new List<ICommand>();
        for(int i = 0; i < length; i++)
        {
            string f = FaceName.GetRandomFaceName();
            float angle = Random.Range(0, 2) == 1 ? 90 : -90;
            subcommands.Add(new ViewCmdRotateFace(ref view, f, angle));
            subcommands.Add(new ModelCmdRotateFace(ref model, f, angle));
        }
    }

    public void Execute()
    {
        if(onfinish!=null)
            onfinish.Invoke(this);
    }
    public ICommand GetUndoCmd()
    {
        for (int i = 0; i < subcommands.Count; i++)
        {
            subcommands[i] = subcommands[i].GetUndoCmd();
        }
        return this;
    }

    public List<ICommand> SubCommands()
    {
        return subcommands;
    }  

    public bool ToBeRemembered()
    {
        return false;
    }

    public void SetToBeRemembered(bool b)
    {
        
    }
}
