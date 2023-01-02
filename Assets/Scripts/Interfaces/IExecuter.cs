using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.Events;

public interface IExecuter
{
    void AddCommand(ICommand i);
    void AddCommand(List<ICommand> cmds);
   
    void Undo(int count);
 
    bool IsRunning();
    int StackSize();
    int QueueSize();
    void ClearHistory();
    void SetOnFinish(UnityAction onFinish);
}
