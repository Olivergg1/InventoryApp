class GlobalServiceProvider
{
  private static IServiceProvider? _instance;

  public static void Initialize(IServiceProvider serviceProvider)
  {
    _instance = serviceProvider;
  }

  public static IServiceProvider GetInstance() => _instance ?? throw new InvalidOperationException("Service provider not initialized.");
  
}