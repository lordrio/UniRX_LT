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

        // 違うやり方
        /*var checker = numUse.Select((arg) => Tuple.Create(arg > 0, arg < numStock.Value)).ToReactiveProperty();

        checker.Select((arg) => arg.Item1)
               .DistinctUntilChanged()
               .SubscribeToInteractable(minusButton);

        checker.Select((arg) => arg.Item2)
               .DistinctUntilChanged()
               .SubscribeToInteractable(plusButton);*/

        numUse.SubscribeToText(counterTxt);

        plusButton.OnClickAsObservable()
                  .Subscribe(_ => numUse.Value++);
        
        minusButton.OnClickAsObservable()
                   .Subscribe(_ => numUse.Value--);

        maxButton.OnClickAsObservable()
                 .Subscribe(_ => numUse.Value = numStock.Value);
    }
}
