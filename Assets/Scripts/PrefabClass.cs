using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabClass
{
    public ObjectType objectType;
    public Collider colliders;
    public Color color;
    public TransformReference tRef;
    public System.Type[] components;

    public PrefabClass(ObjectType objT, Collider coll, Color color_, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        var go = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Sphere"));
        go.name = "Prefab";

        go.GetComponent<Renderer>().material.color = color_;

        go.transform.position = position;
        go.transform.rotation = rotation;
        go.transform.localScale = scale;

        if (coll == Collider.Collider)
        {
            if (go.TryGetComponent<UnityEngine.Collider>(out UnityEngine.Collider collider) == true)
            {
                go.GetComponent<UnityEngine.Collider>().isTrigger = false;
            }

            if (go.TryGetComponent<UnityEngine.Collider>(out UnityEngine.Collider collider_) == false)
            {
                go.AddComponent<BoxCollider>();
                go.GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        else if (coll == Collider.Trigger)
        {
            if (go.TryGetComponent<UnityEngine.Collider>(out UnityEngine.Collider collider2) == true)
            {
                go.GetComponent<UnityEngine.Collider>().isTrigger = true;
            }

            if (go.TryGetComponent<UnityEngine.Collider>(out UnityEngine.Collider collider1) == false)
            {
                go.AddComponent<BoxCollider>();
                go.GetComponent<BoxCollider>().isTrigger = true;
            }
        }

        objectType = objT;
        colliders = coll;
        color = go.GetComponent<Renderer>().material.color;
        tRef = TransformReference._transformReference(go);
    }

    public SceneObjectsDTO GetSavedData()
    {
        var save = new SceneObjectsDTO()
        {
            objectType = this.objectType,
            colliders = this.colliders,
            color = this.color,
            position = this.tRef.position,
            rotation = this.tRef.rotation,
            scale = this.tRef.scale,
            components = this.components,
        };
        return save;
    }
}
