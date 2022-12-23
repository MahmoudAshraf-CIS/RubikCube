using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RubikCubeViewExecuter : IExecuter<RubikCubeView>
{
    [SerializeField]
    Stack<ICommand<RubikCubeView>> commands;

    public RubikCubeViewExecuter()
    {
        commands = new Stack<ICommand<RubikCubeView>>();
    }

    public void AddCommand(ICommand<RubikCubeView> i)
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
            ICommand<RubikCubeView> i = commands.Pop();
            i.Stop();
        }
    }

    public void Undo()
    {
        if (commands.Count > 0)
        {
            ICommand<RubikCubeView> i = commands.Pop();
            i.Undo();
        }
    }

    public void Update()
    {
        if (commands.Count > 0)
        {
            ICommand<RubikCubeView> i = commands.Peek();
            i.Update();
        }
    }
}
