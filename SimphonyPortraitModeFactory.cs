#nullable disable

using Micros.PosCore.Extensibility;
using System;

namespace SimphonyPortraitMode
{
  internal class SimphonyPortraitModeFactory : IExtensibilityAssemblyFactory
  {
    private static SimphonyPortraitMode.SimphonyPortraitMode singleton;

    public ExtensibilityAssemblyBase Create(IExecutionContext context)
    {
      try
      {
        if (SimphonyPortraitModeFactory.singleton == null)
          SimphonyPortraitModeFactory.singleton = new SimphonyPortraitMode.SimphonyPortraitMode(context);
        return (ExtensibilityAssemblyBase) SimphonyPortraitModeFactory.singleton;
      }
      catch (Exception ex)
      {
        ExtensibilityAppLogger.CurrentInstance.LogAlways("SimphonyPortraitMode: " + ex.Message);
        return (ExtensibilityAssemblyBase) null;
      }
    }

    public static SimphonyPortraitMode.SimphonyPortraitMode GetInstance()
    {
      return SimphonyPortraitModeFactory.singleton;
    }

    public void Destroy(ExtensibilityAssemblyBase app) => app.Destroy();
  }
}
