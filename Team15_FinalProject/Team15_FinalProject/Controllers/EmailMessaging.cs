﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace Team15_FinalProject.Messaging
{
    public class EmailMessaging
    {
        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {                    //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("katiegray1234567890@gmail.com", "Abc123!!"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message       
            // /n is a new line – this will add some white space after the main body of the message             
            String finalMessage = emailBody + "\n\n Longhorn Bank thanks you for your continued support ";

            //Create an email address object for the sender address      
            MailAddress senderEmail = new MailAddress("katiegray1234567890@gmail.com", "Team 15");

            MailMessage mm = new MailMessage();
            mm.Subject = "Team 15 - " + emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }
    }
}