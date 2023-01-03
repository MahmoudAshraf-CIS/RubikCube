using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.UI;

/// <summary>
/// Based on the user input (mouse movement) will decide what commands to be executed
/// <see cref="RubikCubeClient.solver"/>Holding a mathimatical representation of the Rubik cube (potentially used for AI solver)
/// <typeparam name="RubikCubeView"> Holding the 3D representation of the Cube in game</typeparam>/// </summary>
public class HumanSolver : ISolver
{
    RubikCubeView view;
    RubikCubeModel model;

 
    RaycastHit hit1, hit2;
    Ray ray;

    Vector3 a, b, c;
    public bool active = false;
    Quaternion originalRotation;
    public float angle;

    Collider[] hitColliders;
    public GameObject point1, point2, point3;
 
    public HumanSolver(ref RubikCubeView view,ref RubikCubeModel model)
    {
        this.view = view;
        this.model = model;
    }
 

    
    /// <summary>
    /// Based on the Human input it'll construct a list of commands to be executed 
    /// </summary>
    /// <returns>List of commands based on the human input</returns>
    public List<ICommand> Decide()
    {
        active = false;
        #region debugging
#if UNITY_EDITOR
        GameObject.Destroy(point1);
        GameObject.Destroy(point2);
        GameObject.Destroy(point3);
#endif
        #endregion
        if (hit1.transform != null)
        {
            List<ICommand> cmds = new List<ICommand>();
            
            b = hit2.point;
             
            float angleDiffrence = Quaternion.Angle(originalRotation, hit1.transform.rotation);
            if (angle > 0 )
            {
                cmds.Add(new ViewCmdRotateFace(ref view, hit1.transform.name, 90 - angleDiffrence, hit1.transform.rotation));
                cmds.Add(new ModelCmdRotateFace(ref model, hit1.transform.name, 90));
            }
            else
            {
                cmds.Add(new ViewCmdRotateFace(ref view, hit1.transform.name, -90 + angleDiffrence, hit1.transform.rotation));
                cmds.Add(new ModelCmdRotateFace(ref model, hit1.transform.name, -90));
            }
            return cmds;
        }

        return null;
    }

    /// <summary>
    /// Prepair for the Decition
    ///     Select the face if valid 
    ///     Keep the face in hand till release    
    /// Raycasts from the screen point if hits a face then holds it down till release
    /// </summary>
    public void Expect()
    {
         
        #region debugging 
#if UNITY_EDITOR
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (!canvas)
            canvas = new GameObject("Canvas").AddComponent<Canvas>();

        point1 = new GameObject("a");
        RectTransform trans = point1.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
        trans.sizeDelta = new Vector2(10, 10); // custom size
        point1.AddComponent<Image>();
        point1.transform.SetParent(canvas.transform);


        point2 = new GameObject("b");
        trans = point2.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
        trans.sizeDelta = new Vector2(10, 10); // custom size
        point2.AddComponent<Image>();
        point2.transform.SetParent(canvas.transform);

        point3 = new GameObject("c");
        trans = point3.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
        trans.sizeDelta = new Vector2(10, 10); // custom size
        point3.AddComponent<Image>();
        point3.transform.SetParent(canvas.transform);
#endif
        #endregion
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit1, 100.0f, 1<<10))
        {
            a = Camera.main.WorldToScreenPoint(hit1.point);
            c = Camera.main.WorldToScreenPoint(hit1.transform.position);
            #region debugging
#if UNITY_EDITOR
            point1.transform.position = a;
            point3.transform.position = c;
#endif
            #endregion
            originalRotation = hit1.transform.rotation;
            active = true;
            // make the neighbor edges child to (hit1.transform)

            hitColliders = Physics.OverlapBox(hit1.transform.position, new Vector3(1.0f, 1.0f / view.size, 1.0f), hit1.transform.rotation, 1 << 9);

            foreach (var item in hitColliders)
            {
                item.transform.parent = hit1.transform;
            }
        }
        else { 
            active = false; 
        }
       
    }

    /// <summary>
    /// Update clock is called via <see cref="RubikCubeClient"/>
    /// here it keeps the face rotate following the touch point
    /// </summary>
    public void Update()
    {
        if (!active)
            return;
         
        b = Input.mousePosition;
        #region debugging
#if UNITY_EDITOR
        point2.transform.position = b;
#endif
        #endregion
        angle = Vector3.Angle(a - c, b - c);
        int bc = GetPointSquare(b, c);
        int ac = GetPointSquare(a, c);
        if (AntiClockWise(c, a, b))
        {
            angle *= -1;
        }
        //angle = Mathf.Clamp(angle, -90, 90);
        hit1.transform.rotation = Quaternion.Lerp(originalRotation, originalRotation * Quaternion.Euler(Vector3.up * angle), .2f);
    }


    /// <summary>
    /// In a 2D coordinate system 
    ///  2   |   3
    /// _____|_____  
    ///      |
    ///  1   |   4
    /// assuming the center is always (0,0)  in a reversed coordinate system 
    /// 
    ///      -ve y
    ///       |     
    /// -ve___x___+ve
    ///       |
    ///       +ve y
    /// it'll return 1,2,3 or 4 indecating the square the point is relative to the center
    /// 
    /// If passing Vector3 it'll ignore the z value
    /// </summary>
    /// <param name="center">the (0,0,0) point of the coordinate </param>
    /// <param name="point">the point queruing in which quarter the point is </param>
    /// <returns></returns>
    public int GetPointSquare(Vector2 point)
    {
        Debug.Log(point);
        if (point.x > 0)
            return point.y > 0 ? 3 : 4;

        return point.y > 0 ? 2 : 1;
    }

    public int GetPointSquare(Vector2 point, Vector2 center)
    {
        point.x -= center.x;
        point.y -= center.y;

        if (point.x > 0)
            return point.y > 0 ? 3 : 4;

        return point.y > 0 ? 2 : 1;
    }

    /// <summary>
    /// Checking if point c is on the left side of the vector a --> b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public bool AntiClockWise(Vector2 a, Vector2 b, Vector2 c)
    {
        return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)) > 0;
    }

}
