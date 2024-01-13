namespace FrybreadFusion.Models
{
    public class QuizViewModel
    {
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();

        public int CalculateScore()
        {
            int score = 0;
            foreach (var question in Questions)
            {
                if (question.UserAnswerIndex < 0 || question.UserAnswerIndex >= question.Options.Count)
                {
                    // If the UserAnswerIndex is out of range, it's an automatic wrong answer.
                    question.IsCorrect = false;
                }
                else
                {
                    // Determine if the user's answer is correct.
                    question.IsCorrect = question.UserAnswerIndex == question.CorrectAnswerIndex;
                    if (question.IsCorrect)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
    }

    public class QuestionModel
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public int UserAnswerIndex { get; set; } = -1;
        public bool IsCorrect { get; set;}
    }
}
