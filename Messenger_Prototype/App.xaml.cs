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

            MessengerWindow main1 = new MessengerWindow();
            main1.Title = "Чат - окно 1";
            main1.Show();

            MessengerWindow main2 = new MessengerWindow();
            main2.Title = "Чат - окно 2";
            main2.Show();
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
