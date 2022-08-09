using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class ObjectPool<T> where T : Component
    {
        private int _size;
        private Transform _container;
        private T _prefab;

        private List<T> _pool;

        public ObjectPool(int size, Transform poolContainer, T prefab)
        {
            _size = size;
            _container = poolContainer;
            _prefab = prefab;
            _pool = new List<T>(_size);
        }

        public void Initialize()
        {
            for (int i = 0; i < _size; i++)
            {
                AddElementToPool(_prefab);
            }
        }

        private T AddElementToPool(T element)
        {
            var obj = Object.Instantiate(_prefab, _container);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);

            return obj;
        }

        public T GetElement()
        {
            for(int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeSelf == false)
                {
                    _pool[i].gameObject.SetActive(true);
                    return _pool[i];
                }
            }

            var element = AddElementToPool(_prefab);
            element.gameObject.SetActive(true);

            return element;
        }

        public void ReturnElementToPool(T element)
        {
            if (_pool.Contains(element))
            {
                element.gameObject.SetActive(false);
                element.transform.parent = _container;
            }
        }

        public List<T> GetAllElementsFromPool()
        {
            return _pool;
        }
    }
}
