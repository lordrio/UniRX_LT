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
        stockTxt.text = "x " + numStock;
        counterTxt.text = numUse.ToString();
        minusButton.interactable = (numUse > 0);
        plusButton.interactable = (numUse < numStock);

        plusButton.OnClickAsObservable()
                  .Select(_ => numUse + 1)
                  .Where(arg => arg <= numStock)
                  .Do(arg => plusButton.interactable = numStock > (numUse = arg)  )
                  .Do(arg => minusButton.interactable = true )
                  .SubscribeToText(counterTxt);

        minusButton.OnClickAsObservable()
                   .Select(_ => numUse - 1)
                   .Where(arg => arg >= 0)
                   .Do(arg => minusButton.interactable = 0 < (numUse = arg))
                   .Do(arg => plusButton.interactable = true)
                   .SubscribeToText(counterTxt);

        maxButton.OnClickAsObservable()
                 .Do(_ => minusButton.interactable = !(plusButton.interactable = false))
                 .Select(_ => numUse = numStock)
                 .SubscribeToText(counterTxt);
    }
}
