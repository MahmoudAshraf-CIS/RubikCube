
using System.Collections.Generic;

public interface ICommand {

    /// <summary>
    /// Here goes the command logic,
    /// ,To be Called an executer of <see cref="IExecuter{T}"/>
    /// 
    /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
    /// <c>IExecuter</c>
    /// <typeparamref name="T"/>
    /// the given x- and y-offsets. 
    /// </summary>
    void Execute();
    void Undo();
    void Finish();

    /// <summary>
    /// In case of finishing this command requires executing other commands
    /// on the same executer, return them here (ordered)
    /// they should be added to the <see cref="IExecuter{T}"/> stack once <see cref="Finish"/> is called.
    /// </summary>
    /// <returns></returns>
    List<ICommand> SubCommands();
    void Update();
}

