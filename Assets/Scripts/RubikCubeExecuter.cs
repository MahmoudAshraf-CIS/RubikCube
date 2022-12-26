using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RubikCubeExecuter : IExecuter
{
    [SerializeField]
    Stack<ICommand> commands;

 
   
    public RubikCubeExecuter()
    {
        commands = new Stack<ICommand>();
    }

    public void AddCommand(ICommand i)
    {
        Finish();
        i.Execute();
        commands.Push(i);
    }

    public void AddCommand(List<ICommand> cmds)
    {
        foreach (var cmd in cmds)
        {
            AddCommand(cmd);
        }
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

    public void Undo(int count)
    {
        for (int i = 0; i < count; i++)
            Undo();
    }
}
