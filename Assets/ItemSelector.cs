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

    [SerializeField]
    private IntReactiveProperty numUse = new IntReactiveProperty(0);
    [SerializeField]
    private IntReactiveProperty numStock = new IntReactiveProperty(100);

    private void Awake()
    {
        stockTxt.text = "x " + numStock.Value;

        numUse.Select((arg) => Tuple.Create(arg <= 0, arg >= numStock.Value))
              .Subscribe((Tuple<bool, bool> obj) =>
              {
                  minusButton.interactable = !obj.Item1;
                  plusButton.interactable = !obj.Item2;
              });

        numUse.SubscribeToText(counterTxt);

        plusButton.OnClickAsObservable()
                  .Where(_ => numUse.Value + 1 <= numStock.Value)
                  .Subscribe(_ => numUse.Value++);
        
        minusButton.OnClickAsObservable()
                   .Where(_ => numUse.Value >= 0)
                   .Subscribe(_ => numUse.Value--);

        maxButton.OnClickAsObservable()
                 .Subscribe(_ => numUse.Value = numStock.Value);
    }
}
