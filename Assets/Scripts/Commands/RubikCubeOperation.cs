using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikCubeOperation 
{
    public void Reverse(ref RubikCubeModel c)
    {
        c.testState = "reversed";
    }
}
