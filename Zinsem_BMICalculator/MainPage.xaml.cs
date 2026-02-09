namespace Zinsem_BMICalculator;

public partial class MainPage : ContentPage
{
    private enum Gender
    {
        None,
        Male,
        Female
    }

    private Gender _selectedGender = Gender.None;

    public MainPage()
    {
        InitializeComponent();

        // initialize labels to match slider defaults
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
        // highlight selected with border + slight opacity change
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
    {
        HeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    private void OnWeightChanged(object sender, ValueChangedEventArgs e)
    {
        WeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
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

        // BMI formula: (weight(lb) * 703) / height(in)^2
        double bmi = (weightLb * 703.0) / (heightIn * heightIn);
        double bmiRounded = Math.Round(bmi, 1);

        string status = GetHealthStatus(_selectedGender, bmi);
        string recommendation = GetRecommendation(status);

        string genderText = _selectedGender == Gender.Male ? "Male" : "Female";

        await DisplayAlert(
            "Your calculated BMI results are:",
            $"Gender: {genderText}\n" +
            $"BMI: {bmiRounded}\n" +
            $"Health Status: {status}\n\n" +
            $"Recommendations:\n- {recommendation.Replace("\n", "\n- ")}",
            "Ok"
        );
    }

    private static string GetHealthStatus(Gender gender, double bmi)
    {
        // Gender-based thresholds from the assignment:
        if (gender == Gender.Male)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal Weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
        else // Female
        {
            if (bmi < 18) return "Underweight";
            if (bmi < 24) return "Normal Weight";
            if (bmi < 29) return "Overweight";
            return "Obese";
        }
    }

    private static string GetRecommendation(string status)
    {
        // Matches the recommendation table in your screenshot
        return status switch
        {
            "Underweight" =>
                "Increase calorie intake with nutrient-rich foods (e.g., nuts, lean protein, whole grains).\n" +
                "Incorporate strength training to build muscle mass.\n" +
                "Consult a nutritionist if needed.",

            "Normal Weight" =>
                "Maintain a balanced diet with proteins, healthy fats, and fiber.\n" +
                "Stay physically active with at least 150 minutes of exercise per week.\n" +
                "Keep regular check-ups to monitor overall health.",

            "Overweight" =>
                "Reduce processed foods and focus on portion control.\n" +
                "Engage in regular aerobic exercises (e.g., jogging, swimming) and strength training.\n" +
                "Drink plenty of water and track your progress.",

            "Obese" =>
                "Consult a doctor for personalized guidance.\n" +
                "Start with low-impact exercises (e.g., walking, cycling).\n" +
                "Follow a structured weight-loss meal plan and consider behavioral therapy for lifestyle changes.\n" +
                "Avoid sugary drinks and maintain a consistent sleep schedule.",

            _ => "Maintain healthy habits and consult a professional if you have concerns."
        };
    }
}
