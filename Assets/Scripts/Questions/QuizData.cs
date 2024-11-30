using System.Collections.Generic;

public class QuizData
{
    public List<QuestionData> Questions { get; }

    public QuizData(List<QuestionData> questions)
    {
        Questions = questions;
    }

    public QuestionData this[int index] => Questions[index];
    public int Length => Questions.Count;

    public static readonly string[] Answers = new[] { "A", "B", "C", "D" };
}

public class QuestionData
{
    public string Category { get; }
    public string Question { get; }
    public List<string> Choices { get; }
    public string Answer { get; }

    public QuestionData(string category, string question, List<string> choices, string answer)
    {
        Category = category;
        Question = question;
        Choices = choices;
        Answer = answer;
    }

    public override string ToString()
    {
        return $"Category: {Category}\n" +
               $"Question: {Question}\n" +
               $"Choices: {string.Join(", ", Choices)}\n" +
               $"Answer: {Answer}";
    }
}