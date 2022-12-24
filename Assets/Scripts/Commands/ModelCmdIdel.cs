using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCmdIdel : ICommand
{
    RubikCubeModel state;
    public ModelCmdIdel(ref RubikCubeModel state)
    {
        this.state = state;
    }

    public void Execute()
    {
       
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
