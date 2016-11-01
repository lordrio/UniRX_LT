using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField]private Button maxButton;
    [SerializeField]private Button minButton;
    [SerializeField]private Button plusButton;
    [SerializeField]private Button minusButton;
    [SerializeField]private Text stockTxt;
    [SerializeField]private InputField counterTxt;
    [SerializeField]private Slider slider;

    [SerializeField] private int max = 5;
    [SerializeField] private int min = -5;
    [SerializeField] private int count = 0;

    private void Awake()
    {
        minButton.onClick.AddListener(Min);
        maxButton.onClick.AddListener(Max);
        plusButton.onClick.AddListener(Plus);
        minusButton.onClick.AddListener(Minus);
        slider.onValueChanged.AddListener(OnSliderUpdate);
        counterTxt.onEndEdit.AddListener(OnEndEdit);
        stockTxt.text = "x " + max;

        slider.maxValue = max;
        slider.minValue = min;
        UpdateView();
    }

    private void OnEndEdit(string input)
    {
        int num = count;
        int.TryParse(input,out num);

        if (num <= min)
        {
            Min();
            return;
        }
        else if (num >= max)
        {
            Max();
            return;
        }

        count = num;
        UpdateView();
    }


    private void OnSliderUpdate(float arg)
    {
        count = (int)slider.value;
        UpdateView();
    }

    private void UpdateView()
    { 
        minusButton.interactable = (count > min);
        plusButton.interactable = (count < max);
        counterTxt.textComponent.color = (count >= 0) ? Color.black : Color.red;
        counterTxt.text = count.ToString();
        slider.value = count;
    }

    private void Minus()
    {
        if (--count <= min)
        {
            Min();
            return;
        }

        UpdateView();
    }

    private void Plus()
    {
        if (++count >= max)
        {
            Max();
            return;
        }

        UpdateView();
    }

    private void Max()
    {
        count = max;
        UpdateView();
    }

    private void Min()
    {
        count = min;
        UpdateView();
    }

}
