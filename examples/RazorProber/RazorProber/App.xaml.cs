using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Magellan.Framework;
using RazorProber.Controllers;
using Magellan;
using Magellan.Routing;
using Magellan.Transitionals;
using Magellan.Transitionals.Transitions;

namespace RazorProber
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			DispatcherUnhandledException += (x, y) => MessageBox.Show(y.Exception.Message);
			
			NavigationTransitions.Table.Add("Backward", "Forward", () => new SlideTransition(SlideDirection.Back));
			NavigationTransitions.Table.Add("Forward", "Backward", () => new SlideTransition(SlideDirection.Forward));

			var controllers = new ControllerFactory();
			controllers.Register("Home", () => new HomeController());

			var routes = new ControllerRouteCatalog(controllers);
			routes.MapRoute("{controller}/{action}");

			var navigation = new NavigatorFactory(routes);
			var mainWindow = new MainWindow();
			var navigator = navigation.CreateNavigator(mainWindow.MainFrame);
			mainWindow.Show();

			navigator.NavigateWithTransition("Home", "Index", "Forward");
		}
	}
}
