using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
public interface IExecuter<T>
{
    void AddCommand(ICommand<T> i);

    void Undo();

    void Stop();
    void Update();
    int StackSize();
}
