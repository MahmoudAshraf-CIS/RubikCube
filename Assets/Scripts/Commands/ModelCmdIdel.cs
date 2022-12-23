using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCmdIdel : ICommand<RubikCubeModel>
{
    RubikCubeModel state;
    public ModelCmdIdel(ref RubikCubeModel state)
    {
        this.state = state;
    }

    public void Execute()
    {
        state.testState = "idle";
    }

    public void Stop()
    {
         
    }

    public void Undo()
    {
        
    }

    public void Update()
    {
        
    }
}
