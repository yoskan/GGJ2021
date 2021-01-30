
using UnityEditor;
using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    [MenuItem("HaH/SetDressing/Snap Selected To Ground %g")]
    public static void Ground()
    {
        foreach (var transform in Selection.transforms)
        {
            var hits = Physics.RaycastAll(transform.position + Vector3.up , Vector3.down , 1000f);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != transform.gameObject && "Terrain" == hit.collider.transform.name)
                {

                    transform.position = hit.point;
                    break;
                }

            }
        }
    }

}