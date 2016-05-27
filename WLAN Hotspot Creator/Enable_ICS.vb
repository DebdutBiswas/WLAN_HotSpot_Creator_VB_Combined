Imports System.Management.Automation

Namespace IcsManagerLibrary
	<Cmdlet(VerbsLifecycle.Enable, "ICS")> _
	Public Class Enable_ICS
		Inherits PSCmdlet

		<Parameter(HelpMessage := "Connection to share (name or GUID)", Mandatory := True, Position := 0)> _
		Public Shared_connection As String

		<Parameter(HelpMessage := "Home connection (name or GUID)", Mandatory := True, Position := 1)> _
		Public Home_connection As String

		<Parameter(HelpMessage := "Force disabling ICS if already enabled", Mandatory := False)> _
		Public force As Boolean = False

		Protected Overrides Sub ProcessRecord()
			Dim connectionToShare = IcsManager.FindConnectionByIdOrName(Shared_connection)
			If connectionToShare Is Nothing Then
				WriteError(New ErrorRecord(New PSArgumentException("Connection not found"), "", ErrorCategory.InvalidArgument, Shared_connection))
				Return
			End If
			Dim homeConnection = IcsManager.FindConnectionByIdOrName(Home_connection)
			If homeConnection Is Nothing Then
				WriteError(New ErrorRecord(New PSArgumentException("Connection not found"), "", ErrorCategory.InvalidArgument, Home_connection))
				Return
			End If

			Dim currentShare = IcsManager.GetCurrentlySharedConnections()
			If currentShare.Exists Then
                WriteWarning("Internet Connection Sharing is already enabled: " & currentShare.ToString)
                If Not force Then
					WriteError(New ErrorRecord(New PSInvalidOperationException("Please disable existing ICS if you want to enable it for other connections, or set the force flag to true"), "", ErrorCategory.InvalidOperation, Nothing))
					Return
				End If
				Console.WriteLine("Sharing will be disabled first.")
			End If

			IcsManager.ShareConnection(connectionToShare, homeConnection)
		End Sub
	End Class
End Namespace
