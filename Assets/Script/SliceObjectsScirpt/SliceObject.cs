using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceObject : MonoBehaviour
{
    SliceMe sliceMe;

    private void Start()
    {
        sliceMe = this.GetComponent<SliceMe>();
    }

    public void SliceThisObject(SlicerPlane slicePlane)
    {
        sliceMe.Slice(slicePlane);
    }

    
}
