using Android.App;
using Android.OS;
using Android.Widget;


namespace CET95_T02_Conversor_EUR_USD_ANDRE
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private EditText? amountInput;
        private TextView? resultText;
        private TextView? fromCurrencyLabel;
        private TextView? toCurrencyLabel;
        private Button? convertButton;
        private Button? swapButton;
        private TextView? rateText;

        private bool isUsdToEur = true;
        private const double ExchangeRate = 1.07; // 1 EUR = 1.07 USD

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize UI components
            amountInput = FindViewById<EditText>(Resource.Id.amountInput);
            resultText = FindViewById<TextView>(Resource.Id.resultText);
            fromCurrencyLabel = FindViewById<TextView>(Resource.Id.fromCurrencyLabel);
            toCurrencyLabel = FindViewById<TextView>(Resource.Id.toCurrencyLabel);
            convertButton = FindViewById<Button>(Resource.Id.convertButton);
            swapButton = FindViewById<Button>(Resource.Id.swapButton);
            rateText = FindViewById<TextView>(Resource.Id.rateText);

            // Set up button click events
            convertButton.Click += ConvertButton_Click;
            swapButton.Click += SwapButton_Click;

            UpdateCurrencyLabels();
        }

        private void ConvertButton_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(amountInput.Text))
            {
                Toast.MakeText(this, "Por favor, insira um valor para converter", ToastLength.Short).Show();
                return;
            }

            if (double.TryParse(amountInput.Text, out double amount))
            {
                double result;
                string resultCurrency;

                if (isUsdToEur)
                {
                    // Convert USD to EUR
                    result = amount / ExchangeRate;
                    resultCurrency = "EUR";
                }
                else
                {
                    // Convert EUR to USD
                    result = amount * ExchangeRate;
                    resultCurrency = "USD";
                }

                resultText.Text = $"Resultado: {result:F2} {resultCurrency}";
            }
            else
            {
                Toast.MakeText(this, "Valor inválido. Por favor, insira um número.", ToastLength.Short).Show();
            }
        }

        private void SwapButton_Click(object sender, System.EventArgs e)
        {
            isUsdToEur = !isUsdToEur;
            UpdateCurrencyLabels();

            // Clear previous result
            resultText.Text = GetString(Resource.String.result_label);

            // Optionally clear the input
            // amountInput.Text = string.Empty;
        }

        private void UpdateCurrencyLabels()
        {
            if (isUsdToEur)
            {
                fromCurrencyLabel.Text = GetString(Resource.String.usd_label);
                toCurrencyLabel.Text = GetString(Resource.String.eur_label);
                rateText.Text = "Taxa atual: 1 EUR = " + ExchangeRate.ToString("F2") + " USD";
            }
            else
            {
                fromCurrencyLabel.Text = GetString(Resource.String.eur_label);
                toCurrencyLabel.Text = GetString(Resource.String.usd_label);
                rateText.Text = "Taxa atual: 1 USD = " + (1 / ExchangeRate).ToString("F4") + " EUR";
            }
        }
    }
}
