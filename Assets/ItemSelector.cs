using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField]private Button maxButton;
    [SerializeField]private Button plusButton;
    [SerializeField]private Button minusButton;
    [SerializeField]private Text stockTxt;
    [SerializeField]private Text counterTxt;

    [SerializeField] private int numStock = 100;
    [SerializeField] private int numUse = 0;

    public int NumUse
    {
        get { return numUse; }

        set
        {
            numUse = value;
            plusButton.interactable = true;
            minusButton.interactable = true;

            if (numUse <= 0)
            {
                numUse = 0;
                minusButton.interactable = false;
            }
            else if (numUse >= numStock)
            {
                numUse = numStock;
                plusButton.interactable = false;
            }

            counterTxt.text = numUse.ToString();
        }
    }


    private void Awake()
    {
        maxButton.onClick.AddListener(() => NumUse = numStock);
        plusButton.onClick.AddListener(() => NumUse++);
        minusButton.onClick.AddListener(() => NumUse--);
        stockTxt.text = "x " + numStock;
        counterTxt.text = numUse.ToString();
        minusButton.interactable = (numUse > 0);
        plusButton.interactable = (numUse < numStock);
    }
}
