// Decompiled with JetBrains decompiler
// Type: SimphonyPortraitMode.SimphonyPortraitModeFactory
// Assembly: SimphonyPortraitMode, Version=1.0.7537.14868, Culture=neutral, PublicKeyToken=null
// MVID: 2F330B84-E650-4F53-871B-4CB699A44B8C
// Assembly location: K:\DOCS\Téléchargements\SimphonyPortraitMode.dll

using Micros.PosCore.Extensibility;
using System;

#nullable disable
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
