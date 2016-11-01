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

        // ボタンをnumUseにより有効無効化にする
        numUse.Select((arg) => Tuple.Create(arg <= 0, arg >= numStock.Value))
              .Subscribe((Tuple<bool, bool> obj) =>
              {
                  minusButton.interactable = !obj.Item1;
                  plusButton.interactable = !obj.Item2;
              });

        // 違うやり方
        /*
        // ReactivePropertyに変更
        var checker = numUse.Select((arg) => Tuple.Create(arg > 0, arg < numStock.Value)).ToReactiveProperty();

        // Propertyをボタンに繋ぐ
        checker.Select((arg) => arg.Item1)
               .DistinctUntilChanged()
               .SubscribeToInteractable(minusButton);

        // Propertyをボタンに繋ぐ
        checker.Select((arg) => arg.Item2)
               .DistinctUntilChanged()
               .SubscribeToInteractable(plusButton);*/

        // 表示のホック
        numUse.SubscribeToText(counterTxt);

        // プラス、チェックは入りません（ちゃんとinteractableでボタンを押せないになってます）
        plusButton.OnClickAsObservable()
                  .Subscribe(_ => numUse.Value++);

        // マイナス、チェックは入りません（ちゃんとinteractableでボタンを押せないになってます）
        minusButton.OnClickAsObservable()
                   .Subscribe(_ => numUse.Value--);

        // マックス
        maxButton.OnClickAsObservable()
                 .Subscribe(_ => numUse.Value = numStock.Value);
    }
}
