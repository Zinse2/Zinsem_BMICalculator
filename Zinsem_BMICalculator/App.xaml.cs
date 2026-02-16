namespace Zinsem_BMICalculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Stack-based navigation root
            MainPage = new NavigationPage(new InputPage());
        }
    }
}