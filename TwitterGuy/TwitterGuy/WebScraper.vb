Imports System.ComponentModel

Public Class frmMain
    Public User As String
    Public Address As String

    Private Sub tb_Twitter_KeyUp(sender As Object, e As KeyEventArgs) Handles tb_Twitter.KeyUp
        If e.KeyCode = Keys.Enter Then
            'GetTweetData(User)
            Updatestats(tb_Twitter.Text)
            If Not bw_TwitterGetter.IsBusy = True Then
                Me.ProgressBar1.Visible = True
                bw_TwitterGetter.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub tb_Twitter_TextChanged(sender As Object, e As EventArgs) Handles tb_Twitter.TextChanged
        User = tb_Twitter.Text
    End Sub

    Private Sub btn_scrape_Click(sender As Object, e As EventArgs) Handles btn_scrape.Click
        Updatestats(tb_Twitter.Text)
        If Not bw_TwitterGetter.IsBusy = True Then
            Me.ProgressBar1.Visible = True
            bw_TwitterGetter.RunWorkerAsync()
        Else
            If MsgBox("Already downloading tweets in background. Cancel download in progress?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then bw_TwitterGetter.CancelAsync() 'Cancel bw
        End If
    End Sub
    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Updatestats(tb_Twitter.Text)
    End Sub

    Private Sub bw_TwitterGetter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_TwitterGetter.DoWork
        GetTweetData(tb_Twitter.Text)
    End Sub

    Private Sub bw_TwitterGetter_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw_TwitterGetter.RunWorkerCompleted
        Me.ProgressBar1.Visible = False
    End Sub

    Private Sub bw_TwitterGetter_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bw_TwitterGetter.ProgressChanged
        ProgressBar1.Show()
        ProgressBar1.Value += (e.ProgressPercentage - ProgressBar1.Value + 1)
        If ProgressBar1.Value > 0 Then ProgressBar1.Value -= 1
        Application.DoEvents()
        'ProgressBar1.Update()
        'ProgressBar1.Increment(e.ProgressPercentage)
        'ProgressBar1.Increment(10)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim searchterms(5)
        searchterms(0) = tbLinkedinName.Text
        searchterms(1) = tbLinkedinCompany.Text
        searchterms(2) = tbLinkedinCity.Text
        searchterms(3) = tbLinkedinState.Text
        searchterms(4) = tbLinkedinOther.Text
        searchterms(5) = tb_URL.Text
        FindCorrectUser(SearchTerms)
    End Sub

    Private Sub cb_Company_CheckedChanged(sender As Object, e As EventArgs) Handles cb_Company.CheckedChanged
        If cb_Company.Checked Then
            cb_User.CheckState = CheckState.Unchecked
            tb_URL.Text = "https://www.linkedin.com/company/"
        Else
            cb_User.CheckState = CheckState.Checked
            tb_URL.Text = "https://www.linkedin.com/in/"
        End If
    End Sub

    Private Sub cb_User_CheckedChanged(sender As Object, e As EventArgs) Handles cb_User.CheckedChanged
        If cb_User.Checked Then
            cb_Company.CheckState = CheckState.Unchecked
            tb_URL.Text = "https://www.linkedin.com/in/"
        Else
            cb_Company.CheckState = CheckState.Checked
            tb_URL.Text = "https://www.linkedin.com/company/"
        End If
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        cb_User.CheckState = CheckState.Checked
        tb_URL.Text = "https://www.linkedin.com/in/"
    End Sub

    Private Sub btn_DirectURL_Click(sender As Object, e As EventArgs) Handles btn_DirectURL.Click
        If lbl_URL.Visible = True Then
            lbl_URL.Visible = False
            tb_URL.Visible = False
            lblLinkedinCity.Visible = True
            lblLinkedinCompany.Visible = True
            lblLinkedInName.Visible = True
            lblLinkedinOther.Visible = True
            lblLinkedinState.Visible = True
            tbLinkedinCity.Visible = True
            tbLinkedinCompany.Visible = True
            tbLinkedinOther.Visible = True
            tbLinkedinState.Visible = True
            tbLinkedinName.Visible = True
            btn_DirectURL.Text = "Use Direct URL"
        Else
            lbl_URL.Visible = True
            tb_URL.Visible = True
            lblLinkedinCity.Visible = False
            lblLinkedinCompany.Visible = False
            lblLinkedInName.Visible = False
            lblLinkedinOther.Visible = False
            lblLinkedinState.Visible = False
            tbLinkedinCity.Visible = False
            tbLinkedinCompany.Visible = False
            tbLinkedinOther.Visible = False
            tbLinkedinState.Visible = False
            tbLinkedinName.Visible = False
            btn_DirectURL.Text = "Search By Terms"
        End If
    End Sub
End Class
