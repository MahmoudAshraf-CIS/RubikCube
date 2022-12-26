using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
public class CmdScramble : ICommand
{
    RubikCubeModel model;
    RubikCubeView view;
    List<ICommand> subcommands;


    public CmdScramble(ref RubikCubeModel model, ref RubikCubeView view, int length)
    {
        this.model = model;
        this.view = view;
        subcommands = new List<ICommand>();
        for(int i = 0; i < length; i++)
        {
            string f = FaceName.GetRandomFaceName();
            float angle = Random.Range(0, 2) == 1 ? 90 : -90;
            //Debug.Log(f + " " + angle);
            subcommands.Add(new ViewCmdRotateFace(ref view, FaceName.Left, -90));
            subcommands.Add(new ModelCmdRotateFace(ref model, FaceName.Left, -90));
        }
    }

    public void Execute()
    {
        
    }

    public void Finish()
    {
        
    }

    public List<ICommand> SubCommands()
    {
        return subcommands;
    }

    public void Undo()
    {
        for (int i = 0; i < subcommands.Count; i++)
        {
            subcommands[0].Undo();
        }
    }

    public void Update()
    {
        
    }
}
