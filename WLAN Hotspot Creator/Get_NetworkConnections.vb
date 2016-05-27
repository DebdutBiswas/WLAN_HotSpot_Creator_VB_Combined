Imports System.Management.Automation

Namespace IcsManagerLibrary
	<Cmdlet(VerbsCommon.Get, "NetworkConnections")> _
	Public Class Get_NetworkConnections
		Inherits PSCmdlet

		Protected Overrides Sub ProcessRecord()
			For Each nic In IcsManager.GetIPv4EthernetAndWirelessInterfaces()
				Dim connection = IcsManager.GetConnectionById(nic.Id)
				Dim properties = IcsManager.GetProperties(connection)
				Dim configuration = IcsManager.GetConfiguration(connection)
				Dim record = New With {Key .Name = nic.Name, Key .GUID = nic.Id, Key .MAC = nic.GetPhysicalAddress(), Key .Description = nic.Description, Key .SharingEnabled = configuration.SharingEnabled, Key .NetworkAdapter = nic, Key .Configuration = configuration, Key .Properties = properties}
				WriteObject(record)
			Next nic
		End Sub
	End Class
End Namespace
