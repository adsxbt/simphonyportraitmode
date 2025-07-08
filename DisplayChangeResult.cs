// Decompiled with JetBrains decompiler
// Type: SimphonyPortraitMode.DisplayChangeResult
// Assembly: SimphonyPortraitMode, Version=1.0.7537.14868, Culture=neutral, PublicKeyToken=null
// MVID: 2F330B84-E650-4F53-871B-4CB699A44B8C
// Assembly location: K:\DOCS\Téléchargements\SimphonyPortraitMode.dll

namespace SimphonyPortraitMode
{
  internal enum DisplayChangeResult
  {
    BadDualView = -6, // 0xFFFFFFFA
    BadParam = -5, // 0xFFFFFFFB
    BadFlags = -4, // 0xFFFFFFFC
    NotUpdated = -3, // 0xFFFFFFFD
    BadMode = -2, // 0xFFFFFFFE
    Failed = -1, // 0xFFFFFFFF
    Successful = 0,
    Restart = 1,
  }
}
