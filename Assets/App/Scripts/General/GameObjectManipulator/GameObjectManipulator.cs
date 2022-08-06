using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManipulator : MonoBehaviour
{
    public new T Instantiate<T>(T objectTypeT, Transform parent) where T : Object
    {
        return MonoBehaviour.Instantiate(objectTypeT, parent);
    }

    public new T Instantiate<T>(T objectTypeT, Vector3 positon, Quaternion rotation) where T : Object
    {
        return MonoBehaviour.Instantiate(objectTypeT, positon, rotation);
    }

    public void Destroy<T>(T objectTypeT) where T : Object
    {
        MonoBehaviour.Destroy(objectTypeT);
    }
}
