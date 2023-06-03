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
    
    [SerializeField] private PressableButton leftPressableButton;
    [SerializeField] private PressableButton rightPressableButton;

    [SerializeField] private TextMeshProUGUI scoresText;
    
    void Start()
    {
        leftPressableButton.OnPressDown += () => OnPressLeftButton?.Invoke();
        leftPressableButton.OnPressUp += () => OnFinishPress?.Invoke();
        
        rightPressableButton.OnPressDown += () => OnPressRightButton?.Invoke();
        rightPressableButton.OnPressUp += () => OnFinishPress?.Invoke();
    }
}
