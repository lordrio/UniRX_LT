using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemSelector : MonoBehaviour
{
    [SerializeField]private Button maxButton;
    [SerializeField]private Button plusButton;
    [SerializeField]private Button minusButton;
    [SerializeField]private Text stockTxt;
    [SerializeField]private Text counterTxt;

    [SerializeField] private int numStock = 100;
    [SerializeField] private int numUse = 0;

    private void Awake()
    {
        maxButton.onClick.AddListener(All);
        //plusButton.onClick.AddListener(Plus);
        minusButton.onClick.AddListener(Minus);
        stockTxt.text = "x " + numStock;
        counterTxt.text = numUse.ToString();
        minusButton.interactable = (numUse > 0);
        plusButton.interactable = (numUse < numStock);

        plusButton.OnClickAsObservable()
                  
                  .Scan(1, (arg1, arg2) => numUse += arg1)
                  .SubscribeToText(counterTxt);
    }

    private void Minus()
    {
        if (--numUse <= 0)
        {
            numUse = 0;
            minusButton.interactable = false;
        }

        plusButton.interactable = true;
        counterTxt.text = numUse.ToString();
    }

    private void Plus()
    {
        if (++numUse >= numStock)
        {
            All();
            return;
        }

        minusButton.interactable = true;
        counterTxt.text = numUse.ToString();
    }

    private void All()
    {
        numUse = numStock;
        minusButton.interactable = true;
        plusButton.interactable = false;
        counterTxt.text = numUse.ToString();
    }
}
