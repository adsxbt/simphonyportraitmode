#nullable disable

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SimphonyPortraitMode.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Resources.resourceMan == null)
          Resources.resourceMan = new ResourceManager("SimphonyPortraitMode.Properties.Resources", typeof (Resources).Assembly);
        return Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Resources.resourceCulture;
      set => Resources.resourceCulture = value;
    }

    internal static string InvalidOperation_Disp_Change_BadDualView
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_BadDualView), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_BadFlags
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_BadFlags), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_BadMode
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_BadMode), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_BadParam
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_BadParam), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_Failed
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_Failed), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_NotUpdated
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_NotUpdated), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_Disp_Change_Restart
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_Disp_Change_Restart), Resources.resourceCulture);
      }
    }

    internal static string InvalidOperation_FatalError
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (InvalidOperation_FatalError), Resources.resourceCulture);
      }
    }

    internal static string Msg_Disp_Change
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (Msg_Disp_Change), Resources.resourceCulture);
      }
    }

    internal static string Msg_Disp_Change_Original
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (Msg_Disp_Change_Original), Resources.resourceCulture);
      }
    }

    internal static string Msg_Disp_Change_Reset
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (Msg_Disp_Change_Reset), Resources.resourceCulture);
      }
    }

    internal static string Msg_Disp_Change_Rotate
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (Msg_Disp_Change_Rotate), Resources.resourceCulture);
      }
    }

    internal static string Msg_Disp_Change_Successful
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (Msg_Disp_Change_Successful), Resources.resourceCulture);
      }
    }

    internal static string UI_RotationButton_Landscape
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_RotationButton_Landscape), Resources.resourceCulture);
      }
    }

    internal static string UI_RotationButton_Portrait
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_RotationButton_Portrait), Resources.resourceCulture);
      }
    }

    internal static string UI_RotationButton_Inverted
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_RotationButton_Inverted), Resources.resourceCulture);
      }
    }

    internal static string UI_RotationButton_Portrait270
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_RotationButton_Portrait270), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationDialog_Title
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationDialog_Title), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationDialog_Subtitle
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationDialog_Subtitle), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationButton_Landscape
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationButton_Landscape), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationButton_Portrait
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationButton_Portrait), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationButton_Inverted
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationButton_Inverted), Resources.resourceCulture);
      }
    }

    internal static string UI_OrientationButton_Portrait270
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_OrientationButton_Portrait270), Resources.resourceCulture);
      }
    }

    internal static string UI_Button_Cancel
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_Button_Cancel), Resources.resourceCulture);
      }
    }

    internal static string UI_Msg_OrientationApplied
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_Msg_OrientationApplied), Resources.resourceCulture);
      }
    }

    internal static string UI_Title_ScreenRotation
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_Title_ScreenRotation), Resources.resourceCulture);
      }
    }

    internal static string UI_Error_RotationFailed
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_Error_RotationFailed), Resources.resourceCulture);
      }
    }

    internal static string UI_Title_RotationError
    {
      get
      {
        return Resources.ResourceManager.GetString(nameof (UI_Title_RotationError), Resources.resourceCulture);
      }
    }
  }
}
