using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LifeManagementApp.Services;
using LifeManagementApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp;

public partial class App : Application
{
    // DI container: all app dependencies are resolved from here
    public static ServiceProvider Services { get; private set; } = null!;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        // Register: interface -> implementation
        var services = new ServiceCollection();
        services.AddSingleton<IWeatherService, OpenMeteoWeatherService>(); // Singleton: one shared HttpClient
        services.AddSingleton<ITodoRepository, TodoRepository>();          // Singleton: one database entry point
        services.AddTransient<MainWindow>();                               // Transient: a fresh window each time
        Services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = Services.GetRequiredService<MainWindow>(); // resolve from the container + constructor injection

        base.OnFrameworkInitializationCompleted();
    }
}
