using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressableButton: Button, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPressDown;   
    public event Action OnPressUp;   
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnPressDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnPressUp?.Invoke();
    }
}
