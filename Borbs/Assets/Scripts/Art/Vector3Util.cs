using UnityEngine;

public static class Vector3Util
{
    // cheaper but sqaure 
    public static float DistanceFlatSqrY(this Vector3 pointA , Vector3 pointB)
    {
        return (new Vector2(pointA.x , pointA.y) - new Vector2(pointB.x , pointB.y)).sqrMagnitude;
    }

    // cheaper but sqaure 
    public static float DistanceFlatSqrZ(this Vector3 pointA , Vector3 pointB)
    {
        return (new Vector2(pointA.x , pointA.z) - new Vector2(pointB.x , pointB.z)).sqrMagnitude;
    }

    //actualy distance
    public static float DistanceFlatZ(this Vector3 a , Vector3 b)
    {
        return Vector2.Distance(new Vector2(a.x , a.z) , new Vector2(b.x , b.z));
    }

    //actualy distance
    public static float DistanceFlatY(this Vector3 a , Vector3 b)
    {
        return Vector2.Distance(new Vector2(a.x , a.y) , new Vector2(b.x , b.y));
    }

    public static bool RangeCheck(this Vector3 pointA , Vector3 pointB , float range) =>
        (pointA - pointB).sqrMagnitude < (range * range);

    public static bool RangeCheckFlat(this Vector3 pointA , Vector3 pointB , float range)
    {
        pointA.y = pointB.y;
        return (pointA - pointB).sqrMagnitude < (range * range);
    }

    public static bool RangeCheck(this Transform transA , Transform transB , float range) =>
        RangeCheck(transA.position , transB.position , range);

    public static bool RangeCheckFlat(this Transform transA , Transform transB , float range) =>
        RangeCheckFlat(transA.position , transB.position , range);

}