﻿using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace AngularApp.DependencyInjection
{
	public class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			
			// Register dependencies in controllers
			builder.RegisterControllers(typeof (MvcApplication).Assembly);

			builder.RegisterModule(new AutofacWebTypesModule());

			// Register dependencies in filter attributes
			builder.RegisterFilterProvider();

			// Register dependencies in custom views
			builder.RegisterSource(new ViewRegistrationSource());

			// Register our Data dependencies
			builder.RegisterModule(new DataModule());

			var container = builder.Build();

			// Set MVC DI resolver to use our Autofac container
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}