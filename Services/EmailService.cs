using Org.BouncyCastle.Asn1.Smime;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using WebAPI_React.Data.FindYourPrice.Data;


namespace WebAPI_React.Services
{
    public class EmailService
    {
        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

        public async Task<bool> SendCodeAsync(string email, string code)
        {
            try
            {
                var client = new SmtpClient("smtp.yandex.ru", 587);
                client.Credentials = new NetworkCredential("alexandrradaev13@yandex.com", "assmaawexutcbtbm");
                client.EnableSsl = true;
                var message = new MailMessage();
                message.From = new MailAddress("alexandrradaev13@yandex.com");
                message.To.Add(email);
                message.Subject = "Код подтверждения";
                message.Body = $"Ваш код подтверждения:\n\t{code}";

                await client.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GenerateCode(PostEmail email)
        {
            if(email == null)
            {
                throw new ArgumentNullException("email");
            }
            var normalizedEmail = NormalizeEmail(email.Email);
            var code = Random.Shared.Next(100000, 1000000).ToString();
            StorageHistory.Codes[normalizedEmail] = new VerificationCode
            {
                Email = normalizedEmail,
                Code = code,
                DateAt = DateTime.UtcNow.AddMinutes(2)
            };
            StorageHistory.ConfirmedEmails.Remove(normalizedEmail);

            var send = await SendCodeAsync(normalizedEmail, code);
            if (!send)
            {
                throw new Exception("Не удалось отправить код");
            }

            return ($"Код успешно отправлен на почту: {normalizedEmail}\n" +
                $"Время действия кода 2 минуты");
        }

        public ReturnDto VetifyStatusCode(VerifyCode data)
        {
            if(data == null)
            {
                throw new Exception("Данных нет");
            }
            var normalizedEmail = NormalizeEmail(data.Email);
            if(!StorageHistory.Codes.TryGetValue(normalizedEmail, out var code))
            {
                throw new KeyNotFoundException("Код не найден");
            }
            if (code.DateAt < DateTime.UtcNow)
            {
                StorageHistory.Codes.Remove(normalizedEmail);
                throw new KeyNotFoundException("Срок действия кода истек. Запросите новый код");
            }
            if (code.Code != data.Code)
            {
                throw new KeyNotFoundException("Неправильный код");
            }
            StorageHistory.Codes.Remove(normalizedEmail);
            StorageHistory.ConfirmedEmails.Add(normalizedEmail);

            return new ReturnDto
            {
                message = "Ваша почта подтверждена успешно",
                token = "123456"
            };
        }

        public bool IsEmailConfirmed(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            return StorageHistory.ConfirmedEmails.Contains(NormalizeEmail(email));
        }

        public void ConsumeConfirmedEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }
            StorageHistory.ConfirmedEmails.Remove(NormalizeEmail(email));
        }


        public class ReturnDto
        {
            public string? message { get; set; }
            public string? token { get; set; }
        }
    }
}
