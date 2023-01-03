using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.Events;

public interface IExecuter
{
    /// <summary>
    /// Invoked each time the Executer clears the aiting commands
    /// </summary>
    UnityAction onFinish { get; set; }

    /// <summary>
    /// Adds <see cref="ICommand"/> i to waiting <see cref="Queue"/>    
    /// </summary>
    /// <param name="i">command to be added</param>
    void AddCommand(ICommand i);

    /// <summary>
    /// Adds List of <see cref="ICommand"/> to waiting <see cref="Queue"/>    
    /// </summary>    
    void AddCommand(List<ICommand> cmds);


    /// <param name="count"> of commands to be undon</param>
    void Undo(int count);

    /// <returns>either the executor get a command that is running</returns>
    bool IsRunning();
  
    /// <returns>Commands history <see cref="Stack"/> size</returns>
    int HistorySize();
    /// <returns>Commands waiting <see cref="Queue"/> size</returns>
    int WaitingSize();
    /// <summary>
    /// Clears the commands history
    /// </summary>
    void ClearHistory();
     
}
