using System;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public class Vector3Wrapper
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Wrapper(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    public static Vector3 UnityVec3(Vector3Wrapper wrapper)
    {
        return new Vector3(wrapper.X, wrapper.Y, wrapper.Z);
    }

    public static Vector3[] CustomVec3Unwrapper(Vector3Wrapper[] arr)
    {
        List<Vector3> wrapper = new List<Vector3>();
        for (int i = 0; i < arr.Length; ++i)
        {
            wrapper.Add(Tool.UnityVec3(arr[i]));
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
