Imports NETCONLib

Namespace IcsManagerLibrary
	Public Class NetShare

		Public SharedConnection As INetConnection

		Public HomeConnection As INetConnection

		Public Sub New(ByVal sharedConnection As INetConnection, ByVal homeConnection As INetConnection)
			Me.SharedConnection = sharedConnection
			Me.HomeConnection = homeConnection
		End Sub

		Public ReadOnly Property Exists() As Boolean
			Get
				Return (SharedConnection IsNot Nothing) AndAlso (HomeConnection IsNot Nothing)
			End Get
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("{0} -> {1}", SharedConnection, HomeConnection)
		End Function

	End Class

End Namespace
