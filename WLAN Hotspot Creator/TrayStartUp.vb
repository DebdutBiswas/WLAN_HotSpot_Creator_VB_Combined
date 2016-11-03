Imports System.Windows.Forms.Control

Public Class TrayStartUp
    Inherits ApplicationContext

    Public Shared WithEvents AppTray As New NotifyIcon
    Public Shared WithEvents TrayMenuStrip As New ContextMenuStrip
    Public Shared WithEvents StartTrayMenuItm As New ToolStripMenuItem("&Start Hotspot")
    Public Shared WithEvents StopTrayMenuItm As New ToolStripMenuItem("St&op Hotspot")
    Public Shared WithEvents OpenAppTrayMenuItm As New ToolStripMenuItem("Open &Application")
    Public Shared WithEvents CloseAppTrayMenuItm As New ToolStripMenuItem("&Close Application")
    Public Shared WithEvents ExitTrayMenuItm As New ToolStripMenuItem("&Exit")
    Public Shared MainWindow As New MainDialog

    Private Sub TrayMenuStripInitialize()

        TrayMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {StartTrayMenuItm, StopTrayMenuItm, OpenAppTrayMenuItm, CloseAppTrayMenuItm, ExitTrayMenuItm})
        TrayMenuStrip.ShowImageMargin = False
        TrayMenuStrip.Size = New System.Drawing.Size(143, 114)
        CloseAppTrayMenuItm.Visible = False

    End Sub

    Private Sub AppTrayInitialize()

        TrayMenuStripInitialize()
        AppTray.Text = "WLAN Hotspot Creator"
        AppTray.ContextMenuStrip = TrayMenuStrip
        AppTray.Visible = True

    End Sub

    Private Shared Sub TrayAppStartedStatus()

        AppTray.Icon = My.Resources.connection_icon_white
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Application started..."
        AppTray.ShowBalloonTip(500)

    End Sub

    Public Sub New()

        CheckForIllegalCrossThreadCalls = False

        AppTrayInitialize()
        TrayAppStartedStatus()

    End Sub

    Private Shared Sub AppTray_Click(sender As Object, e As EventArgs) Handles AppTray.Click

        If MainWindow.Visible = True Then
            OpenAppTrayMenuItm.Visible = False
            CloseAppTrayMenuItm.Visible = True
        ElseIf MainWindow.Visible = False Then
            CloseAppTrayMenuItm.Visible = False
            OpenAppTrayMenuItm.Visible = True
        End If

    End Sub

    Private Shared Sub OpenAppTrayMenuItm_Click(sender As Object, e As EventArgs) Handles OpenAppTrayMenuItm.Click

        If MainWindow.Visible = False Then
            OpenAppTrayMenuItm.Visible = False
            CloseAppTrayMenuItm.Visible = True
            MainWindow.ShowDialog()
        End If

    End Sub

    Private Shared Sub CloseAppTrayMenuItm_Click(sender As Object, e As EventArgs) Handles CloseAppTrayMenuItm.Click

        If MainWindow.Visible = True Then
            CloseAppTrayMenuItm.Visible = False
            OpenAppTrayMenuItm.Visible = True
            MainWindow.Visible = False
        End If

    End Sub

    Private Shared Sub ExitTrayMenuItm_Click(sender As Object, e As EventArgs) Handles ExitTrayMenuItm.Click

        Dim conf As DialogResult
        conf = MessageBox.Show("Do you want to exit application?", "WLAN Hotspot Creator", MessageBoxButtons.YesNo)
        If conf = DialogResult.Yes Then
            AppTray.Visible = False
            'MainDialog.Dispose()
            Application.Exit()
        End If

    End Sub
    '--AppTray Shared Status Functions-----------------------------------------------------------
    Public Shared Sub TrayStartingStatus()

        AppTray.Icon = My.Resources.connection_icon_blue
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Creating Hotspot..."
        AppTray.ShowBalloonTip(500)

    End Sub

    Public Shared Sub TrayStartedStatus()

        AppTray.Icon = My.Resources.connection_icon_green
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Hotspot Started..."
        AppTray.ShowBalloonTip(500)

    End Sub

    Public Shared Sub TrayStoppingStatus()

        AppTray.Icon = My.Resources.connection_icon_yellow
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Stopping Hotspot..."
        AppTray.ShowBalloonTip(500)

    End Sub

    Public Shared Sub TrayStoppedStatus()

        AppTray.Icon = My.Resources.connection_icon_red
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Hotspot Stopped..."
        AppTray.ShowBalloonTip(500)

    End Sub

    Public Shared Sub TrayErrorStatus()

        AppTray.Icon = My.Resources.connection_icon_red
        AppTray.BalloonTipIcon = ToolTipIcon.Info
        AppTray.BalloonTipTitle = "WiFi Hotspot Status"
        AppTray.BalloonTipText = "Hotspot couldn't be started..."
        AppTray.ShowBalloonTip(500)

    End Sub
    '--------------------------------------------------------------------------------------------
End Class
