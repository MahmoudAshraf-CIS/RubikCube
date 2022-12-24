using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISolver
{
    void Expect();
    void Update();
    ICommand Decide();
     List<ICommand> Decide(int commandsLimit);
}
