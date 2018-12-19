namespace AsyncCorrelationLogging
{
  using System.Windows;
  using Autofac;
  using Prism.Autofac;

  public sealed class Bootstrapper : AutofacBootstrapper
  {
    private IContainer container;

    /// <summary>Creates the shell or main window of the application.</summary>
    /// <returns>The shell of the application.</returns>
    /// <remarks>
    /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
    /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
    /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
    /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
    /// attached property from XAML.
    /// </remarks>
    protected override DependencyObject CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    /// <summary>Initializes the shell.</summary>
    protected override void InitializeShell()
    {
      base.InitializeShell();

      Application.Current.MainWindow = (Window)Shell;
      Application.Current.MainWindow?.Show();
    }

    /// <summary>
    /// Configures the <see cref="T:Autofac.ContainerBuilder" />.
    /// May be overwritten in a derived class to add specific type mappings required by the application.
    /// </summary>
    protected override void ConfigureContainerBuilder(ContainerBuilder builder)
    {
      base.ConfigureContainerBuilder(builder);
    }

    /// <summary>
    /// Creates the Autofac <see cref="T:Autofac.IContainer" /> that will be used as the default container.
    /// </summary>
    /// <returns>A new instance of <see cref="T:Autofac.IContainer" />.</returns>
    protected override IContainer CreateContainer(ContainerBuilder containerBuilder)
    {
      container = base.CreateContainer(containerBuilder);
      return container;
    }
  }
}
