using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISolver
{
    /// <summary>
    /// Prepair or initialize any variables needed in order to make the right decition "commands"
    /// </summary>
    void Expect();

    /// <summary>
    /// Update clock is called via <see cref="MonoBehaviour"/> Update method
    /// Mostly used for human interaction (touch, input,....etc)
    /// </summary>
    void Update();
    
    /// <returns>A List of <see cref="ICommand"/> to be executed in order to achieve the solver decition</returns>
    List<ICommand> Decide();
}
