using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicCubeModelExecuter : IExecuter<Models.RubikCubeModel>
{
    Stack<ICommand<RubikCubeModel>> commands;

    public RubicCubeModelExecuter()
    {
        commands = new Stack<ICommand<RubikCubeModel>>();
    }

    public void AddCommand(ICommand<RubikCubeModel> i)
    {
        i.Execute();
        commands.Push(i);
    }

    public int StackSize()
    {
        return commands.Count;
    }

    public void Stop()
    {
        if (commands.Count > 0)
        {
            ICommand<RubikCubeModel> i = commands.Pop();
            i.Stop();
        }
    }

    public void Undo()
    {
        if(commands.Count > 0)
        {
            ICommand<RubikCubeModel> i = commands.Pop();
            i.Undo();
        }

    }

    public void Update()
    {
        
    }
}
