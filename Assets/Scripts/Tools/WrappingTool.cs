using System;
using System.Collections.Generic;
using UnityEngine;

public static class WrappingTool
{
    public class CustomVectorThree
    {
        public float X;
        public float Y;
        public float Z;

        public CustomVectorThree(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    public static Vector3 CustomToUnityVectorThree(CustomVectorThree wrapper)
    {
        return new Vector3(wrapper.X, wrapper.Y, wrapper.Z);
    }

    public static Vector3[] CustomVec3Unwrapper(CustomVectorThree[] arr)
    {
        List<Vector3> wrapper = new List<Vector3>();
        for (int i = 0; i < arr.Length; ++i)
        {
            wrapper.Add(WrappingTool.CustomToUnityVectorThree(arr[i]));
        }
        return wrapper.ToArray();
    }

    public static Queue<Tuple<string, int>> EncapsulateInQueue(Tuple<string, int>[] arr)
    {
        Queue<Tuple<string, int>> myQ = new Queue<Tuple<string, int>>();
        for (int i = 0; i < arr.Length; ++i)
        {
            myQ.Enqueue(arr[i]);
        }
        return myQ;
    }
}
