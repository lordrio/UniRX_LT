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
        stockTxt.text = "x " + max;
        counterTxt.text = count.ToString();
        minusButton.interactable = (count > min);
        plusButton.interactable = (count < max);
    }

    private void Minus()
    {
        if (--count <= min)
        {
            Min();
            return;
        }

        plusButton.interactable = true;
        counterTxt.text = count.ToString();
    }

    private void Plus()
    {
        if (++count >= max)
        {
            Max();
            return;
        }

        minusButton.interactable = true;
        counterTxt.text = count.ToString();
    }

    private void Max()
    {
        count = max;
        minusButton.interactable = true;
        plusButton.interactable = false;
        counterTxt.text = count.ToString();
    }

    private void Min()
    {
        count = min;
        minusButton.interactable = false;
        plusButton.interactable = true;
        counterTxt.text = count.ToString();
    }

}
