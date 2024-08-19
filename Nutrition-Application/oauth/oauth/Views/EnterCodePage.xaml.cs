using oauth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oauth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterCodePage : ContentPage
    {
        public string Code { get; set; }    // public variable for store Code value
        public string Email { get; set; }
        public EnterCodePage()
        {
           
            InitializeComponent();

        }
        public int GenerateRandomNo()
        {
            int _min = 1000000;   // minimum value of range 
            int _max = 9999999;   // maximum  value of range 
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        private async void ButtonSaveCode_Clicked(object sender, EventArgs e)
        {

            
        
         string code = TxtCode.Text.ToString(); ////// store the value of entry code in variable


            /// check if the value of the code is empty when click on the buttton

            if (string.IsNullOrEmpty(code))
            {
                await DisplayAlert("تحذير", " الرجاء إدخال الرمز  في المكان المخصص ", "موافق");
            }

            else
            {
                ////// get the value of code which genearted in the code of application ( code in the email message )

                var resetPasswordCode = await SecureStorage.GetAsync("code");

                // store Code value in public variable

                Code = resetPasswordCode;     

                
                
                ////// check if the entry value and the real value of code are the same
                if (code == Code)
                {

                    var myUser = new PasswordPropertyModel();

                    //var resetPasswordPage = new ResetPasswordPage);

                    // if the tow values are the same go to reset password page
                   App.Current.MainPage = new ResetPasswordPage(myUser, true);


                }

                
                // if the enter code is not correct display alert to user 
                else
                {
                    await DisplayAlert("تحذير", " الرمز المدخل غير صحيح ", "موافق");

                }

            }

        }

        private async void ButtonresendCode_Clicked(object sender, EventArgs e)
        {

            try
            {

                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("aseelhamayel16@gmail.com"); /// the email we use to send email messages for users

                var email = await SecureStorage.GetAsync("Email");
                Email = email;

                mail.To.Add(Email);

                mail.Subject = "رمز إعادة تعيين كلمة المرور الخاصة بك "; // subject of email

                mail.Body = GenerateRandomNo().ToString();  // email body and it contains the code of reset password

               await    SecureStorage.SetAsync("code", mail.Body);  // store the code of reset password in secure storage to use it while the programmin and because it is sensitive data


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
              await  DisplayAlert("Faild", ex.Message, "OK");
            }



        }

        private void BackBtn_Tapped(object sender, EventArgs e)
        {

            App.Current.MainPage = new EnterEmailPage();


        }
    }
}