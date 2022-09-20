using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectClass
{
    public ObjectType objectType;
    public Collider colliders;
    public Color color;
    public TransformReference tRef;
    public System.Type[] components;

    public GameObjectClass(ObjectType objT, Collider coll, Color color_, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        var go = new GameObject();
        go.name = "Game Object";

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

        if (go.TryGetComponent<UnityEngine.Renderer>(out UnityEngine.Renderer rend) == false)
        {
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<Renderer>().material.color = color_;
        }

        objectType = objT;
        colliders = coll;
        color = color_;
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
