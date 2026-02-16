namespace Zinsem_BMICalculator;

public partial class InputPage : ContentPage
{
    private Gender _selectedGender = Gender.None;

    public InputPage()
    {
        InitializeComponent();

        HeightValueLabel.Text = ((int)HeightSlider.Value).ToString();
        WeightValueLabel.Text = ((int)WeightSlider.Value).ToString();

        UpdateGenderUI();
    }

    private void OnMaleTapped(object sender, TappedEventArgs e)
    {
        _selectedGender = Gender.Male;
        UpdateGenderUI();
    }

    private void OnFemaleTapped(object sender, TappedEventArgs e)
    {
        _selectedGender = Gender.Female;
        UpdateGenderUI();
    }

    private void UpdateGenderUI()
    {
        if (_selectedGender == Gender.Male)
        {
            MaleFrame.BorderColor = Colors.Black;
            MaleFrame.Opacity = 1.0;

            FemaleFrame.BorderColor = Colors.Transparent;
            FemaleFrame.Opacity = 0.85;
        }
        else if (_selectedGender == Gender.Female)
        {
            FemaleFrame.BorderColor = Colors.Black;
            FemaleFrame.Opacity = 1.0;

            MaleFrame.BorderColor = Colors.Transparent;
            MaleFrame.Opacity = 0.85;
        }
        else
        {
            MaleFrame.BorderColor = Colors.Transparent;
            FemaleFrame.BorderColor = Colors.Transparent;
            MaleFrame.Opacity = 1.0;
            FemaleFrame.Opacity = 1.0;
        }
    }

    private void OnHeightChanged(object sender, ValueChangedEventArgs e)
        => HeightValueLabel.Text = ((int)e.NewValue).ToString();

    private void OnWeightChanged(object sender, ValueChangedEventArgs e)
        => WeightValueLabel.Text = ((int)e.NewValue).ToString();

    private async void OnGoToResultsClicked(object sender, EventArgs e)
    {
        if (_selectedGender == Gender.None)
        {
            await DisplayAlert("Missing Gender", "Please select Male or Female.", "OK");
            return;
        }

        double heightIn = Math.Round(HeightSlider.Value);
        double weightLb = Math.Round(WeightSlider.Value);

        if (heightIn <= 0 || weightLb <= 0)
        {
            await DisplayAlert("Invalid Input", "Height and weight must be greater than 0.", "OK");
            return;
        }

        var session = new BmiSession
        {
            Gender = _selectedGender,
            HeightIn = heightIn,
            WeightLb = weightLb
        };

        await Navigation.PushAsync(new ResultPage(session));
    }
}
