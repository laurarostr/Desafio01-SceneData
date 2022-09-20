using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformReference
{
    public Vector3 position, scale = new Vector3();
    public Quaternion rotation;

    static public TransformReference _transformReference(GameObject go)
    {
        var tf = go.GetComponent<Transform>();
        var tfReference = new TransformReference
        {
            position = tf.position,
            scale = tf.localScale,
            rotation = tf.rotation
        };
        return tfReference;
    }
}
