namespace Zinsem_BMICalculator;

public partial class ResultPage : ContentPage
{
    private readonly BmiSession _session;
    private readonly double _bmiRounded;
    private readonly string _status;

    public ResultPage(BmiSession session)
    {
        InitializeComponent();

        _session = session;

        double bmi = _session.CalculateBmi();
        _bmiRounded = Math.Round(bmi, 1);
        _status = _session.GetHealthStatus(bmi);

        GenderLabel.Text = $"Gender: {_session.Gender}";
        BmiLabel.Text = $"BMI: {_bmiRounded}";
        CategoryLabel.Text = $"Health Status: {_status}";
    }

    private async void OnGoToRecommendationsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecommendationsPage(_session, _bmiRounded, _status));
    }

    private async void OnBackToInputClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(); // back to Page 1
    }
}
