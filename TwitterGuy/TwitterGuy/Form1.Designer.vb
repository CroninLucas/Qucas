<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tb_Twitter = New System.Windows.Forms.TextBox()
        Me.lbl_Twitter = New System.Windows.Forms.Label()
        Me.btn_scrape = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'tb_Twitter
        '
        Me.tb_Twitter.Location = New System.Drawing.Point(125, 12)
        Me.tb_Twitter.Name = "tb_Twitter"
        Me.tb_Twitter.Size = New System.Drawing.Size(100, 20)
        Me.tb_Twitter.TabIndex = 1
        '
        'lbl_Twitter
        '
        Me.lbl_Twitter.AutoSize = True
        Me.lbl_Twitter.Location = New System.Drawing.Point(13, 12)
        Me.lbl_Twitter.Name = "lbl_Twitter"
        Me.lbl_Twitter.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Twitter.TabIndex = 2
        Me.lbl_Twitter.Text = "Twitter"
        '
        'btn_scrape
        '
        Me.btn_scrape.Location = New System.Drawing.Point(262, 10)
        Me.btn_scrape.Name = "btn_scrape"
        Me.btn_scrape.Size = New System.Drawing.Size(75, 23)
        Me.btn_scrape.TabIndex = 4
        Me.btn_scrape.Text = "Get Tweets"
        Me.btn_scrape.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 60)
        Me.Controls.Add(Me.btn_scrape)
        Me.Controls.Add(Me.lbl_Twitter)
        Me.Controls.Add(Me.tb_Twitter)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_Twitter As TextBox
    Friend WithEvents lbl_Twitter As Label
    Private WithEvents btn_scrape As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
