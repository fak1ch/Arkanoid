using System;
using UnityEngine;

namespace Pool
{
    [Serializable]
    public class PoolData<T>
    {
        public int size;
        public Transform container;
        public T prefab;
    }
}
