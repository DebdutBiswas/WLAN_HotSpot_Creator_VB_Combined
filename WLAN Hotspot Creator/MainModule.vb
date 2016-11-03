
Module MainModule

    Public IcsVirtualAdapterIdArray As New ComboBox
    Public IcsVirtualAdapterId As String
    Public Sub Main()

        Application.EnableVisualStyles()
        Application.Run(New TrayStartUp)

    End Sub

End Module