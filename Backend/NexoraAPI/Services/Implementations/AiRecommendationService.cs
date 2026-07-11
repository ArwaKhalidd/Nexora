using Microsoft.EntityFrameworkCore;
using NexoraAPI.Models;
using System.Text;
using System.Text.Json;

namespace NexoraAPI.Services
{
    public class AiRecommendationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly AppDbContext _context;

        public AiRecommendationService(HttpClient httpClient, IConfiguration config, AppDbContext context)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"];
            _context = context;
        }

        public async Task<string> GetChatbotResponse(int studentId, string studentMessage)
        {
            // 1. جلب بيانات الطالب (الاسم)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == studentId); // تأكدي إن دي u.Id ولا u.StudentId حسب الداتا بيز بتاعتك

            if (user == null)
            {
                return "Error: Student profile not found.";
            }

            string studentName = $"{user.FirstName} {user.LastName}";

            // 2. حساب متوسط درجات الطالب الفعلي (وليس أول امتحان فقط)
            double score = await _context.StudentAssessments
                .Where(sa => sa.IdStudent == user.StudentId) // بنفلتر بكل امتحانات الطالب
                .AverageAsync(sa => (double?)sa.Score) ?? 0; // بنحسب المتوسط، ولو ممتحنش خالص بياخد 0 (Cold Start)

            score = Math.Round(score, 2); // تقريب الرقم عشان الـ Prompt يكون شكله نظيف

            // 3. بناء البروفايل الحقيقي للذكاء الاصطناعي
            string systemContext = $"You are a smart and friendly academic advisor on the Nexora platform. " +
                                   $"You are assisting a student named {studentName}. " +
                                   $"The student's current overall average assessment score is {score}%. " +
                                   $"Your task: Reply to their question briefly and encouragingly based on their performance, " +
                                   $"and suggest appropriate topics to help them improve. Speak directly to the student.";

            string fullPrompt = $"{systemContext}\n\nStudent Question: {studentMessage}\n\nAcademic Advisor Reply:";

            // 4. الاتصال بـ Google Gemini API
            string baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/";

            string modelName = "gemini-3.5-flash";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = fullPrompt } } } }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            int maxRetries = 3;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var response = await _httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        using var document = JsonDocument.Parse(responseString);

                        // استخراج الرد النصي من JSON الخاص بجوجل
                        return document.RootElement.GetProperty("candidates")[0]
                                       .GetProperty("content").GetProperty("parts")[0]
                                       .GetProperty("text").GetString() ?? "Sorry, I couldn't generate a response.";
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                    {
                        await Task.Delay(2000); // انتظار ثانيتين ثم إعادة المحاولة
                        continue;
                    }
                    var error = await response.Content.ReadAsStringAsync();

                    return $"Status: {response.StatusCode}\n{error}";
                }
                catch (Exception ex)
                {
                    return $"System Exception: {ex.Message}";
                }
            }
            return "AI service is currently busy, please try again later.";
        }
    }
}