// Decompiled with JetBrains decompiler
// Type: SimphonyPortraitMode.SafeNativeMethods
// Assembly: SimphonyPortraitMode, Version=1.0.7537.14868, Culture=neutral, PublicKeyToken=null
// MVID: 2F330B84-E650-4F53-871B-4CB699A44B8C
// Assembly location: K:\DOCS\Téléchargements\SimphonyPortraitMode.dll

using System.Runtime.InteropServices;

#nullable disable
namespace SimphonyPortraitMode
{
  internal static class SafeNativeMethods
  {
    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int DMDO_DEFAULT = 0;
    public const int DMDO_90 = 1;
    public const int DMDO_180 = 2;
    public const int DMDO_270 = 3;
    public const uint FORMAT_MESSAGE_FROM_HMODULE = 2048;
    public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;
    public const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    public const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    public const uint FORMAT_MESSAGE_FLAGS = 4864;

    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumDisplaySettings(
      [MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName,
      [MarshalAs(UnmanagedType.U4)] int iModeNum,
      [In, Out] ref SafeNativeMethods.DEVMODE lpDevMode);

    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
    [return: MarshalAs(UnmanagedType.I4)]
    public static extern int ChangeDisplaySettings(
      [In, Out] ref SafeNativeMethods.DEVMODE lpDevMode,
      [MarshalAs(UnmanagedType.U4)] uint dwflags);

    [DllImport("kernel32.dll", SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern uint FormatMessage(
      [MarshalAs(UnmanagedType.U4)] uint dwFlags,
      [MarshalAs(UnmanagedType.U4)] uint lpSource,
      [MarshalAs(UnmanagedType.U4)] uint dwMessageId,
      [MarshalAs(UnmanagedType.U4)] uint dwLanguageId,
      [MarshalAs(UnmanagedType.LPTStr)] out string lpBuffer,
      [MarshalAs(UnmanagedType.U4)] uint nSize,
      [MarshalAs(UnmanagedType.U4)] uint Arguments);

    public struct DEVMODE
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string dmDeviceName;
      [MarshalAs(UnmanagedType.U2)]
      public ushort dmSpecVersion;
      [MarshalAs(UnmanagedType.U2)]
      public ushort dmDriverVersion;
      [MarshalAs(UnmanagedType.U2)]
      public ushort dmSize;
      [MarshalAs(UnmanagedType.U2)]
      public ushort dmDriverExtra;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmFields;
      public SafeNativeMethods.POINTL dmPosition;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmDisplayOrientation;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmDisplayFixedOutput;
      [MarshalAs(UnmanagedType.I2)]
      public short dmColor;
      [MarshalAs(UnmanagedType.I2)]
      public short dmDuplex;
      [MarshalAs(UnmanagedType.I2)]
      public short dmYResolution;
      [MarshalAs(UnmanagedType.I2)]
      public short dmTTOption;
      [MarshalAs(UnmanagedType.I2)]
      public short dmCollate;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string dmFormName;
      [MarshalAs(UnmanagedType.U2)]
      public ushort dmLogPixels;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmBitsPerPel;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmPelsWidth;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmPelsHeight;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmDisplayFlags;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmDisplayFrequency;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmICMMethod;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmICMIntent;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmMediaType;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmDitherType;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmReserved1;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmReserved2;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmPanningWidth;
      [MarshalAs(UnmanagedType.U4)]
      public uint dmPanningHeight;

      public void Initialize()
      {
        this.dmDeviceName = new string(new char[32]);
        this.dmFormName = new string(new char[32]);
        this.dmSize = (ushort) Marshal.SizeOf<SafeNativeMethods.DEVMODE>(this);
      }
    }

    public struct POINTL
    {
      public int x;
      public int y;
    }
  }
}
