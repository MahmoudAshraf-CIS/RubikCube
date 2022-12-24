using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicCubeModelExecuter : IExecuter
{
    Stack<ICommand> commands;

    public RubicCubeModelExecuter()
    {
        commands = new Stack<ICommand>();
    }

    public void AddCommand(ICommand i)
    {
        i.Execute();
        commands.Push(i);
    }

    public int StackSize()
    {
        return commands.Count;
    }

    public void Finish()
    {
        if (commands.Count > 0)
        {
            ICommand i = commands.Pop();
            i.Finish();
        }
    }

    public void Undo()
    {
        if(commands.Count > 0)
        {
            ICommand i = commands.Pop();
            i.Undo();
        }

    }

    public void Update()
    {
        
    }
}
