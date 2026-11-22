using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LifeManagementApp.Services;
using LifeManagementApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp;

public partial class App : Application
{
    // DI 容器：全应用的依赖都从这里领
    public static ServiceProvider Services { get; private set; } = null!;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        // —— 注册：接口 → 实现 ——//
        var services = new ServiceCollection();
        services.AddSingleton<IWeatherService, OpenMeteoWeatherService>(); // 单例：HttpClient 共享一份
        services.AddSingleton<ITodoRepository, TodoRepository>();          // 单例：一个数据库连接入口
        services.AddTransient<MainWindow>();                               // 瞬态：每次启动新窗口
        Services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();   // 容器解析 + 构造注入

        base.OnFrameworkInitializationCompleted();
    }
}
