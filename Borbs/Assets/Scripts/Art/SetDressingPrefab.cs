using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDressingPrefab : MonoBehaviour
{
    public SetDressingData SetData;
    public void Dress()
    {
        float scale = Random.Range(SetData.minScale , SetData.maxScale);
        transform.localScale = new Vector3(scale , scale , scale);
        transform.eulerAngles = new Vector3(Random.Range(-SetData.xRanRotation , SetData.xRanRotation) , Random.Range(-SetData.yRanRotation , SetData.yRanRotation) , Random.Range(-SetData.zRanRotation , SetData.zRanRotation));
    }

    public void Awake()
    {
        if (SetData == null)
        {
            Debug.LogWarning("Unessecary Setdressing Randomizer, If you intent to use these make sure to add a setdata or remove this component");
            return;
        }
        //Dress();
        Destroy(this);
    }
}
