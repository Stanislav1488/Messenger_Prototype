using Messenger_Prototype.Data;
using Messenger_Prototype.Model.Entities;
using Messenger_Prototype.Services;
using Messenger_Prototype.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger_Prototype
{
    public partial class App : Application
    {
        private ServerHost serverHost;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            serverHost = new ServerHost();
            await serverHost.StartAsync();

            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.Add(new UserEntity { Login = "admin", Password = "123", Name = "Админ", Status = "offline" });
                db.Users.Add(new UserEntity { Login = "user", Password = "123", Name = "Стас", Status = "offline" });
                db.Users.Add(new UserEntity { Login = "lina", Password = "123", Name = "Лина", Status = "offline" });
                db.Users.Add(new UserEntity { Login = "egor", Password = "123", Name = "Егор", Status = "offline" });
                db.SaveChanges();
            }

            LoginWindow login1 = new LoginWindow();
            login1.Title = "Log in - окно 1";
            login1.Show();

            LoginWindow login2 = new LoginWindow();
            login2.Title = "Log in - окно 2";
            login2.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if(serverHost != null)
            {
                await serverHost.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
