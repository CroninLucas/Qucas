Public Class Form1
    Public User As String
    Public Address As String

    Private Sub tb_Twitter_KeyUp(sender As Object, e As KeyEventArgs) Handles tb_Twitter.KeyUp
        If e.KeyCode = Keys.KeyCode.Enter Then
            GetTweetData(User)
        End If
    End Sub

    Private Sub tb_Twitter_TextChanged(sender As Object, e As EventArgs) Handles tb_Twitter.TextChanged
        User = tb_Twitter.Text
    End Sub

    Private Sub btn_scrape_Click(sender As Object, e As EventArgs) Handles btn_scrape.Click
        Dim user = tb_Twitter.Text
        GetTweetData(user)
    End Sub
End Class
