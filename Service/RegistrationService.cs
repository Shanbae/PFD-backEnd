using LoginAPI.BR;
using LoginAPI.Bson;
using MongoDB.Driver;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data;

namespace LoginAPI.Service
{
    public class RegistrationService
    {
        private readonly IMongoCollection<Registration> _usersCollection;
        private readonly IConfiguration _configuration;


        public RegistrationService(MongoDBService mongoDbService, IConfiguration configuration)
        {
            _usersCollection = mongoDbService.GetCollection<Registration>("Users");
            _configuration = configuration;
        }

        public async Task AddUserAsync(Registration reg)
        {
            string email = reg.Email;
            var filter =  Builders<Registration>.Filter.Eq(u => u.Email, email);

            var listuser =  await _usersCollection.Find(filter).FirstOrDefaultAsync();
            if (listuser != null)
            {
                reg.Valid = false;
            }
            else
            {
                 _usersCollection.InsertOneAsync(reg);
                string senderMail = _configuration["Email:senderMail"].ToString();
                string senderPassword = _configuration["Email:senderPassword"].ToString();
                string emailBody = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
                              <div style='max-width: 600px; margin: auto; background-color: #fff; padding: 30px; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1);'>
                                <h2 style='color: #333;'>Welcome, {reg.Name}!</h2>
                                <p style='font-size: 16px; color: #555;'>Thank you for registering with us. Your account has been created successfully.</p>
                                <p style='font-size: 16px; color: #555;'>
                                  <strong>UserName:</strong> {reg.Name} <br />
                                  <strong>Password:</strong> {reg.Password}
                                </p>
                                <p style='font-size: 16px; color: #555;'>You can log in using these credentials:</p>
    
                                <p style='margin-top: 30px; font-size: 14px; color: #999;'>Please keep this information secure and do not share it with others.</p>
                                <p style='font-size: 14px; color: #999;'>If you need help, feel free to contact us.</p>
                                <p style='font-size: 14px; color: #999;'>- PERSONAL FINANCE- +91-9600891448</p>
                              </div>
                            </div>";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                //smtp.Timeout = 100000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(senderMail, senderPassword);

                MailMessage mail = new MailMessage(senderMail, reg.Email, "FOR UNDER TESTING", emailBody);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                reg.Valid=true;

                try
                {

                    smtp.Send(mail);
                }
                catch (SmtpException e)
                {
                    //textBox1.Text = e.Message;
                }
            }
        }

        public async Task FetchUserasync(string email)
        {
            var filter = Builders<Registration>.Filter.Eq(u => u.Email, email);
            
            var listuser = await _usersCollection.Find(filter).FirstOrDefaultAsync();
           
            if (listuser != null)
            {
                string senderMail = _configuration["Email:senderMail"].ToString();
                string senderPassword = _configuration["Email:senderPassword"].ToString();
                string emailBody = $@"
                            <div style='font-family:Arial, sans-serif; padding:20px; background-color:#f9f9f9;'>
                                <h2 style='color:#333;'>Account Recovery Details</h2>
                                <p>Hi <strong>{listuser.Name}</strong>,</p>
                                <p>You requested to recover your login credentials. Please find your details below:</p>
                                <table style='border-collapse: collapse; margin-top:10px;'>
                                  <tr>
                                    <td style='padding: 8px; border: 1px solid #ccc;'>Username:</td>
                                    <td style='padding: 8px; border: 1px solid #ccc;'><strong>{listuser.Name}</strong></td>
                                  </tr>
                                  <tr>
                                    <td style='padding: 8px; border: 1px solid #ccc;'>Password:</td>
                                    <td style='padding: 8px; border: 1px solid #ccc;'><strong>{listuser.Password}</strong></td>
                                  </tr>
                                </table>
                                <p style='margin-top:20px;'>Please keep your credentials safe and do not share them with anyone.</p>
                                <p>Thanks & regards,<br/>Your App Team</p>
                              </div>";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                //smtp.Timeout = 100000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(senderMail, senderPassword);

                MailMessage mail = new MailMessage(senderMail, email, "FOR UNDER TESTING", emailBody);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;

                try
                {

                    smtp.Send(mail);
                }
                catch (SmtpException e)
                {
                    //textBox1.Text = e.Message;
                }
            }

        }
    }
}
