using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Implements the command design pattern
/// Resposiple for coordinating a punch of <see cref="ICommand"/>s
/// 
/// The command state would be
///     - Waiting in <see cref="RubikCubeExecuter.cmdQueue"/>
///     - Running in <see cref="RubikCubeExecuter.runningCmd"/>
///     - Finished in <see cref="RubikCubeExecuter.cmdHistory"/>
/// Keep in mind that the command can be ignored from being kept in history <see cref="ICommand.SetToBeRemembered(bool)"/>
/// </summary>
[System.Serializable]
public class RubikCubeExecuter : IExecuter
{

    [SerializeField]
    Stack<ICommand> cmdHistory;
    Queue<ICommand> cmdQueue;

    ICommand runningCmd;
    /// <summary>
    /// Invoked each time the Executer clears the aiting commands
    /// </summary>
    public UnityAction onFinish { get; set; }


    public RubikCubeExecuter()
    {
        cmdHistory = new Stack<ICommand>();
        cmdQueue = new Queue<ICommand>();
    }

    /// <summary>
    /// Adds <see cref="ICommand"/> i to waiting queue <see cref="RubikCubeExecuter.cmdQueue"/>    
    /// </summary>
    /// <param name="i">command to be added</param>
    public void AddCommand(ICommand i)
    {
        cmdQueue.Enqueue(i);
        i.onfinish += ExecuteNextCmd;
        if (!IsRunning())
            ExecuteNextCmd(null);
        // if no cmd is running then run i
        // on i finish dequeue
        // if cmd is running wait in the queue
    }
    /// <summary>
    /// Adds List of <see cref="ICommand"/> cmds to waiting queue <see cref="RubikCubeExecuter.cmdQueue"/>    
    /// </summary>
    /// <param name="cmds">List of commands to be added</param>
    public void AddCommand(List<ICommand> cmds)
    {
        if (cmds == null || cmds.Count == 0)
            return;
        foreach (var cmd in cmds)
        {
            AddCommand(cmd);
        }
    }
    /// <summary>
    /// Called after each <see cref="ICommand"/> execution 
    /// </summary>
    /// <param name="finishedCmd">the command that has just been executed</param>
    public void ExecuteNextCmd(ICommand finishedCmd)
    {
        // add the finished cmd to the history
        if (finishedCmd != null && finishedCmd.ToBeRemembered())
        {
            cmdHistory.Push(finishedCmd);
        }
        // if the queue is empty - do nothing
        if (cmdQueue.Count == 0 )
        {
            // runningCmd now is the last command 
            runningCmd = null;
            // we need to inform the client that the cmnd queue is empty - 
            // to maybe decide if the game is solved or not
            if (onFinish != null)
                onFinish.Invoke();
            return;
        }
        // execute the first item in the queue
        runningCmd = cmdQueue.Dequeue();
        AddCommand(runningCmd.SubCommands());
        runningCmd.Execute();
    }

    /// <summary>
    /// Undo the last item in <see cref="RubikCubeExecuter.cmdHistory"/>
    /// Undo by adding the reversed command into the waiting queue after it's marked to not to be remembered
    /// so it won't be added to the history again once finished.
    /// </summary>
    void Undo()
    {
        // if running 
        // empty queue 
        // undo the running 
        // non empty queue 
        // remove the last item added to queue
        // not running 
        // undo the stack pop


        // OR ------------
        // pop from the history 
        // add the undo cmd to the queue - set the undo cmd not to be kept in history

        if (cmdHistory.Count > 0)
        {
            ICommand i = cmdHistory.Pop();
            i.SetToBeRemembered(false);
            AddCommand(i.GetUndoCmd());
        }
    }

    
    /// <returns>either the executor get a command that is running</returns>
    public bool IsRunning()
    {
        return runningCmd != null;
    }




    /// <returns>Commands history <see cref="RubikCubeExecuter.cmdHistory"/> size</returns>
    public int HistorySize()
    {
        return cmdHistory.Count;
    }
  
    /// <returns>Commands waiting <see cref="RubikCubeExecuter.cmdQueue"/> size</returns>
    public int WaitingSize()
    {
        return cmdQueue.Count;
    }

   
    /// <summary>
    /// Executes <see cref="RubikCubeExecuter.Undo"/> for <paramref name="count"/>
    /// </summary>
    /// <param name="count"></param>
    public void Undo(int count)
    {
        for (int i = 0; i < count; i++)
            Undo();
    }

    /// <summary>
    /// Clears the commands history
    /// </summary>
    public void ClearHistory()
    {
        cmdHistory.Clear();
    }

  

   
}
