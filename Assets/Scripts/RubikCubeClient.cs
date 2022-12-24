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
    
    public IExecuter modelExecuter;
    public IExecuter viewExecuter;

    ISolver solver;
 
    void Start()
    {
        model = new RubikCubeModel(size);
        if (!view)
        {
            Debug.LogError("View can not be null!");
            Destroy(this);
        }

        view.Init(model.Size);

        modelExecuter = new RubicCubeModelExecuter();
        viewExecuter = new RubikCubeViewExecuter();
        modelExecuter.AddCommand(new ModelCmdIdel(ref model));
        viewExecuter.AddCommand(new ViewCmdIdel(ref view));

        solver = new HumanSolver(ref view, ref model);
    }

 
   
     

    void Update()
    {
        CheckForKeyboardCommand();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //modelExecuter.Undo();
            viewExecuter.Undo();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            //solver.Expect()
            //viewExecuter.AddCommand(new ViewCmdHumanSolver(ref view));
            solver.Expect();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //viewExecuter.Addcommand(solver.Decive())
            //viewExecuter.Finish();
            ICommand cmd = solver.Decide();
            if (cmd != null)
            {
                viewExecuter.AddCommand(cmd);
            }
        }

        if (Input.GetMouseButton(0))
        {
            viewExecuter.Update();
            solver.Update();
        }
        
    }

    void CheckForKeyboardCommand()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)) ||
            ((Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.A)))
            )
            Debug.Log("sas");
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // front
            if (Input.GetKeyDown(KeyCode.F))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, -90, view.GetFaceRoot(FaceName.Front).transform.rotation));
            //back
            else if (Input.GetKeyDown(KeyCode.B))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, -90, view.GetFaceRoot(FaceName.Back).transform.rotation));
            //up
            else if (Input.GetKeyDown(KeyCode.U))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, -90, view.GetFaceRoot(FaceName.Up).transform.rotation));
            //down
            else if (Input.GetKeyDown(KeyCode.D))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, -90, view.GetFaceRoot(FaceName.Down).transform.rotation));
            //right
            else if (Input.GetKeyDown(KeyCode.R))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, -90, view.GetFaceRoot(FaceName.Right).transform.rotation));
            //left
            else if (Input.GetKeyDown(KeyCode.R))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, -90, view.GetFaceRoot(FaceName.Left).transform.rotation));
        }
        else
        {

            // front
            if (Input.GetKeyDown(KeyCode.F))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, 90, view.GetFaceRoot(FaceName.Front).transform.rotation));
            //back
            else if (Input.GetKeyDown(KeyCode.B))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, 90, view.GetFaceRoot(FaceName.Back).transform.rotation));
            //up
            else if (Input.GetKeyDown(KeyCode.U))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, 90, view.GetFaceRoot(FaceName.Up).transform.rotation));
            //down
            else if (Input.GetKeyDown(KeyCode.D))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, 90, view.GetFaceRoot(FaceName.Down).transform.rotation));
            //right
            else if (Input.GetKeyDown(KeyCode.R))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, 90, view.GetFaceRoot(FaceName.Right).transform.rotation));
            //left
            else if (Input.GetKeyDown(KeyCode.R))
                viewExecuter.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, 90, view.GetFaceRoot(FaceName.Left).transform.rotation));

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //modelExecuter.AddCommand(new ModelCmdIdel(ref model));
            //viewExecuter.AddCommand(new ViewCmdIdel(ref view));
        }
    }
}
