using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubikCubeView : MonoBehaviour, IView
{
    
    public RubikCubeMaterialSet matSet;
    [SerializeField]
    GameObject baseCubePrefab;
    [SerializeField]
    GameObject cellPrefab;
    [SerializeField]
    public bool showCellNames;
    [Tooltip("Marging arround each cell, will be added to both sides.")]
    public float margin = 0.01f;

    public int size;
    
    GameObject _cube;
    public GameObject Cube { get => _cube; set => _cube = value; }
    
    

    void Start()
    {
        Cube = Instantiate(baseCubePrefab);
        Cube.transform.parent = this.transform;
        matSet.Init();
        
    }


    public void AddFace(Face f,Transform pivot)
    {
        GameObject faceHolder = new GameObject(f.Name);
        faceHolder.transform.parent = pivot;
        faceHolder.transform.localPosition = Vector3.zero;
        faceHolder.transform.localScale = Vector3.one;
        faceHolder.transform.localRotation = Quaternion.identity;
        float step = (1.0f / f.Size); // + offset
        float xstart = -0.5f + (step/ 2.0f);

        for (int i = 0; i < f.Size; i++)
        {
            for (int j = 0; j < f.Size; j++)
            {
                GameObject cell = Instantiate(cellPrefab, faceHolder.transform);
                cell.transform.localPosition = new Vector3(
                    xstart + step * i,
                    0,
                   -(xstart + step * j)
                    );
                cell.transform.localScale = Vector3.one / f.Size - (2* margin * Vector3.one);
                cell.transform.localRotation = Quaternion.Euler(90, 0, 0);
                cell.gameObject.name = f.GetCell(i, j).Name;

                cell.gameObject.GetComponent<MeshRenderer>().sharedMaterial = matSet.materialSet[f.Name];
                cell.transform.GetChild(0).gameObject.SetActive(showCellNames);
                if (showCellNames)
                {
                    TextMeshPro txt = cell.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                    txt.text = cell.gameObject.name.ToString();
                }
            }
        }

        BoxCollider bc = faceHolder.AddComponent<BoxCollider>();
        bc.size = new Vector3(1, 0.1f, 1);
        bc.center = new Vector3(0, 0.04f, 0);
    }

    public void Init(int size)
    {
        this.size = size;   
         // up
        AddFace(new Face(FaceName.Up, size, Color.yellow),  Cube.transform.GetChild(0));
        // front 
        AddFace(new Face(FaceName.Front, size, Color.yellow),  Cube.transform.GetChild(1));
         // left
        AddFace(new Face(FaceName.Left, size, Color.yellow),  Cube.transform.GetChild(2));

         // right 
        AddFace(new Face(FaceName.Right, size, Color.yellow),  Cube.transform.GetChild(3));
         // back
        AddFace(new Face(FaceName.Back, size, Color.yellow),  Cube.transform.GetChild(4));
         // bottom
        AddFace(new Face(FaceName.Down, size, Color.yellow),  Cube.transform.GetChild(5));
    }

     
}
