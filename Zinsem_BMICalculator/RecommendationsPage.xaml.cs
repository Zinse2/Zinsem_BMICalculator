namespace Zinsem_BMICalculator;

public partial class RecommendationsPage : ContentPage
{
    private readonly BmiSession _session;

    public RecommendationsPage(BmiSession session, double bmiRounded, string status)
    {
        InitializeComponent();

        _session = session;

        SummaryLabel.Text = $"Gender: {_session.Gender}\nBMI: {bmiRounded}\nHealth Status: {status}";
        RecommendationLabel.Text = _session.GetRecommendation(status);
    }

    private async void OnBackToResultClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // back to Page 2
    }

    private async void OnBackToInputClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(); // back to Page 1
    }
}
