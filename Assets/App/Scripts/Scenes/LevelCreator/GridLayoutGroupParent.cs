using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridLayoutGroupParent : MonoBehaviour, IPointerDownHandler
{
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform _rectTransform;
    [SerializeField] private Camera _camera;

    public bool PointerDownOverObject { get; set; }
    public bool PointerOverObject { get; set; }

    private void Update()
    {
        PointerOverObject = RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, _camera);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDownOverObject = RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, 
            eventData.position, null, out Vector2 localPoint);
    }

    private void LateUpdate()
    {
        if (PointerDownOverObject)
            PointerDownOverObject = false;
    }
}
