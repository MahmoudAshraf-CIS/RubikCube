using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Models
{
    public class FaceName
    {
        public readonly static string Up = "Up";
        public readonly static string Down = "Down";
        public readonly static string Front = "Front";
        public readonly static string Back = "Back";
        public readonly static string Right = "Right";
        public readonly static string Left = "Left";



    }
    [System.Serializable]
    public class RubikCubeModel    {

        [SerializeField]
        private  int size;
        
        [SerializeField]
        private Face[] faces;
        [SerializeField]
        public RubikCubeMaterialSet matSet;

        void Init(int size)
        {
            this.size = size;
            this.faces = new Face[] {
                        new Face("---",size, null),
                        new Face(FaceName.Up ,size, matSet.Up),
                        new Face("---",size, null),
                        new Face("---",size, null),
                        new Face(FaceName.Left ,size,matSet.Left),
                        new Face(FaceName.Front,size, matSet.Front),
                        new Face(FaceName.Right,size, matSet.Right),
                        new Face(FaceName.Back ,size, matSet.Back),
                        new Face("---",size, null),
                        new Face(FaceName.Down ,size, matSet.Down),
                        new Face("---",size, null),
                        new Face("---",size, null)
                    };
            //this.faces = new Face[] {
            //            new Face("---",size, Color.clear),
            //            new Face(FaceName.Up   ,size, Color.yellow),
            //            new Face("---",size, Color.clear),
            //            new Face("---",size, Color.clear),
            //            new Face(FaceName.Left ,size, Color.blue),
            //            new Face(FaceName.Front,size, Color.red),
            //            new Face(FaceName.Right,size, Color.green),
            //            new Face(FaceName.Back ,size, new Color(255, 140, 0) ),
            //            new Face("---",size, Color.clear),
            //            new Face(FaceName.Down ,size, Color.white),
            //            new Face("---",size, Color.clear),
            //            new Face("---",size, Color.clear)
            //        };
        }
        public RubikCubeModel(bool oldState, RubikCubeMaterialSet matSet, int size = 6)
        {
            // read the faces from the playerprefs
            // read the size from the player prefs
            Debug.Log("model initiated");
            this.matSet = matSet;
            if (oldState)
            {
                // try to load the old state from player prefs
                string facesJson = PlayerPrefs.GetString("RubikCube.faces", null);
                int oldSize = PlayerPrefs.GetInt("RubikCube.size", 0);
                
                if(string.IsNullOrEmpty(facesJson) || oldSize == 0)
                {
                    // means that their is no old stored state
                    Init(size);
                    return;
                }
                else
                {
                    // we got a valid old state
                    faces = (Face[])JsonConvert.DeserializeObject(
                       facesJson,
                       typeof(Face[]),
                       new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    // materials are not serialized - as we need to change them with the matSet per game
                    // Am consedering aving a settings so the user can change the material set (cube surfaces color set)

                    UpdateCellColorsWithMaterialSet();
                    this.size = oldSize;

                }
            }
            else
            {
                Init(size);
            }
        }

        public void SaveState()
        {
            //Debug.Log("write");
            // write down the faces into the player prefs
            if (size == 0)
                return;

            string facesJson = JsonConvert.SerializeObject(faces, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            PlayerPrefs.SetString("RubikCube.faces", facesJson);
            Debug.Log(facesJson);
            // write down the model size into player prefs
            PlayerPrefs.SetInt("RubikCube.size", this.size);
         
        }

        void UpdateCellColorsWithMaterialSet()
        {
            foreach (var face in faces)
            for (int i = 0; i < face.Size; ++i)
            {
                for (int j = 0; j < face.Size; ++j)
                {
                        face.Cells[i, j].Material = GetFaceMaterial(face.Cells[i, j].OriginalFaceName);
                }
            }
        }

        Material GetFaceMaterial(string OriginalFaceName)
        {
            if(matSet.map.ContainsKey(OriginalFaceName))
                return matSet.map[OriginalFaceName];
            return null;
        }
        public int Size { get => size; set => size = value; }
        public Face Up { get => faces[1]; set => faces[1] = value; }
        public Face Left { get => faces[4]; set => faces[4] = value; }
        public Face Front { get => faces[5]; set => faces[5] = value; }
        public Face Right { get => faces[6]; set => faces[6] = value; }
        public Face Back { get => faces[7]; set => faces[7] = value; }
        public Face Down { get => faces[9]; set => faces[9] = value; }
        public ref Face[] Faces { get => ref faces; }

        Cell[] r, d, l, u;
        public void Rotate(string facename)
        {
            // this is rotating the front face
            switch (facename)
            {
                case var val when val == FaceName.Front:
                    Right.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Up, out d);
                    Left.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Down, out u);

                    Front.RotateRef(ref r, ref d, ref l, ref u);
                    break;
                case var val when val == FaceName.Right:
                    Back.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Right, out d);
                    Front.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Right, out u);

                    Right.RotateRef(ref r, ref d, ref l, ref u);
                    break;
                case var val when val == FaceName.Up:
                    Right.GetEdgeRef(EdgeName.Up, out r);
                    Front.GetEdgeRef(EdgeName.Up, out d);
                    Left.GetEdgeRef(EdgeName.Up, out l);
                    Back.GetEdgeRef(EdgeName.Up, out u);

                    Up.RotateRef(ref r, ref d, ref l, ref u);
                    break;

                case var val when val == FaceName.Left:
                    Front.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Left, out d);
                    Back.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Left, out u);

                    Left.RotateRef(ref r, ref d, ref l, ref u);
                    break;

                case var val when val == FaceName.Back:
                    Left.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Down, out d);
                    Right.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Up, out u);

                    Back.RotateRef(ref r, ref d, ref l, ref u);
                    break;


                case var val when val == FaceName.Down:
                    Right.GetEdgeRef(EdgeName.Down, out r);
                    Back.GetEdgeRef(EdgeName.Down, out d);
                    Left.GetEdgeRef(EdgeName.Down, out l);
                    Front.GetEdgeRef(EdgeName.Down, out u);

                    Down.RotateRef(ref r, ref d, ref l, ref u);
                    break;
                default:
                    break;
            }
        }

        public void RotateDash(string facename)
        {
            switch (facename)
            {
                case var val when val == FaceName.Front:
                    Right.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Up, out d);
                    Left.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Down, out u);

                    Front.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;
                case var val when val == FaceName.Right:
                    Back.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Right, out d);
                    Front.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Right, out u);

                    Right.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;
                case var val when val == FaceName.Up:
                    Right.GetEdgeRef(EdgeName.Up, out r);
                    Front.GetEdgeRef(EdgeName.Up, out d);
                    Left.GetEdgeRef(EdgeName.Up, out l);
                    Back.GetEdgeRef(EdgeName.Up, out u);

                    Up.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;

                case var val when val == FaceName.Left:
                    Front.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Left, out d);
                    Back.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Left, out u);

                    Left.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;

                case var val when val == FaceName.Back:
                    Left.GetEdgeRef(EdgeName.Left, out r);
                    Down.GetEdgeRef(EdgeName.Down, out d);
                    Right.GetEdgeRef(EdgeName.Right, out l);
                    Up.GetEdgeRef(EdgeName.Up, out u);

                    Back.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;


                case var val when val == FaceName.Down:
                    Right.GetEdgeRef(EdgeName.Down, out r);
                    Back.GetEdgeRef(EdgeName.Down, out d);
                    Left.GetEdgeRef(EdgeName.Down, out l);
                    Front.GetEdgeRef(EdgeName.Down, out u);

                    Down.RotateDashRef(ref r, ref d, ref l, ref u);
                    break;
                default:
                    break;
            }
            //faces[6].GetEdgeRef(EdgeName.Left, out r);
            //faces[9].GetEdgeRef(EdgeName.Up, out d);
            //faces[4].GetEdgeRef(EdgeName.Right, out l);
            //faces[1].GetEdgeRef(EdgeName.Down, out u);

            //faces[5].RotateDashRef(ref r, ref d, ref l, ref u);
        }

        public bool Solved()
        {
            return Up.Solved() && Down.Solved() && Right.Solved() && Left.Solved() &&
                Back.Solved() && Front.Solved();
        }
    }

    [System.Serializable]
    public class Face
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int size;

        private Cell[,] cells;
        public Face(string name, int size, Material mat)
        {
            this.name = name;
            this.size = size;
            cells = new Cell[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    cells[i, j] = new Cell(name[0] == '-' ? "-" : name[0].ToString() + (i + j * size).ToString(), mat,name);
                }
            }
        }

        public string Name { get => name; set => name = value; }
        public int Size { get => size; set => size = value; }
        public Cell[,] Cells { get => cells; set => cells = value; }

        public Cell GetCell(int i, int k)
        {
            if (i >= 0 && i < size && k >= 0 && k < size)
                return cells[i, k];
            return null;
        }

        public Cell[] GetEdge(EdgeName edge)
        {
            Cell[] ret = new Cell[size];
            int index = 0;
            switch (edge)
            {
                case EdgeName.Right:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        ret[index] = cells[size - 1, i];
                        index++;
                    }
                    return ret;

                case EdgeName.Down:
                    for (int i = 0; i < size; i++)
                    {
                        ret[index] = cells[i, size - 1];
                        index++;
                    }
                    return ret;
                case EdgeName.Left:
                    for (int i = 0; i < size; i++)
                    {
                        ret[index] = cells[0, i];
                        index++;
                    }
                    return ret;

                case EdgeName.Up:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        ret[index] = cells[i, 0];
                        index++;
                    }
                    return ret;
                default:
                    return null;
            }
        }

        public void SetEdge(EdgeName edge, Cell[] val)
        {
            if (val.Length != size)
            {
                Debug.Log("Wrong endge dimentions!");
                return;
            }


            int index = 0;
            switch (edge)
            {
                case EdgeName.Right:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        cells[size - 1, i] = val[index];
                        index++;
                    }
                    break;

                case EdgeName.Down:
                    for (int i = 0; i < size; i++)
                    {
                        cells[i, size - 1] = val[index]; ;
                        index++;
                    }
                    break;
                case EdgeName.Left:
                    for (int i = 0; i < size; i++)
                    {
                        cells[0, i] = val[index]; ;
                        index++;
                    }
                    break;

                case EdgeName.Up:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        cells[i, 0] = val[index]; ;
                        index++;
                    }
                    break;
                default:
                    break;
            }
        }

        public void GetEdgeRef(EdgeName edgeName, out Cell[] edgeVal)
        {
            edgeVal = new Cell[size];
            int index = 0;
            switch (edgeName)
            {
                case EdgeName.Right:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        edgeVal[index] = cells[size - 1, i];
                        index++;
                    }
                    break;
                case EdgeName.Down:
                    for (int i = 0; i < size; i++)
                    {
                        edgeVal[index] = cells[i, size - 1];
                        index++;
                    }
                    break;
                case EdgeName.Left:
                    for (int i = 0; i < size; i++)
                    {
                        edgeVal[index] = cells[0, i];
                        index++;
                    }
                    break;

                case EdgeName.Up:
                    for (int i = size - 1; i >= 0; i--)
                    {
                        edgeVal[index] = cells[i, 0];
                        index++;
                    }
                    break;
                default:
                    break;
            }
        }



        void RotateDash()
        {
            Cell[,] ret = new Cell[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    ret[i, j] = cells[size - j - 1, i];
                }
            }
            cells = ret;
        }


        void Reverse()
        {
            // Traverse each row of [,]mat
            for (int i = 0; i < size; i++)
            {
                // Initialise start and end index
                int start = 0;
                int end = size - 1;

                // Till start < end, swap the element
                // at start and end index
                while (start < end)
                {

                    // Swap the element
                    Cell temp = cells[i, start];
                    cells[i, start] = cells[i, end];
                    cells[i, end] = temp;

                    // Increment start and decrement
                    // end for next pair of swapping
                    start++;
                    end--;
                }
            }
        }
        void Transpose()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    Cell temp = cells[i, j];
                    cells[i, j] = cells[j, i];
                    cells[j, i] = temp;
                }
            }
        }

        //// https://stackoverflow.com/a/8664879/6739392
        public void RotateRef(ref Cell[] rightEdge, ref Cell[] downEdge, ref Cell[] leftEdge, ref Cell[] uperEdge)
        {
            Reverse();
            Transpose();

            SwapEdge(ref rightEdge, ref uperEdge);
            SwapEdge(ref downEdge, ref uperEdge);
            SwapEdge(ref leftEdge, ref uperEdge);
        }

        public void RotateDashRef(ref Cell[] rightEdge, ref Cell[] downEdge, ref Cell[] leftEdge, ref Cell[] uperEdge)
        {

            Transpose();
            Reverse();

            SwapEdge(ref rightEdge, ref downEdge);
            SwapEdge(ref downEdge, ref leftEdge);
            SwapEdge(ref uperEdge, ref leftEdge);
        }
        static void SwapEdge(ref Cell[] a, ref Cell[] b)
        {

            if (a == null || b == null || a.Length != b.Length)
            {
                Debug.Log("Error mismatch edges!");
                return;
            }
            Cell temp = new Cell("", null,"--");
            for (int i = 0; i < a.Length; i++)
            {
                temp = new Cell(a[i].Name, a[i].Material,a[i].OriginalFaceName);
                a[i].Name = b[i].Name;
                a[i].Material = b[i].Material;
                a[i].OriginalFaceName = b[i].OriginalFaceName;

                b[i].Name = temp.Name;
                b[i].Material = temp.Material;
                b[i].OriginalFaceName = temp.OriginalFaceName;
            }
        }

        public bool Solved()
        {
            Color c = cells[0, 0].Material.color;
            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    if (cells[i, k].Material.color != c)
                        return false;
                }
            }
            return true;
        }
    }
    public enum EdgeName
    {
        Right,
        Down,
        Left,
        Up
    }


    [System.Serializable]
    public class Cell
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private string originalFaceName;

        [JsonIgnore]
        private Material material;
        public Cell(string name, Material material,string originalFaceName)
        {
            this.name = name;
            this.material = material;
            this.originalFaceName = originalFaceName;
        }

        public string Name { get => name; set => name = value; }
        [JsonIgnore]
        public Material Material { get => material; set => material = value; }
        public string OriginalFaceName { get => originalFaceName; set => originalFaceName = value; }

        public override string ToString()
        {
            return name.ToString();
        }
    }


}