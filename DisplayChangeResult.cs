#nullable disable

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
