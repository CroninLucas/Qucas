<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlg_LinkedInChoose
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
        Me.btn_next = New System.Windows.Forms.Button()
        Me.btn_choose = New System.Windows.Forms.Button()
        Me.pb_ProfilePic = New System.Windows.Forms.PictureBox()
        Me.lbl_name = New System.Windows.Forms.Label()
        Me.lbl_ConnectionCount = New System.Windows.Forms.Label()
        Me.lbl_Location = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_summary = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Education = New System.Windows.Forms.Label()
        Me.lbl_PreviousPosition = New System.Windows.Forms.Label()
        Me.lbl_CurrentPosition = New System.Windows.Forms.Label()
        Me.tb_summary = New System.Windows.Forms.TextBox()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        CType(Me.pb_ProfilePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_next
        '
        Me.btn_next.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_next.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btn_next.Location = New System.Drawing.Point(220, 3)
        Me.btn_next.Name = "btn_next"
        Me.btn_next.Size = New System.Drawing.Size(80, 25)
        Me.btn_next.TabIndex = 0
        Me.btn_next.Text = "Next"
        Me.btn_next.UseVisualStyleBackColor = True
        '
        'btn_choose
        '
        Me.btn_choose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_choose.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btn_choose.Location = New System.Drawing.Point(134, 3)
        Me.btn_choose.Name = "btn_choose"
        Me.btn_choose.Size = New System.Drawing.Size(80, 25)
        Me.btn_choose.TabIndex = 2
        Me.btn_choose.Text = "Select"
        Me.btn_choose.UseVisualStyleBackColor = True
        '
        'pb_ProfilePic
        '
        Me.pb_ProfilePic.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pb_ProfilePic.Location = New System.Drawing.Point(15, 1)
        Me.pb_ProfilePic.Name = "pb_ProfilePic"
        Me.pb_ProfilePic.Size = New System.Drawing.Size(200, 200)
        Me.pb_ProfilePic.TabIndex = 3
        Me.pb_ProfilePic.TabStop = False
        '
        'lbl_name
        '
        Me.lbl_name.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_name.AutoSize = True
        Me.lbl_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_name.Location = New System.Drawing.Point(253, 16)
        Me.lbl_name.Name = "lbl_name"
        Me.lbl_name.Size = New System.Drawing.Size(65, 24)
        Me.lbl_name.TabIndex = 4
        Me.lbl_name.Text = "Name"
        '
        'lbl_ConnectionCount
        '
        Me.lbl_ConnectionCount.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_ConnectionCount.AutoSize = True
        Me.lbl_ConnectionCount.Location = New System.Drawing.Point(455, 16)
        Me.lbl_ConnectionCount.Name = "lbl_ConnectionCount"
        Me.lbl_ConnectionCount.Size = New System.Drawing.Size(66, 13)
        Me.lbl_ConnectionCount.TabIndex = 4
        Me.lbl_ConnectionCount.Text = "Connections"
        '
        'lbl_Location
        '
        Me.lbl_Location.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_Location.AutoSize = True
        Me.lbl_Location.Location = New System.Drawing.Point(257, 55)
        Me.lbl_Location.Name = "lbl_Location"
        Me.lbl_Location.Size = New System.Drawing.Size(48, 13)
        Me.lbl_Location.TabIndex = 4
        Me.lbl_Location.Text = "Location"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btn_next)
        Me.Panel1.Controls.Add(Me.btn_Cancel)
        Me.Panel1.Controls.Add(Me.btn_choose)
        Me.Panel1.Location = New System.Drawing.Point(62, 360)
        Me.Panel1.MinimumSize = New System.Drawing.Size(270, 35)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(452, 35)
        Me.Panel1.TabIndex = 6
        '
        'lbl_summary
        '
        Me.lbl_summary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_summary.AutoSize = True
        Me.lbl_summary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_summary.Location = New System.Drawing.Point(28, 220)
        Me.lbl_summary.Name = "lbl_summary"
        Me.lbl_summary.Size = New System.Drawing.Size(65, 16)
        Me.lbl_summary.TabIndex = 4
        Me.lbl_summary.Text = "Summary"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.lbl_Education)
        Me.Panel2.Controls.Add(Me.lbl_PreviousPosition)
        Me.Panel2.Controls.Add(Me.lbl_CurrentPosition)
        Me.Panel2.Controls.Add(Me.tb_summary)
        Me.Panel2.Controls.Add(Me.lbl_summary)
        Me.Panel2.Controls.Add(Me.lbl_Location)
        Me.Panel2.Controls.Add(Me.lbl_ConnectionCount)
        Me.Panel2.Controls.Add(Me.lbl_name)
        Me.Panel2.Controls.Add(Me.pb_ProfilePic)
        Me.Panel2.Location = New System.Drawing.Point(9, 11)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(541, 308)
        Me.Panel2.TabIndex = 7
        '
        'lbl_Education
        '
        Me.lbl_Education.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_Education.AutoSize = True
        Me.lbl_Education.Location = New System.Drawing.Point(257, 174)
        Me.lbl_Education.Name = "lbl_Education"
        Me.lbl_Education.Size = New System.Drawing.Size(55, 13)
        Me.lbl_Education.TabIndex = 7
        Me.lbl_Education.Text = "Education"
        '
        'lbl_PreviousPosition
        '
        Me.lbl_PreviousPosition.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_PreviousPosition.AutoSize = True
        Me.lbl_PreviousPosition.Location = New System.Drawing.Point(257, 133)
        Me.lbl_PreviousPosition.Name = "lbl_PreviousPosition"
        Me.lbl_PreviousPosition.Size = New System.Drawing.Size(88, 13)
        Me.lbl_PreviousPosition.TabIndex = 7
        Me.lbl_PreviousPosition.Text = "Previous Position"
        '
        'lbl_CurrentPosition
        '
        Me.lbl_CurrentPosition.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_CurrentPosition.AutoSize = True
        Me.lbl_CurrentPosition.Location = New System.Drawing.Point(257, 96)
        Me.lbl_CurrentPosition.Name = "lbl_CurrentPosition"
        Me.lbl_CurrentPosition.Size = New System.Drawing.Size(81, 13)
        Me.lbl_CurrentPosition.TabIndex = 7
        Me.lbl_CurrentPosition.Text = "Current Position"
        '
        'tb_summary
        '
        Me.tb_summary.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tb_summary.BackColor = System.Drawing.SystemColors.Control
        Me.tb_summary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tb_summary.Location = New System.Drawing.Point(31, 246)
        Me.tb_summary.Multiline = True
        Me.tb_summary.Name = "tb_summary"
        Me.tb_summary.ReadOnly = True
        Me.tb_summary.Size = New System.Drawing.Size(479, 59)
        Me.tb_summary.TabIndex = 6
        Me.tb_summary.Text = "Summary"
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(48, 3)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(80, 25)
        Me.btn_Cancel.TabIndex = 2
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'dlg_LinkedInChoose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 407)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "dlg_LinkedInChoose"
        Me.Text = "Choose Profile"
        CType(Me.pb_ProfilePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_next As Button
    Friend WithEvents btn_choose As Button
    Friend WithEvents pb_ProfilePic As PictureBox
    Friend WithEvents lbl_name As Label
    Friend WithEvents lbl_ConnectionCount As Label
    Friend WithEvents lbl_Location As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_summary As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents tb_summary As TextBox
    Friend WithEvents lbl_Education As Label
    Friend WithEvents lbl_PreviousPosition As Label
    Friend WithEvents lbl_CurrentPosition As Label
    Friend WithEvents btn_Cancel As Button
End Class
