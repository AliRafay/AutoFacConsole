using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

public interface IMobileServive
{
    void Execute();
}
public class SMSService : IMobileServive
{
    public void Execute()
    {
        Console.WriteLine("SMS service executing.");
    }
}

public interface IMailService
{
    void Execute();
}

public class EmailService : IMailService
{
    public void Execute()
    {
        Console.WriteLine("Email service Executing.");
    }
}

public class NotificationSender
{
    public IMobileServive ObjMobileSerivce = null;
    public IMailService ObjMailService = null;

    //injection through constructor  
    public NotificationSender(IMobileServive tmpService, IMailService ms)
    {
        ObjMobileSerivce = tmpService;
        ObjMailService = ms;
    }
    public void SendNotification()
    {
        ObjMobileSerivce.Execute();
        ObjMailService.Execute();
    }
}

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SMSService>().As<IMobileServive>();
            builder.RegisterType<EmailService>().As<IMailService>();
            builder.RegisterType<NotificationSender>();
            var container = builder.Build();

            container.Resolve<NotificationSender>().SendNotification();
        }
    }
}