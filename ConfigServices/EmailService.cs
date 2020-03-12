using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace noname.ConfigServices
{
    public class EmailService
    {
        public async Task UserActivationMail(User user)
        {
            try
            {
                var html = File.ReadAllText("./mail.html");
                html = html.Replace("@title", "Hesap Aktivasyonu");
                html = html.Replace("@company", "Merhaba, " + user.Username);
                html = html.Replace("@body", "Hesabınızı aktif etmek için aşağıdaki linki kullanabilirsiniz.");


                html = html.Replace("@link", "https://kampusApp/#/change-password/" + user.ActivationCode);
                html = html.Replace("@id", " ");
                html = html.Replace("@image", "login.png");



                html = html.Replace("@date", DateTime.Now.ToString("dd.MM.yyyy"));
                html = html.Replace("@time", DateTime.Now.ToString("HH:mm"));
                html = html.Replace("@random", new Random().Next().ToString());
                var mail = new MailMessage()
                {
                    From = new MailAddress("f.soysal.93@gmail.com", "kampusApp"),
                    Subject = "Aktivasyon Maili",
                    Body = html,

                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(user.UserContact.Email));
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Port = 587;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;

                    smtpClient.Credentials = new NetworkCredential("f.soysal.93@gmail.com", "Sermerkant");
                    await smtpClient.SendMailAsync(mail);
                }

            }
            catch (System.Exception e)
            {
            }

        }
        public async Task ResetPassword(User user)
        {
            try
            {
                var html = File.ReadAllText("./mail.html");
                html = html.Replace("@title", "Şifre Değişiklik Talebi");
                html = html.Replace("@company", "Merhaba, " + user.Username);
                html = html.Replace("@body", "Şifrenizi değiştirmek için aşağıdaki linki kullanabilirsiniz.");


                html = html.Replace("@link", "https://kampusApp.com/#/change-password/" + user.ActivationCode);
                html = html.Replace("@id", " ");
                html = html.Replace("@image", "login.png");



                html = html.Replace("@date", DateTime.Now.ToString("dd.MM.yyyy"));
                html = html.Replace("@time", DateTime.Now.ToString("HH:mm"));
                html = html.Replace("@random", new Random().Next().ToString());
                var mail = new MailMessage()
                {
                    From = new MailAddress("f.soysal.93@gmail.com", "kampusApp"),
                    Subject = "Şifre Değişiklik Talebi",
                    Body = html,

                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(user.UserContact.Email));
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Port = 587;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;

                    smtpClient.Credentials = new NetworkCredential("f.soysal.93@gmail.com", "Semerkant");
                    await smtpClient.SendMailAsync(mail);
                }

            }
            catch (System.Exception e)
            {
            }

        }

    }
}
