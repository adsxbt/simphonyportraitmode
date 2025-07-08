// Decompiled with JetBrains decompiler
// Type: SimphonyPortraitMode.DisplaySettings
// Assembly: SimphonyPortraitMode, Version=1.0.7537.14868, Culture=neutral, PublicKeyToken=null
// MVID: 2F330B84-E650-4F53-871B-4CB699A44B8C
// Assembly location: K:\DOCS\Téléchargements\SimphonyPortraitMode.dll

using System;
using System.Globalization;

namespace SimphonyPortraitMode
{
  internal struct DisplaySettings
  {
    public int Index { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public Orientation Orientation { get; set; }

    public int BitCount { get; set; }

    public int Frequency { get; set; }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} by {1}, {2}, {3} Bit, {4} Hertz", (object) this.Width, (object) this.Height, (object) (int) this.Orientation, (object) this.BitCount, (object) this.Frequency);
    }
  }
}
