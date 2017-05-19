<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tb_Twitter = New System.Windows.Forms.TextBox()
        Me.lbl_Twitter = New System.Windows.Forms.Label()
        Me.btn_scrape = New System.Windows.Forms.Button()
        Me.bw_TwitterGetter = New System.ComponentModel.BackgroundWorker()
        Me.pbProfilePic = New System.Windows.Forms.PictureBox()
        Me.lblUserStats = New System.Windows.Forms.Label()
        Me.lblTweets = New System.Windows.Forms.Label()
        Me.lblFollowers = New System.Windows.Forms.Label()
        Me.lblFollowing = New System.Windows.Forms.Label()
        Me.lblLikes = New System.Windows.Forms.Label()
        Me.lblFollowingCount = New System.Windows.Forms.Label()
        Me.lblFollowersCount = New System.Windows.Forms.Label()
        Me.lblTweetsCount = New System.Windows.Forms.Label()
        Me.lblLikesCount = New System.Windows.Forms.Label()
        Me.tbcMain = New System.Windows.Forms.TabControl()
        Me.tpTwitter = New System.Windows.Forms.TabPage()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.dgvTweets = New System.Windows.Forms.DataGridView()
        Me.lblProfilePic = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.tbLinkedin = New System.Windows.Forms.TabPage()
        Me.btn_DirectURL = New System.Windows.Forms.Button()
        Me.cb_User = New System.Windows.Forms.CheckBox()
        Me.cb_Company = New System.Windows.Forms.CheckBox()
        Me.tb_URL = New System.Windows.Forms.TextBox()
        Me.lbl_URL = New System.Windows.Forms.Label()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblLinkedinOther = New System.Windows.Forms.Label()
        Me.lblLinkedinState = New System.Windows.Forms.Label()
        Me.lblLinkedinCity = New System.Windows.Forms.Label()
        Me.lblLinkedinCompany = New System.Windows.Forms.Label()
        Me.lblLinkedInName = New System.Windows.Forms.Label()
        Me.tbLinkedinOther = New System.Windows.Forms.TextBox()
        Me.tbLinkedinState = New System.Windows.Forms.TextBox()
        Me.tbLinkedinCity = New System.Windows.Forms.TextBox()
        Me.tbLinkedinCompany = New System.Windows.Forms.TextBox()
        Me.tbLinkedinName = New System.Windows.Forms.TextBox()
        Me.pbLinkedinProfilePic = New System.Windows.Forms.PictureBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.pbProfilePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbcMain.SuspendLayout()
        Me.tpTwitter.SuspendLayout()
        CType(Me.dgvTweets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbLinkedin.SuspendLayout()
        CType(Me.pbLinkedinProfilePic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tb_Twitter
        '
        Me.tb_Twitter.Location = New System.Drawing.Point(162, 16)
        Me.tb_Twitter.Name = "tb_Twitter"
        Me.tb_Twitter.Size = New System.Drawing.Size(100, 20)
        Me.tb_Twitter.TabIndex = 1
        '
        'lbl_Twitter
        '
        Me.lbl_Twitter.AutoSize = True
        Me.lbl_Twitter.Location = New System.Drawing.Point(98, 19)
        Me.lbl_Twitter.Name = "lbl_Twitter"
        Me.lbl_Twitter.Size = New System.Drawing.Size(58, 13)
        Me.lbl_Twitter.TabIndex = 2
        Me.lbl_Twitter.Text = "Username:"
        '
        'btn_scrape
        '
        Me.btn_scrape.Location = New System.Drawing.Point(283, 16)
        Me.btn_scrape.Name = "btn_scrape"
        Me.btn_scrape.Size = New System.Drawing.Size(76, 20)
        Me.btn_scrape.TabIndex = 4
        Me.btn_scrape.Text = "Get Tweets"
        Me.btn_scrape.UseVisualStyleBackColor = True
        '
        'bw_TwitterGetter
        '
        Me.bw_TwitterGetter.WorkerReportsProgress = True
        '
        'pbProfilePic
        '
        Me.pbProfilePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbProfilePic.Location = New System.Drawing.Point(14, 16)
        Me.pbProfilePic.Name = "pbProfilePic"
        Me.pbProfilePic.Size = New System.Drawing.Size(58, 50)
        Me.pbProfilePic.TabIndex = 5
        Me.pbProfilePic.TabStop = False
        '
        'lblUserStats
        '
        Me.lblUserStats.AutoSize = True
        Me.lblUserStats.Location = New System.Drawing.Point(39, 94)
        Me.lblUserStats.Name = "lblUserStats"
        Me.lblUserStats.Size = New System.Drawing.Size(56, 13)
        Me.lblUserStats.TabIndex = 7
        Me.lblUserStats.Text = "User Stats"
        '
        'lblTweets
        '
        Me.lblTweets.AutoSize = True
        Me.lblTweets.Location = New System.Drawing.Point(82, 185)
        Me.lblTweets.Name = "lblTweets"
        Me.lblTweets.Size = New System.Drawing.Size(42, 13)
        Me.lblTweets.TabIndex = 8
        Me.lblTweets.Text = "Tweets"
        '
        'lblFollowers
        '
        Me.lblFollowers.AutoSize = True
        Me.lblFollowers.Location = New System.Drawing.Point(84, 123)
        Me.lblFollowers.Name = "lblFollowers"
        Me.lblFollowers.Size = New System.Drawing.Size(51, 13)
        Me.lblFollowers.TabIndex = 8
        Me.lblFollowers.Text = "Followers"
        '
        'lblFollowing
        '
        Me.lblFollowing.AutoSize = True
        Me.lblFollowing.Location = New System.Drawing.Point(11, 123)
        Me.lblFollowing.Name = "lblFollowing"
        Me.lblFollowing.Size = New System.Drawing.Size(51, 13)
        Me.lblFollowing.TabIndex = 8
        Me.lblFollowing.Text = "Following"
        '
        'lblLikes
        '
        Me.lblLikes.AutoSize = True
        Me.lblLikes.Location = New System.Drawing.Point(11, 185)
        Me.lblLikes.Name = "lblLikes"
        Me.lblLikes.Size = New System.Drawing.Size(32, 13)
        Me.lblLikes.TabIndex = 8
        Me.lblLikes.Text = "Likes"
        '
        'lblFollowingCount
        '
        Me.lblFollowingCount.AutoSize = True
        Me.lblFollowingCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFollowingCount.Location = New System.Drawing.Point(12, 151)
        Me.lblFollowingCount.Name = "lblFollowingCount"
        Me.lblFollowingCount.Size = New System.Drawing.Size(37, 15)
        Me.lblFollowingCount.TabIndex = 9
        Me.lblFollowingCount.Text = "Count"
        '
        'lblFollowersCount
        '
        Me.lblFollowersCount.AutoSize = True
        Me.lblFollowersCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFollowersCount.Location = New System.Drawing.Point(87, 151)
        Me.lblFollowersCount.Name = "lblFollowersCount"
        Me.lblFollowersCount.Size = New System.Drawing.Size(37, 15)
        Me.lblFollowersCount.TabIndex = 9
        Me.lblFollowersCount.Text = "Count"
        '
        'lblTweetsCount
        '
        Me.lblTweetsCount.AutoSize = True
        Me.lblTweetsCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTweetsCount.Location = New System.Drawing.Point(87, 210)
        Me.lblTweetsCount.Name = "lblTweetsCount"
        Me.lblTweetsCount.Size = New System.Drawing.Size(37, 15)
        Me.lblTweetsCount.TabIndex = 9
        Me.lblTweetsCount.Text = "Count"
        '
        'lblLikesCount
        '
        Me.lblLikesCount.AutoSize = True
        Me.lblLikesCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLikesCount.Location = New System.Drawing.Point(14, 210)
        Me.lblLikesCount.Name = "lblLikesCount"
        Me.lblLikesCount.Size = New System.Drawing.Size(37, 15)
        Me.lblLikesCount.TabIndex = 9
        Me.lblLikesCount.Text = "Count"
        '
        'tbcMain
        '
        Me.tbcMain.Controls.Add(Me.tpTwitter)
        Me.tbcMain.Controls.Add(Me.tbLinkedin)
        Me.tbcMain.Location = New System.Drawing.Point(-4, 0)
        Me.tbcMain.Name = "tbcMain"
        Me.tbcMain.SelectedIndex = 0
        Me.tbcMain.Size = New System.Drawing.Size(479, 379)
        Me.tbcMain.TabIndex = 10
        '
        'tpTwitter
        '
        Me.tpTwitter.Controls.Add(Me.ProgressBar1)
        Me.tpTwitter.Controls.Add(Me.dgvTweets)
        Me.tpTwitter.Controls.Add(Me.lblProfilePic)
        Me.tpTwitter.Controls.Add(Me.lblLikesCount)
        Me.tpTwitter.Controls.Add(Me.pbProfilePic)
        Me.tpTwitter.Controls.Add(Me.lblTweetsCount)
        Me.tpTwitter.Controls.Add(Me.btnUpdate)
        Me.tpTwitter.Controls.Add(Me.btn_scrape)
        Me.tpTwitter.Controls.Add(Me.lblFollowersCount)
        Me.tpTwitter.Controls.Add(Me.tb_Twitter)
        Me.tpTwitter.Controls.Add(Me.lblFollowingCount)
        Me.tpTwitter.Controls.Add(Me.lbl_Twitter)
        Me.tpTwitter.Controls.Add(Me.lblLikes)
        Me.tpTwitter.Controls.Add(Me.lblUserStats)
        Me.tpTwitter.Controls.Add(Me.lblFollowing)
        Me.tpTwitter.Controls.Add(Me.lblFollowers)
        Me.tpTwitter.Controls.Add(Me.lblTweets)
        Me.tpTwitter.Location = New System.Drawing.Point(4, 22)
        Me.tpTwitter.Name = "tpTwitter"
        Me.tpTwitter.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTwitter.Size = New System.Drawing.Size(471, 353)
        Me.tpTwitter.TabIndex = 0
        Me.tpTwitter.Text = "Twitter"
        Me.tpTwitter.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(283, 42)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(174, 23)
        Me.ProgressBar1.TabIndex = 13
        '
        'dgvTweets
        '
        Me.dgvTweets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTweets.Location = New System.Drawing.Point(217, 94)
        Me.dgvTweets.Name = "dgvTweets"
        Me.dgvTweets.Size = New System.Drawing.Size(240, 150)
        Me.dgvTweets.TabIndex = 12
        '
        'lblProfilePic
        '
        Me.lblProfilePic.AutoSize = True
        Me.lblProfilePic.Location = New System.Drawing.Point(9, 69)
        Me.lblProfilePic.Name = "lblProfilePic"
        Me.lblProfilePic.Size = New System.Drawing.Size(72, 13)
        Me.lblProfilePic.TabIndex = 10
        Me.lblProfilePic.Text = "Profile Picture"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(365, 16)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(92, 20)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update Stats"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'tbLinkedin
        '
        Me.tbLinkedin.Controls.Add(Me.btn_DirectURL)
        Me.tbLinkedin.Controls.Add(Me.cb_User)
        Me.tbLinkedin.Controls.Add(Me.cb_Company)
        Me.tbLinkedin.Controls.Add(Me.tb_URL)
        Me.tbLinkedin.Controls.Add(Me.lbl_URL)
        Me.tbLinkedin.Controls.Add(Me.WebBrowser1)
        Me.tbLinkedin.Controls.Add(Me.Button1)
        Me.tbLinkedin.Controls.Add(Me.lblLinkedinOther)
        Me.tbLinkedin.Controls.Add(Me.lblLinkedinState)
        Me.tbLinkedin.Controls.Add(Me.lblLinkedinCity)
        Me.tbLinkedin.Controls.Add(Me.lblLinkedinCompany)
        Me.tbLinkedin.Controls.Add(Me.lblLinkedInName)
        Me.tbLinkedin.Controls.Add(Me.tbLinkedinOther)
        Me.tbLinkedin.Controls.Add(Me.tbLinkedinState)
        Me.tbLinkedin.Controls.Add(Me.tbLinkedinCity)
        Me.tbLinkedin.Controls.Add(Me.tbLinkedinCompany)
        Me.tbLinkedin.Controls.Add(Me.tbLinkedinName)
        Me.tbLinkedin.Controls.Add(Me.pbLinkedinProfilePic)
        Me.tbLinkedin.Location = New System.Drawing.Point(4, 22)
        Me.tbLinkedin.Name = "tbLinkedin"
        Me.tbLinkedin.Padding = New System.Windows.Forms.Padding(3)
        Me.tbLinkedin.Size = New System.Drawing.Size(471, 330)
        Me.tbLinkedin.TabIndex = 1
        Me.tbLinkedin.Text = "Linkedin"
        Me.tbLinkedin.UseVisualStyleBackColor = True
        '
        'btn_DirectURL
        '
        Me.btn_DirectURL.Location = New System.Drawing.Point(182, 145)
        Me.btn_DirectURL.Name = "btn_DirectURL"
        Me.btn_DirectURL.Size = New System.Drawing.Size(107, 23)
        Me.btn_DirectURL.TabIndex = 9
        Me.btn_DirectURL.Text = "Use Direct URL"
        Me.btn_DirectURL.UseVisualStyleBackColor = True
        '
        'cb_User
        '
        Me.cb_User.AutoSize = True
        Me.cb_User.Location = New System.Drawing.Point(358, 66)
        Me.cb_User.Name = "cb_User"
        Me.cb_User.Size = New System.Drawing.Size(48, 17)
        Me.cb_User.TabIndex = 8
        Me.cb_User.Text = "User"
        Me.cb_User.UseVisualStyleBackColor = True
        '
        'cb_Company
        '
        Me.cb_Company.AutoSize = True
        Me.cb_Company.Location = New System.Drawing.Point(358, 44)
        Me.cb_Company.Name = "cb_Company"
        Me.cb_Company.Size = New System.Drawing.Size(70, 17)
        Me.cb_Company.TabIndex = 8
        Me.cb_Company.Text = "Company"
        Me.cb_Company.UseVisualStyleBackColor = True
        '
        'tb_URL
        '
        Me.tb_URL.Location = New System.Drawing.Point(106, 15)
        Me.tb_URL.Name = "tb_URL"
        Me.tb_URL.Size = New System.Drawing.Size(236, 20)
        Me.tb_URL.TabIndex = 5
        Me.tb_URL.Visible = False
        '
        'lbl_URL
        '
        Me.lbl_URL.AutoSize = True
        Me.lbl_URL.Location = New System.Drawing.Point(72, 18)
        Me.lbl_URL.Name = "lbl_URL"
        Me.lbl_URL.Size = New System.Drawing.Size(29, 13)
        Me.lbl_URL.TabIndex = 7
        Me.lbl_URL.Text = "URL"
        Me.lbl_URL.Visible = False
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(6, 145)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(268, 195)
        Me.WebBrowser1.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(358, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblLinkedinOther
        '
        Me.lblLinkedinOther.AutoSize = True
        Me.lblLinkedinOther.Location = New System.Drawing.Point(103, 122)
        Me.lblLinkedinOther.Name = "lblLinkedinOther"
        Me.lblLinkedinOther.Size = New System.Drawing.Size(33, 13)
        Me.lblLinkedinOther.TabIndex = 2
        Me.lblLinkedinOther.Text = "Other"
        '
        'lblLinkedinState
        '
        Me.lblLinkedinState.AutoSize = True
        Me.lblLinkedinState.Location = New System.Drawing.Point(103, 96)
        Me.lblLinkedinState.Name = "lblLinkedinState"
        Me.lblLinkedinState.Size = New System.Drawing.Size(32, 13)
        Me.lblLinkedinState.TabIndex = 2
        Me.lblLinkedinState.Text = "State"
        '
        'lblLinkedinCity
        '
        Me.lblLinkedinCity.AutoSize = True
        Me.lblLinkedinCity.Location = New System.Drawing.Point(103, 70)
        Me.lblLinkedinCity.Name = "lblLinkedinCity"
        Me.lblLinkedinCity.Size = New System.Drawing.Size(24, 13)
        Me.lblLinkedinCity.TabIndex = 2
        Me.lblLinkedinCity.Text = "City"
        '
        'lblLinkedinCompany
        '
        Me.lblLinkedinCompany.AutoSize = True
        Me.lblLinkedinCompany.Location = New System.Drawing.Point(103, 44)
        Me.lblLinkedinCompany.Name = "lblLinkedinCompany"
        Me.lblLinkedinCompany.Size = New System.Drawing.Size(51, 13)
        Me.lblLinkedinCompany.TabIndex = 2
        Me.lblLinkedinCompany.Text = "Company"
        '
        'lblLinkedInName
        '
        Me.lblLinkedInName.AutoSize = True
        Me.lblLinkedInName.Location = New System.Drawing.Point(103, 18)
        Me.lblLinkedInName.Name = "lblLinkedInName"
        Me.lblLinkedInName.Size = New System.Drawing.Size(35, 13)
        Me.lblLinkedInName.TabIndex = 2
        Me.lblLinkedInName.Text = "Name"
        '
        'tbLinkedinOther
        '
        Me.tbLinkedinOther.Location = New System.Drawing.Point(170, 119)
        Me.tbLinkedinOther.Name = "tbLinkedinOther"
        Me.tbLinkedinOther.Size = New System.Drawing.Size(143, 20)
        Me.tbLinkedinOther.TabIndex = 5
        '
        'tbLinkedinState
        '
        Me.tbLinkedinState.Location = New System.Drawing.Point(170, 93)
        Me.tbLinkedinState.Name = "tbLinkedinState"
        Me.tbLinkedinState.Size = New System.Drawing.Size(143, 20)
        Me.tbLinkedinState.TabIndex = 4
        '
        'tbLinkedinCity
        '
        Me.tbLinkedinCity.Location = New System.Drawing.Point(170, 67)
        Me.tbLinkedinCity.Name = "tbLinkedinCity"
        Me.tbLinkedinCity.Size = New System.Drawing.Size(143, 20)
        Me.tbLinkedinCity.TabIndex = 3
        '
        'tbLinkedinCompany
        '
        Me.tbLinkedinCompany.Location = New System.Drawing.Point(170, 41)
        Me.tbLinkedinCompany.Name = "tbLinkedinCompany"
        Me.tbLinkedinCompany.Size = New System.Drawing.Size(143, 20)
        Me.tbLinkedinCompany.TabIndex = 2
        '
        'tbLinkedinName
        '
        Me.tbLinkedinName.Location = New System.Drawing.Point(170, 15)
        Me.tbLinkedinName.Name = "tbLinkedinName"
        Me.tbLinkedinName.Size = New System.Drawing.Size(143, 20)
        Me.tbLinkedinName.TabIndex = 1
        '
        'pbLinkedinProfilePic
        '
        Me.pbLinkedinProfilePic.Location = New System.Drawing.Point(10, 15)
        Me.pbLinkedinProfilePic.Name = "pbLinkedinProfilePic"
        Me.pbLinkedinProfilePic.Size = New System.Drawing.Size(56, 57)
        Me.pbLinkedinProfilePic.TabIndex = 0
        Me.pbLinkedinProfilePic.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 375)
        Me.Controls.Add(Me.tbcMain)
        Me.Name = "frmMain"
        Me.Text = "Web Scraper"
        CType(Me.pbProfilePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbcMain.ResumeLayout(False)
        Me.tpTwitter.ResumeLayout(False)
        Me.tpTwitter.PerformLayout()
        CType(Me.dgvTweets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbLinkedin.ResumeLayout(False)
        Me.tbLinkedin.PerformLayout()
        CType(Me.pbLinkedinProfilePic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tb_Twitter As TextBox
    Friend WithEvents lbl_Twitter As Label
    Private WithEvents btn_scrape As Button
    Friend WithEvents bw_TwitterGetter As System.ComponentModel.BackgroundWorker
    Friend WithEvents pbProfilePic As PictureBox
    Friend WithEvents lblUserStats As Label
    Friend WithEvents lblTweets As Label
    Friend WithEvents lblFollowers As Label
    Friend WithEvents lblFollowing As Label
    Friend WithEvents lblLikes As Label
    Friend WithEvents lblFollowingCount As Label
    Friend WithEvents lblFollowersCount As Label
    Friend WithEvents lblTweetsCount As Label
    Friend WithEvents lblLikesCount As Label
    Friend WithEvents tbcMain As TabControl
    Friend WithEvents tpTwitter As TabPage
    Friend WithEvents tbLinkedin As TabPage
    Friend WithEvents lblProfilePic As Label
    Private WithEvents btnUpdate As Button
    Friend WithEvents dgvTweets As DataGridView
    Friend WithEvents lblLinkedinState As Label
    Friend WithEvents lblLinkedinCity As Label
    Friend WithEvents lblLinkedinCompany As Label
    Friend WithEvents lblLinkedInName As Label
    Friend WithEvents tbLinkedinState As TextBox
    Friend WithEvents tbLinkedinCity As TextBox
    Friend WithEvents tbLinkedinCompany As TextBox
    Friend WithEvents tbLinkedinName As TextBox
    Friend WithEvents pbLinkedinProfilePic As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents lblLinkedinOther As Label
    Friend WithEvents tbLinkedinOther As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents cb_User As CheckBox
    Friend WithEvents cb_Company As CheckBox
    Friend WithEvents tb_URL As TextBox
    Friend WithEvents lbl_URL As Label
    Friend WithEvents btn_DirectURL As Button
    Friend WithEvents BindingSource1 As BindingSource
End Class
