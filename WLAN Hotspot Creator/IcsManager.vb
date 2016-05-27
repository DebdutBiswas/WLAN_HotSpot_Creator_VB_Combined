Imports System.Net.NetworkInformation
Imports NETCONLib

Namespace IcsManagerLibrary
	Public Class IcsManager
		Private Shared ReadOnly SharingManager As INetSharingManager = New NetSharingManager()

		Public Shared Function GetIPv4EthernetAndWirelessInterfaces() As IEnumerable(Of NetworkInterface)
			Return From nic In NetworkInterface.GetAllNetworkInterfaces() _
			       Where nic.Supports(NetworkInterfaceComponent.IPv4) _
			       Where (nic.NetworkInterfaceType = NetworkInterfaceType.Ethernet) OrElse (nic.NetworkInterfaceType = NetworkInterfaceType.Wireless80211) OrElse (nic.NetworkInterfaceType = NetworkInterfaceType.GigabitEthernet) _
			       Select nic
		End Function

		Public Shared Function GetAllIPv4Interfaces() As IEnumerable(Of NetworkInterface)
			Return From nic In NetworkInterface.GetAllNetworkInterfaces() _
			       Where nic.Supports(NetworkInterfaceComponent.IPv4) _
			       Select nic
		End Function

		Public Shared Function GetCurrentlySharedConnections() As NetShare
			Dim sharedConnection As INetConnection = ( _
			    From c As INetConnection In SharingManager.EnumEveryConnection _
			    Where GetConfiguration(c).SharingEnabled _
			    Where GetConfiguration(c).SharingConnectionType = tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC _
			    Select c).DefaultIfEmpty(Nothing).First()
			Dim homeConnection As INetConnection = ( _
			    From c As INetConnection In SharingManager.EnumEveryConnection _
			    Where GetConfiguration(c).SharingEnabled _
			    Where GetConfiguration(c).SharingConnectionType = tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE _
			    Select c).DefaultIfEmpty(Nothing).First()
			Return New NetShare(sharedConnection, homeConnection)
		End Function

		Public Shared Sub ShareConnection(ByVal connectionToShare As INetConnection, ByVal homeConnection As INetConnection)
			If (connectionToShare Is homeConnection) AndAlso (connectionToShare IsNot Nothing) Then
				Throw New ArgumentException("Connections must be different")
			End If
			Dim share = GetCurrentlySharedConnections()
			If share.SharedConnection IsNot Nothing Then
				GetConfiguration(share.SharedConnection).DisableSharing()
			End If
			If share.HomeConnection IsNot Nothing Then
				GetConfiguration(share.HomeConnection).DisableSharing()
			End If
			If connectionToShare IsNot Nothing Then
				Dim sc = GetConfiguration(connectionToShare)
				sc.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
			End If
			If homeConnection IsNot Nothing Then
				Dim hc = GetConfiguration(homeConnection)
				hc.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE)
			End If
		End Sub

		Public Shared Function GetConfiguration(ByVal connection As INetConnection) As INetSharingConfiguration
			Return SharingManager.INetSharingConfigurationForINetConnection(connection)
		End Function

		Public Shared Function GetProperties(ByVal connection As INetConnection) As INetConnectionProps
			Return SharingManager.NetConnectionProps(connection)
		End Function


		Public Shared Function GetAllConnections() As INetSharingEveryConnectionCollection
			Return SharingManager.EnumEveryConnection
		End Function

		Public Shared Function FindConnectionByIdOrName(ByVal [shared] As String) As INetConnection
			Return If(GetConnectionById([shared]), GetConnectionByName([shared]))
		End Function

		Public Shared Function GetConnectionById(ByVal guid As String) As INetConnection
			Return ( _
			    From c As INetConnection In GetAllConnections() _
			    Where GetProperties(c).Guid = guid _
			    Select c).DefaultIfEmpty(Nothing).First()
		End Function

		Public Shared Function GetConnectionByName(ByVal name As String) As INetConnection
			Return ( _
			    From c As INetConnection In GetAllConnections() _
			    Where GetProperties(c).Name = name _
			    Select c).DefaultIfEmpty(Nothing).First()
		End Function

	End Class
End Namespace
