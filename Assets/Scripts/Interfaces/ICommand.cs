
using System.Collections.Generic;
using UnityEngine.Events;

public interface ICommand {
    UnityAction<ICommand> onfinish { get; set; }
    
    /// <summary>
    /// Executes the command 
    /// </summary>
    void Execute();
    

    /// <returns>A <see cref="ICommand"/> that to be executed to reverse this commans execution, 
    /// in some cases changing some local variables and returning the same command does the trick
    /// </returns>
    ICommand GetUndoCmd();



    /// <returns>Subcommands if any and <see cref="null"/> other wise,
    /// Assuming a command that to be executed it must execute other commands</returns>
    List<ICommand> SubCommands();

     

    /// <returns>Either if this command needs to be kept in the history or not</returns>
    bool ToBeRemembered();
    /// <summary>
    /// Sets either if this command needs to be kept in the history or not
    /// </summary>
    /// <param name="b">true or false</param>
    void SetToBeRemembered(bool b);

}

