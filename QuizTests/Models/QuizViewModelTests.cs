using Xunit;
using FrybreadFusion.Models;

namespace QuizTests.Models
{
    public class QuizViewModelTests
    {
        [Fact]
        public void WrongAnswersIdentifiedTest()
        {
            // Arrange
            var viewModel = new QuizViewModel
            {
                Questions = new List<QuestionModel>
                {
                    new QuestionModel { Text = "What is 2+2?", Options = new List<string> { "3", "4", "5" }, CorrectAnswerIndex = 1, UserAnswerIndex = 0 }
                }
            };

            // Act
            viewModel.CalculateScore();

            // Assert
            Assert.False(viewModel.Questions[0].IsCorrect);
        }

        [Fact]
        public void RightAnswersIdentifiedTest()
        {
            // Arrange
            var viewModel = new QuizViewModel
            {
                Questions = new List<QuestionModel>
                {
                    new QuestionModel { Text = "What is 2+2?", Options = new List<string> { "3", "4", "5" }, CorrectAnswerIndex = 1, UserAnswerIndex = 1 }
                }
            };

            // Act
            viewModel.CalculateScore();

            // Assert
            Assert.True(viewModel.Questions[0].IsCorrect);
        }

        [Fact]
        public void AnswerOutOfBoundsTest()
        {
            // Arrange
            var viewModel = new QuizViewModel
            {
                Questions = new List<QuestionModel>
                {
                    new QuestionModel { Text = "What is the boiling point of water?", Options = new List<string> { "90°C", "100°C", "110°C" }, CorrectAnswerIndex = 1, UserAnswerIndex = 3 }
                }
            };

            // Act
            viewModel.CalculateScore();

            // Assert
            Assert.False(viewModel.Questions[0].IsCorrect);
        }

        [Fact]
        public void NoAnswerProvidedTest()
        {
            // Arrange
            var viewModel = new QuizViewModel
            {
                Questions = new List<QuestionModel>
                {
                    new QuestionModel { Text = "What is the boiling point of water?", Options = new List<string> { "90°C", "100°C", "110°C" }, CorrectAnswerIndex = 1, UserAnswerIndex = -1 }
                }
            };

            // Act
            viewModel.CalculateScore();

            // Assert
            Assert.False(viewModel.Questions[0].IsCorrect);
        }
    }
}
