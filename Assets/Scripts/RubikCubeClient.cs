using Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RubikCubeClient : MonoBehaviour
{
 
    [HideInInspector]
    [SerializeField]
    public RubikCubeModel model;
    public RubikCubeView view;
    public CameraView camView;
    public IExecuter executer;

    ISolver solver;

    public bool active = false;
    public UnityAction OnSolved;
    void Start()
    {
        // if new game - then we need the size
        //          model = new RubikCubeModel(size, false,view.matSet);
        // if old game 
        //          model = new RubikCubeModel(size, true,view.matSet);
    }

    public void SetActive(bool a)
    {
        active = a;
    }

    // initialize the game from the saved history
    public void Init()
    {
        // recreate the game from the history
        model = new RubikCubeModel(true, view.matSet);
        
        if (!view || !camView)
        {
            Debug.LogError("Views can not be null!");
            Destroy(this);
        }
        view.Init(model);
        executer = new RubikCubeExecuter();
        executer.SetOnFinish(OnExecuterIsDone);

        solver = new HumanSolver(ref view, ref model);
        Timer.Instance().Activate();
        camView.SetAnimate(false);
    }

    // initialize a new game with cube (size*size)
    public void Init(int size)
    {
        // here we r creating a new game with the size
        model = new RubikCubeModel(false, view.matSet, size);
        if (!view)
        {
            Debug.LogError("View can not be null!");
            Destroy(this);
        }
        view.Init(model.Size);
        executer = new RubikCubeExecuter();
        executer.SetOnFinish(OnExecuterIsDone);
        solver = new HumanSolver(ref view, ref model);
        //executer.AddCommand(new CmdScramble(ref model, ref view, 5));
      
        camView.SetAnimate(false);
    }

   
    private void OnDestroy()
    {
        model.SaveState();
    }

    void Update()
    {
        if (!active || executer.IsRunning())
            return;
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // Clear history
            PlayerPrefs.DeleteAll();
        }

        CheckForKeyboardCommand();
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
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

        }

        if (Input.GetMouseButton(0))
        {
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
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Front, -90));
            }
                
            //back
            else if (Input.GetKeyDown(KeyCode.B))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Back, -90));
            }
            //up
            else if (Input.GetKeyDown(KeyCode.U))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Up, -90));

            }
            //down
            else if (Input.GetKeyDown(KeyCode.D))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Down, -90));

            }
            //right
            else if (Input.GetKeyDown(KeyCode.R))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Right, -90));

            }
            //left
            else if (Input.GetKeyDown(KeyCode.L))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, -90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Left, -90));

            }
        }
        else
        {

            // front
            if (Input.GetKeyDown(KeyCode.F))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Front, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Front, 90));

            }
            //back
            else if (Input.GetKeyDown(KeyCode.B))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Back, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Back, 90));
            }
            //up
            else if (Input.GetKeyDown(KeyCode.U))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Up, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Up, 90));

            }
            //down
            else if (Input.GetKeyDown(KeyCode.D))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Down, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Down, 90));

            }
            //right
            else if (Input.GetKeyDown(KeyCode.R))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Right, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Right, 90));

            }
            //left
            else if (Input.GetKeyDown(KeyCode.L))
            {
                executer.AddCommand(new ViewCmdRotateFace(ref view, FaceName.Left, 90));
                executer.AddCommand(new ModelCmdRotateFace(ref model, FaceName.Left, 90));

            }

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //modelExecuter.AddCommand(new ModelCmdIdel(ref model));
            //viewExecuter.AddCommand(new ViewCmdIdel(ref view));
        }
    }

    

    public void Undo()
    {
        executer.Undo(2);
    }

    public void OnExecuterIsDone()
    {
        if (model.Solved())
        {
            PlayerPrefs.DeleteAll();
            camView.SetAnimate(true);
            Timer.Instance().DeActivate();
            if (OnSolved != null)
                OnSolved.Invoke();
        }
    }
}
