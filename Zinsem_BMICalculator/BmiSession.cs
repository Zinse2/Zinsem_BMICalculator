namespace Zinsem_BMICalculator;

public enum Gender
{
    None,
    Male,
    Female
}

public class BmiSession
{
    public Gender Gender { get; set; }
    public double HeightIn { get; set; }
    public double WeightLb { get; set; }

    public double CalculateBmi()
        => (WeightLb * 703.0) / (HeightIn * HeightIn);

    public string GetHealthStatus(double bmi)
    {
        if (Gender == Gender.Male)
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

    public string GetRecommendation(string status)
    {
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
