using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

internal class Utils
{

    /// <summary>
    /// Destroy all the children inside a parent transform without destroying the parent
    /// </summary>
    /// <param name="parent">The node of which children will be destroyed</param>
    public static void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Recursively finds an object in the scene hierarchy by name.
    /// Source: https://forum.unity.com/threads/solved-find-a-child-by-name-searching-all-subchildren.40684/#post-260028
    /// </summary>
    /// <param name="parent">Transform component of the node where the search will begin</param>
    /// <param name="name">Name of the object that we are looking for</param>
    /// <returns></returns>
    public static Transform RecursiveFind(Transform parent, string name)
    {
        if (parent.name.Equals(name)) return parent;
        foreach (Transform child in parent)
        {
            Transform result = RecursiveFind(child, name);
            if (result != null) return result;
        }
        return null;
    }

    /*
    /// <summary>
    /// something between -PI and PI
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    internal static float SmallerAngle(float start, float end)
    {
        start %= 2 * Mathf.PI;
        end %= 2 * Mathf.PI;
        bool isClockwise = IsClockwise(start, end);
        bool isDecreasing = start > end;
        float result;

        if (isClockwise && !isDecreasing)
        {
            result = end - start + 2 * Mathf.PI;
        }
        else if (!isClockwise && isDecreasing)
        {

            result = end - start - 2 * Mathf.PI;
        }
        else
        {
            result = end - start;
        }
        Debug.Log(Mathf.Round(result / Mathf.PI * 10) / 10f);
        return result;

    }

    internal static bool IsClockwise(float start, float end)
    {
        return (end - start + 2 * Mathf.PI) % (2 * Mathf.PI) < Mathf.PI;
    }



    /// <summary>
    /// Generate two orthonormal basis vectors for a plane defined by a normal vector.
    /// This method should be ideally a static method if we want to use it without adding the script as a component.
    /// Idea from https://math.stackexchange.com/a/2373889
    /// </summary>
    /// <param name="planeNormal">Normal vector of a plane</param>
    /// <returns></returns>
    public static Tuple<Vector3, Vector3> OrthonormalBasis(Vector3 planeNormal)
    {

        if (planeNormal.Equals(Vector3.zero))
        {
            throw new Exception("Cannot find orthonormal basis on normal == zero vector");
        }
        var v1 = Vector3.Cross(planeNormal, new Vector3(1f, 0f, 0f));
        var v2 = Vector3.Cross(planeNormal, new Vector3(0f, 1f, 0f));
        var v3 = Vector3.Cross(planeNormal, new Vector3(0f, 0f, 1f));
        v1 = v1.Equals(Vector3.zero) ? v3 : v1;
        v2 = v2.Equals(Vector3.zero) ? v3 : v2;

        v1 = Vector3.Normalize(v1);
        v2 = Vector3.Normalize(v2);

        return new Tuple<Vector3, Vector3>(v1, v2);
    }

    /// <summary>
    /// Generate a random number from a random distribution with given mean and standard deviation.
    /// Adapted from 
    /// </summary>
    /// <param name="mean"></param>
    /// <param name="stdDev"></param>
    /// <returns></returns>
    public static double RandomNormal(double mean, double stdDev)
    {
        if (stdDev < 0.0001)
        {
            throw new Exception("Cannot generate random variable of zero or negative standard deviation");
        }

        double u1 = Random.Range((float)Math.Pow(10, -10), 1); //uniform(0,1] random doubles
        double u2 = Random.Range((float)Math.Pow(10, -10), 1);
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;
    }


    /// <summary>
    /// Project a point onto 3D plane
    /// </summary>
    /// <param name="origin">Point of origin that defines the plane.</param>
    /// <param name="normal">Normal vector that defines the plane. Does not need to be unit length</param>
    /// <param name="target">Point that will be projected onto plane</param>
    /// <returns></returns>
    public static Vector3 Project3D(Vector3 origin, Vector3 normal, Vector3 target)
    {
        if (normal == Vector3.zero)
        {
            // graceful failure
            Debug.LogError("Provided normal vector is zero. Cannot normalize a zero vector");
            return target;
        }
        var v = target - origin;
        normal = Vector3.Normalize(normal);
        return origin + Vector3.ProjectOnPlane(v, normal);
    }

    /// <summary>
    /// Create a random point on a unit circle
    /// </summary>
    /// <returns>A 2D point of distance 1 from the origin</returns>
    public static Vector2 RandomOnUnitCircle
    {
        get {
            return GetRandomOnUnitCircle();
        }
    }
    private static Vector2 GetRandomOnUnitCircle()
    {
        var r = 1;
        var theta = Random.Range(0, 2 * Mathf.PI);
        return new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
    }

    /// <summary>
    /// Check if given enum includes at least one of the flags
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public static bool EnumHasOneOf(Enum @enum, Enum[] flags)
    {
        foreach (var flag in flags)
        {
            if (@enum.HasFlag(flag)) return true;
        }
        return false;
    }




    /// <summary>
    /// Finds the version of iOS as a single number that can be compared
    /// </summary>
    /// <returns></returns>
    public static float GetiOSVersion()
    {
#if UNITY_IOS
        string[] ver = UnityEngine.iOS.Device.systemVersion.Split('.');
        if (float.TryParse(ver[0], out float iOSVersion))
        {
            return iOSVersion;
        }
        Debug.LogError("Could not find iOS version.");
        Debug.Log("UnityEngine.iOS.Device.systemVersion:" + UnityEngine.iOS.Device.systemVersion);
#else
        Debug.LogError("Not on iOS. Could not get iOS version.");
#endif
        return 0;

    }

    /// <summary>
    /// When this rotation is applied to an object, it will be facing the main camera
    /// </summary>
    /// <param name="position">World coordinate of object location</param>
    /// <param name="normal">Upwards vector of object</param>
    /// <returns></returns>
    public static Quaternion LookAtCamera(Vector3 position, Vector3 normal)
    {
        var here = Camera.main.ScreenToWorldPoint(StateManager.ScreenMidpoint);
        var front = Vector3.ProjectOnPlane(here - position, normal);
        var rotation = Quaternion.LookRotation(front, normal);

        return rotation;
    }

    /// <summary>
    /// Retrieve the lengths of animation clips from an animator
    /// </summary>
    /// <param name="animator">Animator component that holds the animation clips</param>
    /// <param name="clipLengths">Dictionary that will be populated with the lengths of animation clips</param>
    public static void GetAnimationClipTimes(Animator animator, out Dictionary<string, float> clipLengths)
    {
        clipLengths = new Dictionary<string, float>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            clipLengths[clip.name] = clip.length;
            Debug.Log($"In Utilss: Name {clip.name} Length {clip.length}");
        }
    }

    /// <summary>
    /// Set Timeout for a function call. Use like JavaScript's setTimeout
    /// Useful for when waiting for an IAP action to finish processing.
    /// </summary>
    /// <param name="callback">actions to execute later</param>
    /// <param name="milliseconds">amount of time to wait for before executing callback</param>
    /// <returns></returns>
    public static IEnumerator SetTimeout(UnityAction callback, float milliseconds)
    {
        yield return new WaitForSeconds(milliseconds/1000f);
        Debug.Log($"Calling callback from SetTimeout: {callback.Method.Name}");
        callback();
    }

    /// <summary>
    /// Set a Frame-out for a function call.
    /// Use like JavaScript's setTimeout except that the second parameter is number of frames, not milliseconds.
    /// Useful for when you want to wait for a frame while a GameObject gets destroyed.
    /// </summary>
    /// <param name="callback">actions to execute later</param>
    /// <param name="frameCounts">number of frames to wait for before executing callback</param>
    /// <returns></returns>
    public static IEnumerator SetFrameout(UnityAction callback, int frameCounts)
    {
        for (int i = 0; i < frameCounts; i++)
        {
            yield return null;
        }
        callback();
    }

    /// <summary>
    /// Check that an array includes a certain comparable element
    /// </summary>
    /// <typeparam name="T">Type T of element</typeparam>
    /// <param name="array">Array to look through</param>
    /// <param name="element">Element we are looking for</param>
    /// <returns></returns>
    public static bool ArrayHas<T>(T[] array, T element)
    {
        foreach (var item in array)
        {
            if (item.Equals(element)) return true;
        }
        
        return false;
    }
    */
}