using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Zagadki
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartButtonClicked(object sender, EventArgs e)
        {
            var userName = await DisplayPromptAsync("Введите ваше имя", "Как вас зовут?");

            var questionsByCategory = new Dictionary<string, List<(string Text, string[] Options, int CorrectAnswerIndex)>>
            {
                {
                    "Природа", new List<(string Text, string[] Options, int CorrectAnswerIndex)>
                    {
                        ("Что может взойти на гору днем, но никогда не спустится?", new[] {"Солнце", "Луна", "Звезда"}, 0),
                        ("Как называется самая большая планета в нашей солнечной системе?", new[] {"Юпитер", "Марс", "Венера"}, 0),
                        ("Как называется самая высокая гора на Земле?", new[] {"Эверест", "Килиманджаро", "Эльбрус"}, 0),
                        ("Какой элемент составляет около 78% земной атмосферы?", new[] {"Кислород", "Азот", "Углекислый газ"}, 1),
                        ("Как называется самый большой океан на Земле?", new[] {"Индийский", "Атлантический", "Тихий"}, 2),
                    }
                },
                {
                    "Общие", new List<(string Text, string[] Options, int CorrectAnswerIndex)>
                    {
                        ("Что может быть у всех, но ни у кого не одно и то же?", new[] {"Время", "Имя", "Мечта"}, 1),
                        ("Какая национальность у Александра Флеминга, который открыл пенициллин?", new[] {"Шотландец", "Ирландец", "Англичанин"}, 0),
                        ("В какой стране изначально появилась блюдо суши?", new[] {"Китай", "Япония", "Таиланд"}, 1),
                        ("Какой материк является самым маленьким и самым сухим на Земле?", new[] {"Антарктида", "Австралия", "Евразия"}, 1),
                        ("Кто написал роман \"Война и мир\"?", new[] {"Лев Толстой", "Федор Достоевский", "Александр Пушкин"}, 0),
                    }
                }
            };

            var selectedCategory = await DisplayActionSheet("Выберите категорию", "Отмена", null, questionsByCategory.Keys.ToArray());

            if (selectedCategory == "Отмена" || selectedCategory == null)
            {
                return;
            }

            var questions = questionsByCategory[selectedCategory];

            int correctAnswers = 0;
            int totalQuestions = questions.Count;

            Random random = new Random();
            while (questions.Count > 0)
            {
                int randomIndex = random.Next(questions.Count);
                var question = questions[randomIndex];
                questions.RemoveAt(randomIndex);

                var userAnswer = await DisplayActionSheet(question.Text, "Отмена", null, question.Options);
                if (userAnswer != null && userAnswer != "Отмена" && userAnswer == question.Options[question.CorrectAnswerIndex])
                {
                    correctAnswers++;
                }
            }

            await DisplayAlert("Результаты", $"{userName}, вы ответили правильно на {correctAnswers} из {totalQuestions} вопросов.", "Ок");
        }
    }
}