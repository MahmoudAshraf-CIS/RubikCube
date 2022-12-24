using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
public interface IExecuter
{
    void AddCommand(ICommand i);

    void Undo();

    void Finish();
    void Update();
    int StackSize();
}
