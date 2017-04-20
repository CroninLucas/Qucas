Public Class CookieAwareWebClient
    Inherits Net.WebClient

    Private cc As New Net.CookieContainer()
    Private lastPage As String

    Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As System.Net.WebRequest
        Dim R = MyBase.GetWebRequest(address)
        If TypeOf R Is Net.HttpWebRequest Then
            With DirectCast(R, Net.HttpWebRequest)
                .CookieContainer = cc
                If Not lastPage Is Nothing Then
                    .Referer = lastPage
                End If
            End With
        End If
        lastPage = address.ToString()
        Return R
    End Function
End Class
