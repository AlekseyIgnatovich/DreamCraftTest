using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public event Action OnPressLeftButton;   
    public event Action OnPressRightButton;   
    public event Action OnFinishPress;

    public TextMeshProUGUI ScoresText => scoresText;
    
    [SerializeField] private ButtonWrapper leftButton;
    [SerializeField] private ButtonWrapper rightButton;

    [SerializeField] private TextMeshProUGUI scoresText;
    
    void Start()
    {
        leftButton.OnPressDown += () => OnPressLeftButton?.Invoke();
        leftButton.OnPressUp += () => OnFinishPress?.Invoke();
        
        rightButton.OnPressDown += () => OnPressRightButton?.Invoke();
        rightButton.OnPressUp += () => OnFinishPress?.Invoke();
    }
}
