using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RubikCubeViewExecuter : IExecuter
{
    [SerializeField]
    Stack<ICommand> commands;

    public RubikCubeViewExecuter()
    {
        commands = new Stack<ICommand>();
    }

    public void AddCommand(ICommand i)
    {
        Finish();
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
            ICommand i = commands.Peek();
            i.Finish();
            if (i.SubCommands() != null && i.SubCommands().Count > 0)
                foreach (var cmd in i.SubCommands())
                {
                    AddCommand(cmd);
                }
        }
    }

    public void Undo()
    {
        if (commands.Count > 0)
        {
            ICommand i = commands.Pop();
            i.Undo();
        }
    }

    public void Update()
    {
        if (commands.Count > 0)
        {
            ICommand i = commands.Peek();
            i.Update();
        }
    }
}
