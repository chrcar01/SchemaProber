using Magellan.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using RazorProber.Views.Home;

namespace RazorProber.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return Page();
		}
		public ActionResult GenCrud()
		{
			return Page(new GenCrudViewModel());
		}
		public ActionResult Columns()
		{
			return Page(new ColumnsViewModel());
		}
	}
}
