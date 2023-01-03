using Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A client is part of the MVP design pattern acting as the presenter module
/// Responsible for initializing the <seealso cref="RubikCubeModel"/> and <seealso cref="RubikCubeView"/> 
/// <see cref="RubikCubeClient.model"/>Holding a mathimatical representation of the Rubik cube (potentially used for AI solver)
/// <see cref="RubikCubeClient.view"/> Holding the 3D representation of the Cube in game
/// <see cref="RubikCubeClient.model"/>Holding the Game camera interactions
/// <see cref="RubikCubeClient.executer"/> Implements the command mechanizm following the Command design pattern
/// <see cref="RubikCubeClient.solver"/> Responsoble for generating the commands to be executed by the IExecuter
/// <see cref="RubikCubeClient.OnSolved"/> Call back to notify that the cube is solved
/// </summary>
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
     

    public void SetActive(bool a)
    {
        active = a;
    }

     
    /// <summary>
    /// Initialize game from history
    /// </summary>
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
        executer.onFinish += OnExecuterIsDone;
        solver = new HumanSolver(ref view, ref model);
        Timer.Instance().Activate();
        camView.SetAnimate(false);
    }

     
    /// <summary>
    /// initialize a new game with cube (size*size)
    /// </summary>
    /// <param name="size">the cube dimention</param>
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
        executer.onFinish += OnExecuterIsDone;
        solver = new HumanSolver(ref view, ref model);
        ICommand scramble = new CmdScramble(ref model, ref view, 5);
        scramble.SetToBeRemembered(false);
        // the last scramble subcommand callback
        scramble.SubCommands()[scramble.SubCommands().Count-1].onfinish +=((ICommand i) =>
        {
            //scramble is done
            //Enable UI, Clear history, start counting time
            GamePlayUI.Instance().Activate();
            executer.ClearHistory();
            Timer.Instance().Activate();
        });
        //Disable UI, reset timer, stop camera animation
        GamePlayUI.Instance().DeActivate();
        Timer.Instance().Reset();
        executer.AddCommand(scramble);      
        camView.SetAnimate(false);
    }

   
    private void OnDestroy()
    {
        model.SaveState();
    }

    /// <summary>
    /// Called every frame to resolve a command to be executed if any decided by the <see cref="RubikCubeClient.solver"/>
    /// in this case the solver is <see cref="HumanSolver"/>
    /// in general 
    /// <code>
    ///     if (Valid User Input)
    ///         Execute the solver commands
    /// </code>
    /// </summary>
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
            Undo();
        }

        // active indicates that human can interact with the cube or not 
        if (!active || executer.IsRunning())
            return;

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
            List<ICommand> cmds = solver.Decide();
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
 
    /// <summary>
    /// Helper function for developing purbase, expects the input from the keyboard instead of the human solver
    /// </summary>
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
        
    }



    /// <summary>
    /// Called from the <see cref="GamePlayUI.undo"/> onClick action (sat by editor for now)
    /// Also, for developing purpose is called on keyboard.z is down
    /// </summary>
    public void Undo()
    {
        executer.Undo(2);
    }

    /// <summary>
    /// Registered as a callback for <see cref="RubikCubeClient.executer"/> to be called once all the commands are done
    /// Objective is to check for the cube state if solved or not 
    /// <code>
    /// if (solved)
    ///     call <see cref="RubikCubeClient.OnSolved"/>
    /// </code>
    /// </summary>
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
