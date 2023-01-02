using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RubikCubeExecuter : IExecuter
{
    [SerializeField]
    Stack<ICommand> cmdHistory;
    Queue<ICommand> cmdQueue;

    ICommand runningCmd;
    UnityAction onFinish;
    public RubikCubeExecuter()
    {
        cmdHistory = new Stack<ICommand>();
        cmdQueue = new Queue<ICommand>();
    }

    public void AddCommand(ICommand i)
    {
        cmdQueue.Enqueue(i);
        i.SetOnCmdFinish(ExecuteNextCmd);
        if (!IsRunning())
            ExecuteNextCmd(null);
        // if no cmd is running then run i
        // on i finish dequeue
        // if cmd is running wait in the queue
    }

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
            //Debug.Log("Executer queue is now empty, All done !");
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

    public void AddCommand(List<ICommand> cmds)
    {
        if (cmds == null || cmds.Count == 0)
            return;
        foreach (var cmd in cmds)
        {
            AddCommand(cmd);
        }
    }

    public int StackSize()
    {
        return cmdHistory.Count;
    }
    public int QueueSize()
    {
        return cmdQueue.Count;
    }


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

    
    public void Undo(int count)
    {
        for (int i = 0; i < count; i++)
            Undo();
    }

    public void ClearHistory()
    {
        cmdHistory.Clear();
    }

    public bool IsRunning()
    {
        return runningCmd != null;
    }

    public void SetOnFinish(UnityAction onFinish)
    {
        this.onFinish = onFinish;
    }
}
