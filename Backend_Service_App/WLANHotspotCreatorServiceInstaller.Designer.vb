<System.ComponentModel.RunInstaller(True)> Partial Class WLANHotspotCreatorServiceInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.ServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'ServiceProcessInstaller
        '
        Me.ServiceProcessInstaller.Password = Nothing
        Me.ServiceProcessInstaller.Username = Nothing
        '
        'ServiceInstaller
        '
        Me.ServiceInstaller.ServiceName = "WLAN Hotspot Creator Backend Stub"
        '
        'WLANHotspotCreatorServiceInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstaller, Me.ServiceInstaller})

    End Sub

    Friend WithEvents ServiceProcessInstaller As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents ServiceInstaller As ServiceProcess.ServiceInstaller
End Class
