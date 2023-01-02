using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelCmdRotateFace : RubikCubeOperation ,ICommand
{
    public RubikCubeModel model;
    string facename;
    float degree;
    UnityAction<ICommand> onfinish;
    bool remember = true;

    public ModelCmdRotateFace(ref RubikCubeModel model,string facename,float degree)
    {
        this.model = model;
        this.facename = facename;
        this.degree = degree;
        remember = true;
    }


    void ICommand.Execute()
    {
        if (degree > 0)
        {
            model.Rotate(facename);
        }
        else
        {
            model.RotateDash(facename);
        }
        onfinish.Invoke(this);
    }
    public ICommand GetUndoCmd()
    {
        degree *= -1;
        return this;
    }

    public List<ICommand> SubCommands()
    {
        return null;
    }
    public void SetOnCmdFinish(UnityAction<ICommand> onfinish)
    {
        this.onfinish = onfinish;
    }

  
    public bool ToBeRemembered()
    {
        return remember;
    }

    public void SetToBeRemembered(bool b)
    {
        remember = b;
    }
}
