using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikCubeClient : MonoBehaviour
{
    public int size = 3;
    [HideInInspector]
    public RubikCubeModel model;
    public RubikCubeView view;
    
    public IExecuter<RubikCubeModel> modelExecuter;
    public IExecuter<RubikCubeView> viewExecuter;
 
    void Start()
    {
 

        model = new RubikCubeModel(size);
        if (!view)
            view = this.gameObject.AddComponent<RubikCubeView>();

        view.Init(model.Size);

        modelExecuter = new RubicCubeModelExecuter();
        viewExecuter = new RubikCubeViewExecuter();
        modelExecuter.AddCommand(new ModelCmdIdel(ref model));
        viewExecuter.AddCommand(new ViewCmdIdel(ref view));
    }

 
    public bool active = false;
     

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            modelExecuter.AddCommand(new FrontModelCommand(ref model));
            viewExecuter.AddCommand(new FrontViewCommand(ref view));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            modelExecuter.AddCommand(new ModelCmdIdel(ref model));
            viewExecuter.AddCommand(new ViewCmdIdel(ref view));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            modelExecuter.Undo();
            viewExecuter.Undo();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            active = true;
            viewExecuter.AddCommand(new ViewCmdHumanControl(ref view));
        }
        if (Input.GetMouseButtonUp(0) && active)
        {
            viewExecuter.Stop();

            active = false;
         
        }

        if (Input.GetMouseButton(0) && active)
        {
            viewExecuter.Update();
        }
        
    }
}
