// Decompiled with JetBrains decompiler
// Type: SimphonyPortraitMode.DisplayManager
// Assembly: SimphonyPortraitMode, Version=1.0.7537.14868, Culture=neutral, PublicKeyToken=null
// MVID: 2F330B84-E650-4F53-871B-4CB699A44B8C
// Assembly location: K:\DOCS\Téléchargements\SimphonyPortraitMode.dll

using SimphonyPortraitMode.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SimphonyPortraitMode
{
  internal static class DisplayManager
  {
    public static DisplaySettings GetCurrentSettings()
    {
      return DisplayManager.CreateDisplaySettingsObject(-1, DisplayManager.GetDeviceMode());
    }

    public static void SetDisplaySettings(DisplaySettings set)
    {
      SafeNativeMethods.DEVMODE deviceMode = DisplayManager.GetDeviceMode() with
      {
        dmPelsWidth = (uint) set.Width,
        dmPelsHeight = (uint) set.Height,
        dmDisplayOrientation = (uint) set.Orientation,
        dmBitsPerPel = (uint) set.BitCount,
        dmDisplayFrequency = (uint) set.Frequency
      };
      DisplayChangeResult displayChangeResult = (DisplayChangeResult) SafeNativeMethods.ChangeDisplaySettings(ref deviceMode, 0U);
      string message = (string) null;
      switch (displayChangeResult)
      {
        case DisplayChangeResult.BadDualView:
          message = Resources.InvalidOperation_Disp_Change_BadDualView;
          break;
        case DisplayChangeResult.BadParam:
          message = Resources.InvalidOperation_Disp_Change_BadParam;
          break;
        case DisplayChangeResult.BadFlags:
          message = Resources.InvalidOperation_Disp_Change_BadFlags;
          break;
        case DisplayChangeResult.NotUpdated:
          message = Resources.InvalidOperation_Disp_Change_NotUpdated;
          break;
        case DisplayChangeResult.BadMode:
          message = Resources.InvalidOperation_Disp_Change_BadMode;
          break;
        case DisplayChangeResult.Failed:
          message = Resources.InvalidOperation_Disp_Change_Failed;
          break;
        case DisplayChangeResult.Restart:
          message = Resources.InvalidOperation_Disp_Change_Restart;
          break;
      }
      if (message != null)
        throw new InvalidOperationException(message);
    }

    public static IEnumerator<DisplaySettings> GetModesEnumerator()
    {
      SafeNativeMethods.DEVMODE mode = new SafeNativeMethods.DEVMODE();
      mode.Initialize();
      int idx = 0;
      while (SafeNativeMethods.EnumDisplaySettings((string) null, idx, ref mode))
        yield return DisplayManager.CreateDisplaySettingsObject(idx++, mode);
    }

    public static void RotateScreen(bool clockwise)
    {
      DisplaySettings currentSettings = DisplayManager.GetCurrentSettings();
      int height = currentSettings.Height;
      currentSettings.Height = currentSettings.Width;
      currentSettings.Width = height;
      if (clockwise)
        ++currentSettings.Orientation;
      else
        --currentSettings.Orientation;
      if (currentSettings.Orientation < Orientation.Default)
        currentSettings.Orientation = Orientation.Clockwise270;
      else if (currentSettings.Orientation > Orientation.Clockwise270)
        currentSettings.Orientation = Orientation.Default;
      DisplayManager.SetDisplaySettings(currentSettings);
    }

    private static DisplaySettings CreateDisplaySettingsObject(
      int idx,
      SafeNativeMethods.DEVMODE mode)
    {
      return new DisplaySettings()
      {
        Index = idx,
        Width = (int) mode.dmPelsWidth,
        Height = (int) mode.dmPelsHeight,
        Orientation = (Orientation) mode.dmDisplayOrientation,
        BitCount = (int) mode.dmBitsPerPel,
        Frequency = (int) mode.dmDisplayFrequency
      };
    }

    private static SafeNativeMethods.DEVMODE GetDeviceMode()
    {
      SafeNativeMethods.DEVMODE lpDevMode = new SafeNativeMethods.DEVMODE();
      lpDevMode.Initialize();
      return SafeNativeMethods.EnumDisplaySettings((string) null, -1, ref lpDevMode) ? lpDevMode : throw new InvalidOperationException(DisplayManager.GetLastError());
    }

    private static string GetLastError()
    {
      string lpBuffer;
      return SafeNativeMethods.FormatMessage(4864U, 2048U, (uint) Marshal.GetLastWin32Error(), 0U, out lpBuffer, 0U, 0U) == 0U ? Resources.InvalidOperation_FatalError : lpBuffer;
    }
  }
}
