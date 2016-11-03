Imports System.Management.Automation

Namespace IcsManagerLibrary
    <Cmdlet(VerbsLifecycle.Disable, "ICS")>
    Public Class Disable_ICS
		Inherits PSCmdlet

		Protected Overrides Sub ProcessRecord()
			IcsManager.ShareConnection(Nothing, Nothing)
		End Sub
	End Class
End Namespace
