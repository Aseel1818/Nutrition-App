using System;
using System.Net.Mail;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using oauth.Services;
namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterEmailPage : ContentPage
    {
        public EnterEmailPage()
        {
            InitializeComponent();
        }
        public string UserEmail { get; set; }

        // Method for generate the codes of reset password ( code contains 7 digits )
        public int GenerateRandomNo()
        {
            int _min = 1000000;   // minimum value of range 
            int _max = 9999999;   // maximum  value of range 
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        } 


        /// programming the button of save the  entered email and send the code in mail messaeg to the user

        private async void ButtonSaveEmail_Clicked(object sender, EventArgs e)
        {
            // the value of entered email
            string email = TxtEmail.Text;  
            var mongoService = new ForgetPasswordMongoService();

            // check if the email feild is empty
             

             if (string.IsNullOrEmpty(email))
              {
                  await DisplayAlert("تحذير", "الرجاء إدخال البريد الإلكتروني  ", "موافق");

              }
          
           if (mongoService.IsExist(email) ==false)
            {
                await DisplayAlert("تحذير", " البريد الإلكتروني المدخل غير مسجل في التطبيق ", "موافق");

            }

            else
            {

                // We used smtp and essintial in xamarin forms to send email contains code for reset password to user
               

                try
                {

                    MailMessage mail = new MailMessage();

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("aseelhamayel16@gmail.com"); /// the email we use to send email messages for users

                    mail.To.Add(TxtEmail.Text);

                    mail.Subject = "رمز إعادة تعيين كلمة المرور الخاصة بك "; // subject of email

                    mail.Body = GenerateRandomNo().ToString();  // email body and it contains the code of reset password

                    //SecureStorage.SetAsync("code", mail.Body);  // store the code of reset password in secure storage to use it while the programmin and because it is sensitive data

                    var tokenCode= SecureStorage.SetAsync("code", mail.Body);










                 await  SecureStorage.SetAsync("Email", TxtEmail.Text); // store user entered email

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("aseelhamayel16@gmail.com", "ahsgxftqijelsgpg"); // here the email and the password of application to make this email address send email messages for users 
                     

                    SmtpServer.Send(mail);  // to send the email 
                   
                    await DisplayAlert("تحذير", " تم إرسال الرمز اإلى البريد الإلكتروني الخاص بك بنجاح ", "موافق");

                    // go to page of enter reset password code 

                    App.Current.MainPage = new EnterCodePage();
                }
                // if can't send email
                catch (Exception ex)
                {
                 await   DisplayAlert("Faild", ex.Message, "OK");
                }
            }

        }


        public bool CodeTimer()
        {
            var second = TimeSpan.FromMinutes(1);

            bool boolValue = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                if (second <TimeSpan.MaxValue)
                {
                    SecureStorage.Remove("code");
                    boolValue=  true ;
                    return boolValue;
                }
                else
                {
                    boolValue= false;
                    return boolValue;
                }
            });
            return boolValue;

        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();

        }
    }
    }