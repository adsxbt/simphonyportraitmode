#nullable disable

using Micros.Ops.Extensibility;
using Micros.PosCore.Extensibility;
using Micros.PosCore.Extensibility.Ops;
using SimphonyPortraitMode.Properties;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace SimphonyPortraitMode
{
  public class SimphonyPortraitMode : OpsExtensibilityApplication
  {
    private Button rotationButton;
    private Form loginForm;
    private Orientation currentOrientation;
    private bool manualRotationEnabled = true;

    public void CheckDisplayOrientation()
    {
      try
      {
        switch (this.DataStore.ReadExtensionDataValue("WORKSTATION", "KioskRotation", (long) this.OpsContext.WorkstationID))
        {
          case "1":
            this.CheckDisplayOrientationSub(Orientation.Clockwise90);
            break;
          case "2":
            this.CheckDisplayOrientationSub(Orientation.Clockwise180);
            break;
          case "3":
            this.CheckDisplayOrientationSub(Orientation.Clockwise270);
            break;
          default:
            this.CheckDisplayOrientationSub(Orientation.Default);
            break;
        }
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof (CheckDisplayOrientation));
      }
    }

    private void CheckDisplayOrientationSub(Orientation orientation)
    {
      DisplaySettings currentSettings = DisplayManager.GetCurrentSettings();
      if (currentSettings.Orientation == orientation)
      {
        currentOrientation = orientation;
        return;
      }
      
      ApplyOrientation(orientation);
    }

    private void ApplyOrientation(Orientation orientation)
    {
      try
      {
        DisplaySettings currentSettings = DisplayManager.GetCurrentSettings();
        DisplaySettings newSettings = currentSettings with
        {
          Orientation = orientation
        };
        DisplayManager.SetDisplaySettings(newSettings);
        
        // Mettre à jour l'orientation actuelle
        currentOrientation = orientation;
        
        // Mettre à jour le texte du bouton
        UpdateRotationButtonText();
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof(ApplyOrientation));
        MessageBox.Show(string.Format(Resources.UI_Error_RotationFailed, ex.Message), Resources.UI_Title_RotationError, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CreateRotationButton()
    {
      try
      {
        // Rechercher la fenêtre de connexion Simphony
        foreach (Form form in Application.OpenForms)
        {
          if (form.Name.Contains("Login") || form.Name.Contains("Signon") || form.Text.Contains("Simphony"))
          {
            loginForm = form;
            break;
          }
        }

        if (loginForm != null)
        {
          // Créer le bouton de rotation
          rotationButton = new Button
          {
            Text = GetOrientationButtonText(),
            Size = new Size(120, 40),
            Location = new Point(loginForm.Width - 140, 20),
            BackColor = Color.LightBlue,
            ForeColor = Color.Black,
            Font = new Font("Arial", 9, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
          };

          rotationButton.FlatAppearance.BorderColor = Color.DarkBlue;
          rotationButton.FlatAppearance.BorderSize = 2;
          rotationButton.Click += RotationButton_Click;

          // Ajouter le bouton à la fenêtre de connexion
          loginForm.Controls.Add(rotationButton);
          rotationButton.BringToFront();

          // S'assurer que le bouton reste visible lors du redimensionnement
          loginForm.Resize += (sender, e) => {
            if (rotationButton != null)
            {
              rotationButton.Location = new Point(loginForm.Width - 140, 20);
            }
          };
        }
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof(CreateRotationButton));
      }
    }

    private string GetOrientationButtonText()
    {
      switch (currentOrientation)
      {
        case Orientation.Default:
          return Resources.UI_RotationButton_Landscape;
        case Orientation.Clockwise90:
          return Resources.UI_RotationButton_Portrait;
        case Orientation.Clockwise180:
          return Resources.UI_RotationButton_Inverted;
        case Orientation.Clockwise270:
          return Resources.UI_RotationButton_Portrait270;
        default:
          return Resources.UI_RotationButton_Portrait;
      }
    }

    private void UpdateRotationButtonText()
    {
      if (rotationButton != null)
      {
        rotationButton.Text = GetOrientationButtonText();
      }
    }

    private void RotationButton_Click(object sender, EventArgs e)
    {
      try
      {
        ShowOrientationSelectionDialog();
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof(RotationButton_Click));
      }
    }

    private void ShowOrientationSelectionDialog()
    {
      Form orientationForm = new Form
      {
        Text = Resources.UI_OrientationDialog_Title,
        Size = new Size(350, 280),
        StartPosition = FormStartPosition.CenterScreen,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        MaximizeBox = false,
        MinimizeBox = false,
        BackColor = Color.White
      };

      Label titleLabel = new Label
      {
        Text = Resources.UI_OrientationDialog_Subtitle,
        Location = new Point(20, 20),
        Size = new Size(300, 30),
        Font = new Font("Arial", 10, FontStyle.Bold),
        ForeColor = Color.DarkBlue
      };

      Button landscapeBtn = CreateOrientationButton(Resources.UI_OrientationButton_Landscape, new Point(20, 60), Orientation.Default);
      Button portraitBtn = CreateOrientationButton(Resources.UI_OrientationButton_Portrait, new Point(180, 60), Orientation.Clockwise90);
      Button invertedBtn = CreateOrientationButton(Resources.UI_OrientationButton_Inverted, new Point(20, 120), Orientation.Clockwise180);
      Button portrait270Btn = CreateOrientationButton(Resources.UI_OrientationButton_Portrait270, new Point(180, 120), Orientation.Clockwise270);

      Button cancelBtn = new Button
      {
        Text = Resources.UI_Button_Cancel,
        Location = new Point(140, 180),
        Size = new Size(80, 30),
        DialogResult = DialogResult.Cancel
      };

      orientationForm.Controls.AddRange(new Control[] { 
        titleLabel, landscapeBtn, portraitBtn, invertedBtn, portrait270Btn, cancelBtn 
      });

      orientationForm.ShowDialog();
    }

    private Button CreateOrientationButton(string text, Point location, Orientation orientation)
    {
      Button btn = new Button
      {
        Text = text,
        Location = location,
        Size = new Size(140, 40),
        BackColor = currentOrientation == orientation ? Color.LightGreen : Color.LightGray,
        ForeColor = Color.Black,
        Font = new Font("Arial", 8, FontStyle.Regular),
        FlatStyle = FlatStyle.Flat
      };

      btn.Click += (sender, e) => {
        currentOrientation = orientation;
        ApplyOrientation(orientation);
        
        // Sauvegarder le choix de l'utilisateur
        SaveOrientationChoice(orientation);
        
        ((Button)sender).FindForm().Close();
        MessageBox.Show(Resources.UI_Msg_OrientationApplied, Resources.UI_Title_ScreenRotation, MessageBoxButtons.OK, MessageBoxIcon.Information);
      };

      return btn;
    }

    private void SaveOrientationChoice(Orientation orientation)
    {
      try
      {
        string orientationValue = ((int)orientation).ToString();
        this.DataStore.WriteExtensionDataValue("WORKSTATION", "KioskRotation", (long)this.OpsContext.WorkstationID, orientationValue);
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof(SaveOrientationChoice));
      }
    }

    public void ToggleOrientation()
    {
      // Rotation séquentielle : Paysage -> Portrait -> Inversé -> Portrait 270° -> Paysage
      switch (currentOrientation)
      {
        case Orientation.Default:
          currentOrientation = Orientation.Clockwise90;
          break;
        case Orientation.Clockwise90:
          currentOrientation = Orientation.Clockwise180;
          break;
        case Orientation.Clockwise180:
          currentOrientation = Orientation.Clockwise270;
          break;
        case Orientation.Clockwise270:
        default:
          currentOrientation = Orientation.Default;
          break;
      }
      
      ApplyOrientation(currentOrientation);
      SaveOrientationChoice(currentOrientation);
    }

    public SimphonyPortraitMode(IExecutionContext context)
      : base(context)
    {
      try
      {
        currentOrientation = DisplayManager.GetCurrentSettings().Orientation;
      }
      catch (Exception ex)
      {
        currentOrientation = Orientation.Default;
        this.OpsContext.LogException(ex, "SimphonyPortraitMode Constructor");
      }
      
      this.OpsInitEvent += new OpsExtensibilityApplication.OpsInitEventDelegate(this.SimphonyPortraitMode_OpsInitEvent);
      this.OpsSignInEvent += new OpsExtensibilityApplication.OpsSignInEventDelegate(this.SimphonyPortraitMode_OpsSignInEvent);
      this.OpsExitEvent += new OpsExtensibilityApplication.OpsExitEventDelegate(this.SimphonyPortraitMode_OpsExitEvent);
    }

    private EventProcessingInstruction SimphonyPortraitMode_OpsInitEvent(
      object sender,
      OpsInitEventArgs args)
    {
      try
      {
        if (Convert.ToBoolean(this.DataStore.ReadExtensionDataValue("WORKSTATION", "IsKiosk", (long) this.OpsContext.WorkstationID)))
        {
          this.CheckDisplayOrientation();
          
          // Créer le bouton de rotation sur la page d'accueil avec un délai
          System.Threading.Timer timer = new System.Threading.Timer((state) =>
          {
            this.Invoke(() => CreateRotationButton());
          }, null, 2000, System.Threading.Timeout.Infinite);
        }
        else
        {
          base.Destroy();
        }
      }
      catch (Exception ex)
      {
        this.OpsContext.LogException(ex, nameof(SimphonyPortraitMode_OpsInitEvent));
      }
      
      return (EventProcessingInstruction) 0;
    }

    private EventProcessingInstruction SimphonyPortraitMode_OpsSignInEvent(
      object sender,
      OpsSignInPreviewEventArgs args)
    {
      this.CheckDisplayOrientation();
      return (EventProcessingInstruction) 0;
    }

    private EventProcessingInstruction SimphonyPortraitMode_OpsExitEvent(
      object sender,
      OpsExitEventArgs args)
    {
      base.Destroy();
      return (EventProcessingInstruction) 0;
    }

    public override void Destroy()
    {
      try
      {
        // Nettoyer le bouton de rotation
        if (rotationButton != null)
        {
          rotationButton.Click -= RotationButton_Click;
          if (loginForm != null && loginForm.Controls.Contains(rotationButton))
          {
            loginForm.Controls.Remove(rotationButton);
          }
          rotationButton.Dispose();
          rotationButton = null;
        }

        this.OpsInitEvent -= new OpsExtensibilityApplication.OpsInitEventDelegate(this.SimphonyPortraitMode_OpsInitEvent);
        this.OpsSignInEvent -= new OpsExtensibilityApplication.OpsSignInEventDelegate(this.SimphonyPortraitMode_OpsSignInEvent);
        this.OpsExitEvent -= new OpsExtensibilityApplication.OpsExitEventDelegate(this.SimphonyPortraitMode_OpsExitEvent);
      }
      catch (Exception ex)
      {
        // Log l'erreur mais continuer le processus de destruction
        if (this.OpsContext != null)
        {
          this.OpsContext.LogException(ex, nameof(Destroy));
        }
      }
      finally
      {
        base.Destroy();
      }
    }
  }
}
