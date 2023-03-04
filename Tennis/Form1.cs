namespace Tennis
{
    public partial class Form1 : Form
    {
        private const decimal ANNUAL_ADULT_FEE = 5000M;
        private const decimal ANNUAL_FAMILY_FEE = 7000M;
        private const decimal ANNUAL_JUNIOR_FEE = 3000M;

        private const decimal SIX_MONTHS_ADULT_FEE = 3000M;
        private const decimal SIX_MONTHS_FAMILY_FEE = 5000M;
        private const decimal SIX_MONTHS_JUNIOR_FEE = 2000M;

        private const decimal THREE_MONTHS_ADULT_FEE = 2500M;
        private const decimal THREE_MONTHS_FAMILY_FEE = 3500M;

        private const decimal MONTHLY_ADULT_FEE = 2000M;
        private const decimal MONTHLY_FAMILY_FEE = 2500M;

        private const decimal PRIVATE_COACHING_FEE = 1000M;
        private const decimal MATCH_ENTRY_FEE = 1500M;

        private const int MAX_COACHING_HOURS_PER_WEEK = 4;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Please enter a member name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(planComboBox.Text))
            {
                MessageBox.Show("Please select a coaching plan.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                planComboBox.Focus();
                return;
            }

            if (!decimal.TryParse(weightTextBox.Text, out decimal weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid weight in kilograms.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                weightTextBox.Focus();
                return;
            }

            if (!int.TryParse(matchesTextBox.Text, out int matches) || matches < 0)
            {
                MessageBox.Show("Please enter a valid number of matches played.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                matchesTextBox.Focus();
                return;
            }

            int coachingHours = 0;
            if (!string.IsNullOrWhiteSpace(coachingHoursTextBox.Text))
            {
                if (!int.TryParse(coachingHoursTextBox.Text, out coachingHours) || coachingHours < 0)
                {
                    MessageBox.Show("Please enter a valid number of coaching hours required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    coachingHoursTextBox.Focus();
                    return;
                }
                if (coachingHours > MAX_COACHING_HOURS_PER_WEEK)
                {
                    MessageBox.Show($"Members can receive a maximum of {MAX_COACHING_HOURS_PER_WEEK} hours of coaching per week.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    coachingHoursTextBox.Focus();
                    return;
                }
            }

            // Calculate fees
            decimal coachingFee = 0;
            decimal matchFee = matches * MATCH_ENTRY_FEE;
            decimal totalFee = matchFee;
            string plan = planComboBox.Text.ToLower();

            if (plan.Contains("annual"))
            {
                if (plan.Contains("adult"))
                {
                    coachingFee = ANNUAL_ADULT_FEE;
                }
                else if (plan.Contains("family"))
                {
                    coachingFee = ANNUAL_FAMILY_FEE;
                }
                else if (plan.Contains("junior"))
                {
                    coachingFee = ANNUAL_JUNIOR_FEE;
                }
            }
            else if (plan.Contains("6 months"))
            {
                if(plan.Contains("adult"))
            {
                    coachingFee = SIX_MONTHS_ADULT_FEE;
                }
                 else if (plan.Contains("family"))
                {
                    coachingFee = SIX_MONTHS_FAMILY_FEE;
                }
                else if (plan.Contains("junior"))
                {
                    coachingFee = SIX_MONTHS_JUNIOR_FEE;
                }
            }
            else if (plan.Contains("3 months"))
            {
                if (plan.Contains("adult"))
                {
                    coachingFee = THREE_MONTHS_ADULT_FEE;
                }
                else if (plan.Contains("family"))
                {
                    coachingFee = THREE_MONTHS_FAMILY_FEE;
                }
            }
            else if (plan.Contains("monthly"))
            {
                if (plan.Contains("adult"))
                {
                    coachingFee = MONTHLY_ADULT_FEE;
                }
                else if (plan.Contains("family"))
                {
                    coachingFee = MONTHLY_FAMILY_FEE;
                }
            }

            if (privateCoachingCheckBox.Checked)
            {
                coachingFee += PRIVATE_COACHING_FEE;
            }

            if (coachingHours > 0)
            {
                coachingFee += coachingHours * 200M;
            }

            if (planComboBox.Text.Contains("family") && numberOfChildrenNumericUpDown.Value > 0)
            {
                totalFee += (numberOfChildrenNumericUpDown.Value * 1000M);
            }

            if (weight >= 80M)
            {
                totalFee -= (totalFee * 0.1M);
            }

            totalFee += coachingFee;

            // Display results
            resultTextBox.Text = $"Name: {nameTextBox.Text}\r\n\r\n";
            resultTextBox.Text += $"Membership Plan: {planComboBox.Text}\r\n\r\n";
            resultTextBox.Text += $"Coaching Fee: {coachingFee:C}\r\n";
            resultTextBox.Text += $"Match Fee: {matchFee:C}\r\n\r\n";
            resultTextBox.Text += $"Total Fee: {totalFee:C}";
        }
    }
}