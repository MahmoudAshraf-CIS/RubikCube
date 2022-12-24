using Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikCubeClient : MonoBehaviour
{
    public int size = 3;
    [HideInInspector]
    [SerializeField]
    public RubikCubeModel model;
    public RubikCubeView view;
    
    public IExecuter executer;

    ISolver solver;
 
    void Start()
    {
        Debug.Log(view.matSet);
        Debug.Log(view.matSet.Up);
        Debug.Log(view.matSet.Up.color);
        model = new RubikCubeModel(size, false,view.matSet);
        if (!view)
        {
            Debug.LogError("View can not be null!");
            Destroy(this);
        }

        view.Init(model.Size);

        executer = new RubikCubeViewExecuter();
        executer.AddCommand(new ViewCmdIdel(ref view));

        solver = new HumanSolver(ref view, ref model);
    }



    private void OnDestroy()
    {
        model.SaveState();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // Clear history
            PlayerPrefs.DeleteAll();
        }

        CheckForKeyboardCommand();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //modelExecuter.Undo();
            executer.Undo();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            //solver.Expect()
            //viewExecuter.AddCommand(new ViewCmdHumanSolver(ref view));
            solver.Expect();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //// single command solver
            //ICommand cmd = solver.Decide();
            //if (cmd != null)
            //{
            //   executer.AddCommand(cmd);
            //}

            // multi commands solver
            List<ICommand> cmds = solver.Decide(2);
            if(cmds != null && cmds.Count > 0)
            {
                executer.AddCommand(cmds);
            }

            if (model.Solved())
            {
                Debug.Log("Winner winner chicked dinner");
            }
        }

        if (Input.GetMouseButton(0))
        {
            executer.Update();
            solver.Update();
        }

        
    }

    void CheckForKeyboardCommand()
    {
         
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // front
            if (Input.GetKeyDown(KeyCode.F))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, -90, view.GetFaceRoot(FaceName.Front).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Front, -90));
            }
                
            //back
            else if (Input.GetKeyDown(KeyCode.B))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, -90, view.GetFaceRoot(FaceName.Back).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Back, -90));
            }
            //up
            else if (Input.GetKeyDown(KeyCode.U))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, -90, view.GetFaceRoot(FaceName.Up).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Up, -90));

            }
            //down
            else if (Input.GetKeyDown(KeyCode.D))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, -90, view.GetFaceRoot(FaceName.Down).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Down, -90));

            }
            //right
            else if (Input.GetKeyDown(KeyCode.R))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, -90, view.GetFaceRoot(FaceName.Right).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Right, -90));

            }
            //left
            else if (Input.GetKeyDown(KeyCode.L))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, -90, view.GetFaceRoot(FaceName.Left).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Left, -90));

            }
        }
        else
        {

            // front
            if (Input.GetKeyDown(KeyCode.F))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, 90, view.GetFaceRoot(FaceName.Front).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Front, 90));

            }
            //back
            else if (Input.GetKeyDown(KeyCode.B))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, 90, view.GetFaceRoot(FaceName.Back).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Back, 90));
            }
            //up
            else if (Input.GetKeyDown(KeyCode.U))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, 90, view.GetFaceRoot(FaceName.Up).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Up, 90));

            }
            //down
            else if (Input.GetKeyDown(KeyCode.D))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, 90, view.GetFaceRoot(FaceName.Down).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Down, 90));

            }
            //right
            else if (Input.GetKeyDown(KeyCode.R))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, 90, view.GetFaceRoot(FaceName.Right).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Right, 90));

            }
            //left
            else if (Input.GetKeyDown(KeyCode.L))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, 90, view.GetFaceRoot(FaceName.Left).transform.rotation));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Left, 90));

            }

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //modelExecuter.AddCommand(new ModelCmdIdel(ref model));
            //viewExecuter.AddCommand(new ViewCmdIdel(ref view));
        }
    }
}
