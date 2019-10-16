using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSetter 
{
   public enum CubeArrengment
    {
        Square,
        Circle,
        CircularSpiral,
        Default
    }

    public struct ThrowableConstruction
    {
        int throwableCount;
        public List<Vector3> cubePoss;
        public CubeArrengment arrengment;

        public ThrowableConstruction(int count)
        {
           this.throwableCount = count;
           cubePoss = new List<Vector3>(count);
           arrengment = CubeArrengment.Default;
        }
    }

    public ThrowableConstruction cubeConst;

    public CubeSetter(int cubeCount)
    {
        cubeConst = new ThrowableConstruction(cubeCount);
        SetCubeConstruction();
    }

    void SetCubeConstruction()
    {


        switch(cubeConst.arrengment)
        {
            default:
            {
               break;
            }
        }
            
    }

}
