namespace AsyncCorrelationLogging.RestApi
{
  using System.Net.Http.Headers;
  using System.Reflection;
  using System.Web.Http;
  using Autofac;
  using Autofac.Integration.WebApi;

  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);

      var builder = new ContainerBuilder();

      // Get your HttpConfiguration.
      var config = GlobalConfiguration.Configuration;

      // Register your Web API controllers.
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      // OPTIONAL: Register the Autofac filter provider.
      builder.RegisterWebApiFilterProvider(config);

      // OPTIONAL: Register the Autofac model binder provider.
      builder.RegisterWebApiModelBinderProvider();

      builder.RegisterType<AsyncCorrelationLogging.LogService.Logger>();
      builder.RegisterType<AsyncCorrelationLogging.RestApi.Domain.TodoService>();
      builder.RegisterType<AsyncCorrelationLogging.RestApi.Domain.TodoJsonRepository>();

      // Set the dependency resolver to be Autofac.
      var container = builder.Build();
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

      config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
    }
  }
}
