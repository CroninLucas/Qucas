Module mod_GetLinkedinData
    Public Sub FindCorrectUser(Searchterms)
        Dim Name = Searchterms(0)
        Dim Company = Searchterms(1)
        Dim City = Searchterms(2)
        Dim State = Searchterms(3)
        Dim Other = Searchterms(4)
        Dim DirectLink = Searchterms(5)
        Dim HtmlDoc = New HtmlAgilityPack.HtmlDocument()
        Dim address As String = "https://www.google.com/search?q=" & Replace(Name, " ", "+") & "+" & Replace(Company, " ", "+") & "+" & Replace(City, " ", "+") & "+" & Replace(State, " ", "+") & "+" & Replace(Other, " ", "+")
        If frmMain.cb_Company.Checked Then
            address = address & "+site:linkedin.com/company"
        Else
            address = address & "+site:linkedin.com/in"
        End If
        Dim owebbrowser = frmMain.WebBrowser1
        Dim linkContainer(10)
        Dim oChoose As New dlg_LinkedInChoose
        Dim Name1 As String = ""
        Dim ProfHeader As String = ""
        Dim Location As String = ""
        Dim Industry As String = ""
        Dim CurrentPositionName As String = ""
        Dim PreviousPositionName As String = ""
        Dim EducationName As String = ""
        Dim ConnectionsCount As String = ""
        Dim SummaryText As String = ""
        Dim ProfileDoc = New HtmlAgilityPack.HtmlDocument
        Dim Pic As String = ""

        If frmMain.tb_URL.Visible = True Then   'Kind of janky, maybe if directlink not ""
            linkContainer(0) = DirectLink
            owebbrowser.Visible = False
            GoTo NavigateLinkedIn
        End If

        owebbrowser.Visible = False
        owebbrowser.Navigate(address)
        Application.DoEvents()

        'Gets the results as html nodes

        Dim resultcontainernode
        Do Until resultcontainernode IsNot Nothing
            Try
                HtmlDoc.LoadHtml(owebbrowser.Document.Body.OuterHtml)
                resultcontainernode = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""rso""]/div/div")
            Catch
                Application.DoEvents()
            End Try
            Application.DoEvents()
        Loop

        'Get the linkedin Link
        Dim i As Integer
        If My.Forms.frmMain.tb_URL.Visible Then
            linkContainer(0) = DirectLink
            For i = 2 To resultcontainernode.ChildNodes.Count - 1
                linkContainer(i - 1) = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""rso""]/div/div/div[" & i & "]/div/div/div/div/div[1]/cite").InnerText
            Next
        Else
            For i = 1 To resultcontainernode.ChildNodes.Count - 1
                linkContainer(i - 1) = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""rso""]/div/div/div[" & i & "]/div/div/div/div/div[1]/cite").InnerText
            Next
        End If


NavigateLinkedIn:

        Dim ii As Integer
        Dim DocContainer As New List(Of Object)

        For ii = 0 To UBound(linkContainer)
            If linkContainer(ii) Is Nothing Then Exit For
            owebbrowser.Navigate(linkContainer(ii))
            Application.DoEvents()
            Threading.Thread.Sleep(2000)
            Application.DoEvents()
            Do Until owebbrowser.IsBusy = False
                Application.DoEvents()
                Threading.Thread.Sleep(1000)
            Loop
            Try
                ProfileDoc.LoadHtml(owebbrowser.Document.Body.OuterHtml)
            Catch ex As Exception
            End Try
            DocContainer.Add(ProfileDoc)


            'All of this shit is optional so i need to make it robust, programming against having nothing and 
            'Selection of company or directory. 
            'What cases are there? 
            ' Company, Directory, PersonProfile, More?

            With ProfileDoc.DocumentNode
                Try
                    Pic = .SelectSingleNode("//*[@id=""topcard""]/div[1]/div[1]/a[1]").Attributes("href").Value
                Catch ex As Exception
                End Try
                Try
                    Name1 = .SelectSingleNode("//*[@id=""name""]").InnerHtml
                Catch ex As Exception
                End Try
                Try
                    ProfHeader = .SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/p").InnerHtml
                Catch ex As Exception
                End Try
                Try
                    Location = .SelectSingleNode("//*[@id=""demographics""]/dd[1]/span").InnerHtml
                Catch ex As Exception
                End Try
                Try
                    Industry = .SelectSingleNode("//*[@id=""demographics""]/dd[2]").InnerHtml
                Catch ex As Exception
                End Try
                Try
                    CurrentPositionName = .SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[1]/td/ol/li/span").ChildNodes(0).InnerHtml
                Catch ex As Exception
                End Try
                Try
                    PreviousPositionName = .SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[2]/td/ol/li").ChildNodes(0).InnerHtml
                Catch ex As Exception
                End Try
                Try
                    EducationName = .SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[3]/td/ol/li/a").InnerHtml
                Catch ex As Exception
                End Try
                Try
                    ConnectionsCount = .SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/div/strong").ChildNodes(0).InnerHtml
                Catch ex As Exception
                End Try
                Try
                    SummaryText = .SelectSingleNode("//*[@id=""summary""]/div/p/text()").InnerHtml
                Catch ex As Exception
                End Try

            End With
            With oChoose
                .lbl_name.Text = Name1
                .lbl_Location.Text = Location
                .lbl_ConnectionCount.Text = ConnectionsCount
                .lbl_Education.Text = EducationName
                .lbl_CurrentPosition.Text = CurrentPositionName
                .lbl_PreviousPosition.Text = PreviousPositionName
                .tb_summary.Text = SummaryText
                If Pic <> "" Then .pb_ProfilePic.Load(Pic)
                Application.DoEvents()
            End With
            oChoose.ShowDialog()
            If oChoose.DialogResult = DialogResult.Yes Then
                'Import into sql
                owebbrowser.Stop()
                'Addlinkto scrapething
                ScrapeUserPage(ProfileDoc, linkContainer(ii))
                Exit Sub
            ElseIf oChoose.DialogResult = DialogResult.Cancel Then
                Exit Sub
            End If

            'Pop up a dialogue box asking if this is the right profile they want
            'If it is, show everything
            'If its not, show next
            'Should never show directories until all the options are gone

        Next
        MsgBox("Could not find profile, please adjust your search terms")
        owebbrowser.Stop()

        'Idea is to sell a web scraper to underwriters
        'Scrape the website, twitter, linkedin and more if i can find it. Let the underwriter decide if they are who they say they are.

    End Sub
    Public Sub ScrapeUserPage(Document, Link)
        'Only for users, not companies or directories
        Dim x As Integer = 3
        Dim x1
        Dim x2
        Dim x3
        Dim x4
        Dim x5
        Dim Pic As String = ""
        Dim Name As String = ""
        Dim ProfHeader As String = ""
        Dim Location As String = ""
        Dim Industry As String = ""
        Dim CurrentPositionName As String = ""
        Dim PreviousPositionName As String = ""
        Dim CurrentPositionLink As String = ""
        Dim PreviousPositionLink As String = ""
        Dim EducationName As String = ""
        Dim EducationLink As String = ""
        Dim SummaryText As String = ""
        Dim Connectionscount As String = ""
        Dim i As Integer = 0

        'These are all optional that is why they are in try blocks
        'Try to think of better way to do this
        Try
            Pic = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div[1]/div[1]/a[1]").Attributes("href").Value
        Catch ex As Exception
        End Try
        Try
            Name = Document.documentnode.SelectSingleNode("//*[@id=""name""]").InnerHtml
        Catch ex As Exception
        End Try
        Try
            ProfHeader = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/p").InnerHtml
        Catch ex As Exception
        End Try
        Try
            Location = Document.documentnode.SelectSingleNode("//*[@id=""demographics""]/dd[1]/span").InnerHtml
        Catch ex As Exception
        End Try
        Try
            Industry = Document.documentnode.SelectSingleNode("//*[@id=""demographics""]/dd[2]").InnerHtml
        Catch ex As Exception
        End Try
        Try
            CurrentPositionName = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[1]/td/ol/li/span").ChildNodes(0).InnerHtml
            CurrentPositionLink = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[1]/td/ol/li/span").ChildNodes(0).attributes("href").value
        Catch ex As Exception
        End Try
        Try
            PreviousPositionName = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[2]/td/ol/li").ChildNodes(0).InnerHtml
            PreviousPositionLink = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[2]/td/ol/li").ChildNodes(0).attributes("href").value
        Catch ex As Exception
        End Try
        Try
            EducationName = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[3]/td/ol/li/a").InnerHtml
            EducationLink = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/table/tbody/tr[3]/td/ol/li/a").attributes("href").value
        Catch ex As Exception
        End Try
        Try
            Connectionscount = Document.documentnode.SelectSingleNode("//*[@id=""topcard""]/div/div[2]/div/div/strong").ChildNodes(0).InnerHtml
        Catch ex As Exception
        End Try
        Try
            SummaryText = Document.documentnode.SelectSingleNode("//*[@id=""summary""]/div/p/text()").InnerHtml
        Catch ex As Exception
        End Try

Activity:

        Dim Isactivity As Boolean = True
        Dim ActivityCount As Integer
        Try
            ActivityCount = Document.documentnode.SelectSingleNode("//*[@id=""feed""]/ul").childnodes.count
        Catch ex As Exception
            'No activity
            Isactivity = False
            GoTo Experience
        End Try
        Dim ActivityContainer(ActivityCount - 1) As String
        For i = 0 To ActivityCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""feed""]/ul/li[" & i + 1 & "]")
            If x1 IsNot Nothing Then
                ActivityContainer(i) = x1.childnodes(1).Childnodes(1).innertext         'Shared or liked or whatever. The only other data is a link to the thing he liked
            End If
            x1 = Nothing
        Next


Experience:


        Dim IsExperience As Boolean = True
        Dim expcount As Integer
        Try
            expcount = Document.documentnode.SelectSingleNode("//*[@id=""experience""]/ul").childnodes.count
        Catch
            'No experience!
            IsExperience = False
        End Try
        If IsExperience = False Then GoTo Education

        Dim ExperienceContainer(expcount - 1, 7) As String

        For i = 0 To expcount - 1
            x1 = Document.documentnode.SelectSingleNode("//*[@id=""experience""]/ul/li[" & i + 1 & "]/header/h4").childnodes(0)
            x2 = Document.documentnode.selectsinglenode("//*[@id=""experience""]/ul[1]/li[" & i + 1 & "]/header[1]/h5[1]/a[1]/img[1]")
            x3 = Document.documentnode.selectsinglenode("//*[@id=""experience""]/ul/li[" & i + 1 & "]/div/span[1]")
            x4 = Document.documentnode.selectsinglenode("//*[@id=""experience""]/ul/li[" & i + 1 & "]/div/span[2]")
            x5 = Document.documentnode.selectsinglenode("//*[@id=""experience""]/ul/li[" & i + 1 & "]/p")
            If x1 IsNot Nothing Then
                ExperienceContainer(i, 0) = x1.innerhtml 'header
                Try
                    ExperienceContainer(i, 1) = x1.attributes("href").value 'Link
                Catch ex As Exception
                End Try
            End If
            If x2 IsNot Nothing Then
                ExperienceContainer(i, 2) = x2.attributes("alt").value      'Subtitle
                ExperienceContainer(i, 3) = x2.attributes("data-delayed-url").value 'LOGO
                'ExperienceContainer(i, 4) = x2.parentnode.attributes("href").value     'Link         Could bring back but what is it for??
            End If
            If x3 IsNot Nothing Then
                ExperienceContainer(i, 4) = LinkedInDateToSQLDate(x3.childnodes(0).innerhtml)      'Daterange1
                Try
                    ExperienceContainer(i, 5) = LinkedInDateToSQLDate(x3.childnodes(2).innerhtml)
                Catch ex As Exception
                    ExperienceContainer(i, 5) = LinkedInDateToSQLDate(x3.childnodes(1).innerhtml)
                End Try
                'DateRange2
            End If
            If x4 IsNot Nothing Then
                ExperienceContainer(i, 6) = x4.innerhtml                    'Location
            End If
            If x5 IsNot Nothing Then
                ExperienceContainer(i, 7) = x5.innerhtml                    'Description
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next


Education:

        Dim IsEducation As Boolean = True
        Dim Edcount As Integer
        Try
            Edcount = Document.documentnode.selectsinglenode("//*[@id=""education""]/ul").childnodes.count
        Catch ex As Exception
            'No education!
            IsEducation = False
        End Try
        If IsEducation = False Then GoTo Languages
        Dim EducationContainer(Edcount - 1, 5) As String
        For i = 0 To Edcount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""education""]/ul/li[" & i + 1 & "]/header/h4/a")
            x2 = Document.documentnode.selectsinglenode("//*[@id=""education""]/ul/li[" & i + 1 & "]/header/h5[2]/span[2]")
            x3 = Document.documentnode.selectsinglenode("//*[@id=""education""]/ul/li[" & i + 1 & "]/div[1]/span")
            x4 = Document.documentnode.selectsinglenode("//*[@id=""education""]/ul/li[" & i + 1 & "]/div/p")
            If x1 IsNot Nothing Then
                EducationContainer(i, 0) = x1.innerhtml  'Ed title
                Try
                    EducationContainer(i, 1) = x1.attributes("href").value  'Ed title link
                Catch ex As Exception
                    'No Link
                End Try
            End If
            If x2 IsNot Nothing Then
                EducationContainer(i, 2) = x2.innerhtml     'Degree
            End If
            If x3 IsNot Nothing Then
                EducationContainer(i, 3) = LinkedInDateToSQLDate(x3.childnodes(0).innerhtml)           'Time1
                EducationContainer(i, 4) = LinkedInDateToSQLDate(x3.childnodes(2).innerhtml)        'Time2
            End If
            If x4 IsNot Nothing Then
                EducationContainer(i, 5) = x4.innerhtml             'Ed description
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next


Languages:

        Dim IsLanguages As Boolean = True
        Dim LangCount As Integer
        Try
            LangCount = Document.documentnode.selectsinglenode("//*[@id=""languages""]/ul").childnodes.count
        Catch ex As Exception
            'No Languages!
            IsLanguages = False
            GoTo Volunteering
        End Try
        Dim LanguagesContainer(LangCount - 1, 0) As String
        For i = 0 To LangCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""languages""]/ul")
            If x1 IsNot Nothing Then
                LanguagesContainer(i, 0) = x1.childnodes(i).innertext       'Language
            End If

            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next
Volunteering:

        Dim IsVolunteering As Boolean = True
        Dim VolunteerCount As Integer
        Try
            VolunteerCount = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/ul").childnodes.count
        Catch ex As Exception
            'No Volunteering!
            IsVolunteering = False
            GoTo Certifications
        End Try
        Dim VolunteeringContainer(VolunteerCount - 1, 4) As String
        For i = 0 To VolunteerCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/ul/li[" & i + 1 & "]/header[1]")
            x2 = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/ul/li[" & i + 1 & "]/div")
            x3 = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/ul/li[" & i + 1 & "]/p")
            If x1 IsNot Nothing Then
                VolunteeringContainer(i, 0) = x1.childnodes(0).innertext        'Position
                VolunteeringContainer(i, 1) = x1.childnodes(1).innertext        'Place
            End If
            If x2 IsNot Nothing Then
                VolunteeringContainer(i, 2) = LinkedInDateToSQLDate(x2.childnodes(0).innertext)        'Time
                'Add another time in there! some people have two
                VolunteeringContainer(i, 3) = x2.childnodes(1).innertext        'Cause
            End If
            If x3 IsNot Nothing Then
                VolunteeringContainer(i, 4) = x3.innerhtml          'Description
            End If

            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next

Causes:

        Dim Iscauses As Boolean = True
        Dim CauseCount As Integer
        Try
            CauseCount = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/div/ul").childnodes.count
        Catch ex As Exception
            'No Causes
            GoTo Certifications
        End Try
        Dim CauseContainer(CauseCount - 1) As String
        For i = 0 To CauseCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""volunteering""]/div/ul")
            Try
                CauseContainer(i) = x1.childnodes(i).innertext
            Catch ex As Exception
            End Try
            x1 = Nothing
        Next

Certifications:
        Dim IsCertifications As Boolean = True
        Dim CertCount As Integer
        Try
            CertCount = Document.documentnodes.selectsingleNode("//*[@id=""certifications""]/ul").childnodes.count
        Catch ex As Exception
            'No Certifications
            IsCertifications = False
            GoTo Recommendations
        End Try
        Dim CertContainer(CertCount, 3) As String
        For i = 0 To CertCount - 1
            x1 = Document.documentnodes.selectsingleNode("//*[@id=""certifications""]/ul/li[" & i + 1 & "]/h4/a")
            x2 = Document.documentnodes.selectsingleNode("//*[@id=""certifications""]/ul/li[" & i + 1 & "]/h5[2]/a")
            x3 = Document.documentnodes.selectsingleNode("//*[@id=""certifications""]/ul/li[" & i + 1 & "]/div/span/time")
            x4 = Document.documentnodes.selectsingleNode("//*[@id=""certifications""]/ul/li[" & i + 1 & "]/div/span/text()")
            If x1 IsNot Nothing Then
                CertContainer(i, 0) = x1.innerhtml      'Title
            End If
            If x2 IsNot Nothing Then
                CertContainer(i, 1) = x2.innerhtml
            End If
            If x3 IsNot Nothing Then
                CertContainer(i, 2) = LinkedInDateToSQLDate(x3.childnodes(0).innerhtml)
            End If
            If x4 IsNot Nothing Then
                CertContainer(i, 3) = LinkedInDateToSQLDate(x4.innertext)
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
        Next

Recommendations:
        Dim IsRecommendations As Boolean = True
        Dim RecommendationCount As Integer
        Try
            RecommendationCount = Document.documentnode.selectsinglenode("//*[@id=""recommendations""]/div/ul").childnodes.count
        Catch ex As Exception
            'No Recommendations
            IsRecommendations = False
            GoTo Courses
        End Try
        Dim RecommendationContainer(RecommendationCount - 1) As String
        For i = 0 To RecommendationCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""recommendations""]/div/ul")
            If x1 IsNot Nothing Then
                RecommendationContainer(i) = Document.documentnode.selectsinglenode("//*[@id=""recommendations""]/div/ul/li[" & i + 1 & "]/blockquote").innertext
            End If
            x1 = Nothing
        Next



Courses:

        Dim IsCourses As Boolean = True
        Dim SchoolCount As Integer
        Dim CourseCount As Integer
        Try
            SchoolCount = Document.documentnode.selectsinglenode("//*[@id=""courses""]/ul").childnodes.count
            Dim prevcc As Integer = 0
            For i = 1 To SchoolCount
                Try
                    CourseCount = Document.documentnode.selectsinglenode("//*[@id=""courses""]/ul/li[" & i & "]/div/ul").childnodes.count
                    If prevcc > CourseCount Then
                        CourseCount = prevcc
                    Else
                        prevcc = CourseCount
                    End If
                Catch ex As Exception
                    'No courses!
                End Try
            Next
        Catch ex As Exception
            'No Schools Or Courses!!
            IsCourses = False
            GoTo Awards
        End Try
        Dim CoursesContainer(SchoolCount - 1, CourseCount) As String
        For i = 0 To SchoolCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""courses""]/ul/li[" & i + 1 & "]/h4")
            x2 = Document.documentnode.selectsinglenode("//*[@id=""courses""]/ul/li[" & i + 1 & "]/div/ul")
            If x1 IsNot Nothing Then
                CoursesContainer(i, 0) = x1.innertext           'School
            End If
            If x2 IsNot Nothing Then
                For ii = 0 To CourseCount - 1
                    Try
                        CoursesContainer(i, ii + 1) = x2.childnodes(ii).childnodes(1).innertext     'Course
                    Catch ex As Exception
                    End Try
                Next
            End If

            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next


Awards:

        Dim IsAwards As Boolean = True
        Dim AwardCount As Integer
        Try
            AwardCount = Document.documentnode.selectsinglenode("//*[@id=""awards""]/ul").childnodes.count
        Catch ex As Exception
            'No Awards!
            IsAwards = False
            GoTo Scores
        End Try
        Dim AwardsContainer(AwardCount - 1, 3) As String
        For i = 0 To AwardCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""awards""]/ul/li[" & i + 1 & "]")
            x2 = Document.documentnode.selectsinglenode("//*[@id=""awards""]/ul/li[" & i + 1 & "]/header[1]/h4")
            x3 = Document.documentnode.selectsinglenode("//*[@id=""awards""]/ul/li[" & i + 1 & "]/header[1]/h5")
            If x2 IsNot Nothing Then
                AwardsContainer(i, 0) = x2.innerhtml    'title
            End If
            If x3 IsNot Nothing Then
                AwardsContainer(i, 1) = x3.innerhtml        'subtitle
            End If
            If x1 IsNot Nothing Then
                AwardsContainer(i, 2) = LinkedInDateToSQLDate(x1.childnodes(1).innertext)          'time 
                AwardsContainer(i, 3) = x1.childnodes(2).innertext          'Award description
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next
Scores:

        Dim IsScoring As Boolean = True
        Dim ScoreCount As Integer
        Try
            ScoreCount = Document.documentnode.selectsinglenode("//*[@id=""scores""]/ul").childnodes.count
        Catch ex As Exception
            'No Scores!
            IsScoring = False
            GoTo Groups
        End Try
        Dim ScoresContainer(ScoreCount - 1, 5) As String
        For i = 0 To ScoreCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""scores""]/ul/li[" & i + 1 & "]")
            If x1 IsNot Nothing Then
                ScoresContainer(i, 0) = x1.childnodes(0).Childnodes(0).innertext    'Exam
                ScoresContainer(i, 1) = x1.childnodes(0).childnodes(1).innertext    'Score
                ScoresContainer(i, 2) = LinkedInDateToSQLDate(x1.childnodes(1).innertext)                  'Time
                ScoresContainer(i, 3) = x1.childnodes(2).innertext                  'Description
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next
Groups:

        Dim IsGroups As Boolean = True
        Dim GroupCount As Integer
        Try
            GroupCount = Document.documentnode.selectsinglenode("//*[@id=""groups""]/ul").childnodes.count
        Catch ex As Exception
            'No Groups!
            IsGroups = False
            GoTo Organizations
        End Try
        Dim GroupsContainer(GroupCount - 1, 1) As String
        For i = 0 To GroupCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""groups""]/ul/li[" & i + 1 & "]/h4/a")
            If x1 IsNot Nothing Then
                GroupsContainer(i, 0) = x1.innertext                    'Group name
                Try
                    GroupsContainer(i, 1) = x1.attributes("href").value 'Group link
                Catch ex As Exception
                End Try
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next

Organizations:

        Dim IsOrganizations As Boolean = True
        Dim OrgCount As Integer
        Try
            OrgCount = Document.Documentnode.selectsinglenode("//*[@id=""organizations""]/ul").childnodes.count
        Catch ex As Exception
            'No orgs
            IsOrganizations = False
            GoTo Projects
        End Try
        Dim OrgContainer(OrgCount - 1, 5) As String
        For i = 0 To OrgCount - 1
            x1 = Document.Documentnode.selectsinglenode("//*[@id=""organizations""]/ul/li[" & i + 1 & "]")
            x2 = Document.Documentnode.selectsinglenode("//*[@id=""organizations""]/ul/li[" & i + 1 & "]/p")
            If x1 IsNot Nothing Then
                OrgContainer(i, 0) = x1.childnodes(0).Childnodes(0).innertext      'Org title
                OrgContainer(i, 1) = x1.childnodes(0).Childnodes(1).innertext       'Org position
                OrgContainer(i, 2) = x1.childnodes(1).innertext                     'Time check for jimmy
                If x2 IsNot Nothing Then
                    OrgContainer(i, 3) = x1.childnodes(2).innerhtml                 'Description
                End If
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next

Projects:
        Dim IsProjects As Boolean = True
        Dim ProjectCount As Integer
        Dim maxteamcount As Integer
        Dim Teammembcount() As Integer
        Try
            ProjectCount = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul").childnodes.count
            Dim prevtc As Integer = 0
            For i = 1 To ProjectCount
                Try
                    maxteamcount = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i & "]/d1/dd/ul").childnodes.count
                    Teammembcount(i) = maxteamcount
                    If prevtc > maxteamcount Then
                        maxteamcount = prevtc
                    Else
                        prevtc = maxteamcount
                    End If
                Catch ex As Exception
                    'No team for this project
                    Teammembcount(i) = 0
                End Try
            Next
        Catch ex As Exception
            'No projects
            IsProjects = False
            GoTo Skills
        End Try
        Dim ProjectContainer(ProjectCount, 4 + Teammembcount.Max) As String
        For i = 0 To ProjectCount - 1
            x1 = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i + 1 & "]/header/h4/a")
            x2 = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i + 1 & "]/div")
            x3 = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i + 1 & "]/p")
            x4 = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i + 1 & "]/d1/dd/ul")
            ' x5 = Document.documentnode.selectsinglenode("//*[@id=""projects""]/ul/li[" & i + 1 & "]")
            If x1 IsNot Nothing Then
                ProjectContainer(i, 0) = x1.innertext
            End If
            x1 = Nothing
            x2 = Nothing
            x3 = Nothing
            x4 = Nothing
            x5 = Nothing
        Next


Skills:
        Dim IsSkills As Boolean = True
        Dim SkillCount As Integer
        Try
            SkillCount = Document.documentnode.selectsinglenode("//*[@id=""skills""]/ul").childnodes.count
        Catch ex As Exception
            'No Skill haha
            IsSkills = False
            GoTo AlsoViewed
        End Try
        Dim SkillContainer(SkillCount - 2) As String
        For i = 0 To SkillCount - 2
            SkillContainer(i) = Document.documentnode.selectsinglenode("//*[@id=""skills""]/ul/li[" & i + 1 & "]").innertext
        Next

AlsoViewed:
        Dim AlsoViewedContainer(9) As String
        Try
            AlsoViewedContainer(0) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[1]").childnodes(0).attributes("href").value
            AlsoViewedContainer(1) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[2]").childnodes(0).attributes("href").value
            AlsoViewedContainer(2) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[3]").childnodes(0).attributes("href").value
            AlsoViewedContainer(3) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[4]").childnodes(0).attributes("href").value
            AlsoViewedContainer(4) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[5]").childnodes(0).attributes("href").value
            AlsoViewedContainer(5) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[6]").childnodes(0).attributes("href").value
            AlsoViewedContainer(6) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[7]").childnodes(0).attributes("href").value
            AlsoViewedContainer(7) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[8]").childnodes(0).attributes("href").value
            AlsoViewedContainer(8) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[9]").childnodes(0).attributes("href").value
            AlsoViewedContainer(9) = Document.documentnode.selectsinglenode("//*[@id=""aux""]/section[1]/div/ul/li[10]").childnodes(0).attributes("href").value
        Catch ex As Exception
            'Not really sure what to do about an error here. I don't think it ever happens...
        End Try

Done:



        'IMPORT THE DATA INTO SQL

        'Should update when i have a setting for the database
        Dim constring As String = "Data Source=LUCAS-PC\SQLEXPRESS;Initial Catalog=TwitterGuy;Integrated Security=True"
        Using con1 As New SqlClient.SqlConnection(constring)
            con1.Open()
            Dim cmd As New SqlClient.SqlCommand
            cmd.Connection = con1

StartSqlImport:
            'Check if there are enough columns to hold their data...
            ' There are 12 categories, 10 are variable. Headers, Experience, Education, languages, volunteering, courses, awards, exams, groups, organizations, suggestedpeople, skills
            Try
                cmd.CommandText = "Create table LinkedInUsers (link varchar(250), " & vbLf &
                "Name Varchar(100), PictureLink varchar(250), ConnectionsCount integer, ProfHeader varchar(250), " & vbLf &
                "Location Varchar(250), CurrentPosition varchar(250), CurrentPositionLink varchar(250), PreviousPosition varchar(250), PreviousPositionLink varchar(250), " & vbLf &
                "SuggLink1 varchar(100), SuggLink2 varchar(100), SuggLink3 varchar(100), SuggLink4 varchar(100), SuggLink5 varchar(100), " & vbLf &
                "SuggLink6 varchar(100), SuggLink7 varchar(100), SuggLink8 varchar(100), SuggLink9 varchar(100), SuggLink10 varchar(100), " & vbLf &
                ")"
            Catch ex As Exception
                'Table already made
            End Try

            If IsExperience Then
                For i = 1 To expcount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Exp" & i.ToString & "Title'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Exp" & i.ToString & "Title varchar(250), " & vbLf &
                            "Exp" & i.ToString & "Link varchar(250), Exp" & i.ToString & "Subtitle varchar(250), Exp" & i.ToString & "Logo varchar(250), Exp" & i.ToString & "Date1 date, Exp" & i.ToString & "Date2 date, " & vbLf &
                        "Exp" & i.ToString & "Location Varchar(250), Exp" & i.ToString & "Desc Varchar(5000)"
                        cmd.ExecuteNonQuery()
                    End If
                Next
            End If

            If IsEducation Then
                For i = 1 To Edcount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Ed" & i.ToString & "Title'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Ed" & i.ToString & "Title varchar(100), Ed" & i.ToString & "Link varchar(100), Ed" & i.ToString & "Degree varchar(100), " & vbLf &
                        "Ed" & i.ToString & "Date1 date, Ed" & i.ToString & "Date2 date, Ed" & i.ToString & "Desc varchar(1000)"
                    End If
                Next
            End If

            If IsLanguages Then
                For i = 1 To LangCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Lang" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Lang" & i.ToString & " varchar(250)"
                    End If
                Next
            End If
            'dk
            If IsVolunteering Then
                For i = 1 To VolunteerCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Volun" & i.ToString & "Position'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add 'Volun" & i.ToString & "Position varchar(250), Volun" & i.ToString & "Place varchar(250), Volun" & i.ToString & "Date1 date,  Volun" & i.ToString & "Date2 date"
                    End If
                Next
            End If

            If Iscauses Then
                For i = 1 To CauseCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Cause" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Cause" & i.ToString & " varchar(250)"
                    End If
                Next
            End If

            If IsCourses Then
                For i = 1 To SchoolCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'School" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add School" & i.ToString & " varchar(250)"
                    End If
                    For ii = 1 To CourseCount
                        cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Sch" & i.ToString & "Course" & ii.ToString & "'"
                        If cmd.ExecuteScalar Is Nothing Then
                            cmd.CommandText = "Alter table LinkedinUsers add Sch" & i.ToString & "Course" & ii.ToString & " varchar(250)"
                        End If
                    Next
                Next
            End If

            If IsAwards Then
                For i = 1 To AwardCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Award" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Award" & i.ToString & " varchar(500), AwardSubtitle" & i.ToString & " varchar(500), AwardDate" & i.ToString & " date, AwardDesc" & i.ToString & " varchar(1000)"
                    End If
                Next
            End If

            If IsScoring Then
                For i = 1 To ScoreCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Exam" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Exam" & i.ToString & " varchar(250), Score" & i.ToString & " varchar(100), ExamDate" & i.ToString & " date, ExamDesc" & i.ToString & " varchar(1000)"
                    End If
                Next
            End If

            If IsGroups Then
                For i = 1 To GroupCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Group" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Group" & i.ToString & " varchar(250), GroupLink" & i.ToString & " varchar(100)"
                    End If
                Next
            End If

            If IsOrganizations Then
                For i = 1 To OrgCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Org" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Org" & i.ToString & " varchar(100), OrgPosition" & i.ToString & " varchar(250), Org" & i.ToString & "Date1 date, Org" & i.ToString & "Date2 date, OrgDesc" & i.ToString & "varchar(1000)" ' org orgposition date1 date2 desc
                    End If
                Next
            End If

            If IsSkills Then
                For i = 1 To SkillCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Skill" & i.ToString & "'"
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "Alter table LinkedinUsers add Skill" & i.ToString & " varchar(100)"
                    End If
                Next
            End If

            ' project recommendation certification activity
            If IsProjects Then
                For i = 1 To ProjectCount
                    cmd.CommandText = "Select Column_Name From Twitterguy.Information_Schema.Columns where table_name = 'LinkedInUsers' and Column_Name = 'Project" & i.ToString & "'"
                    If cmd.ExecuteScalar Then
                        cmd.CommandText = "Alter table LinkedInUsers Add Project" & i.ToString & " varchar(500), "
                    End If
                Next
            End If



            Try

                Try
                    'Will Create a good start to the database. some people will have more or less than the allotted items.
                    cmd.CommandText = "Create Table LinkedInUsers" & vbLf &
                        "(Link Varchar(250) Primary Key, Name Varchar(100), PictureLink varchar(250), ConnectionsCount integer, ProfHeader varchar(250), " & vbLf &
                        "Location Varchar(250), CurrentPosition varchar(250), CurrentPositionLink varchar(250), PreviousPosition varchar(250), PreviousPositionLink varchar(250), " & vbLf &
                       "EdInstitution varchar(250), EdLink varchar(250), SummaryText varchar (5000), " & vbLf &
                        "Exp1Title varchar(250), Exp1Link varchar(250), Exp1Subtitle varchar(250), Exp1Logo varchar(250), Exp1Date1 date, Exp1Date2 date, " & vbLf &
                        "Exp1Location Varchar(250), Exp1Desc Varchar(5000), " & vbLf &
                        "Exp2Title varchar(250), Exp2Link varchar(250), Exp2Subtitle varchar(250), Exp2Logo varchar(250), Exp2Date1 date, Exp2Date2 date, " & vbLf &
                        "Exp2Location Varchar(250), Exp2Desc Varchar(5000), " & vbLf &
                        "Exp3Title varchar(250), Exp3Link varchar(250), Exp3Subtitle varchar(250), Exp3Logo varchar(250), Exp3Date1 date, Exp3Date2 date, " & vbLf &
                        "Exp3Location Varchar(250), Exp3Desc Varchar(5000), " & vbLf &
                        "Exp4Title varchar(250), Exp4Link varchar(250), Exp4Subtitle varchar(250), Exp4Logo varchar(250), Exp4Date1 date, Exp4Date2 date, " & vbLf &
                        "Exp4Location Varchar(250), Exp4Desc Varchar(5000), " & vbLf &
                        "Exp5Title varchar(250), Exp5Link varchar(250), Exp5Subtitle varchar(250), Exp5Logo varchar(250), Exp5Date1 date, Exp5Date2 date, " & vbLf &
                        "Exp5Location Varchar(250), Exp5Desc Varchar(5000), " & vbLf &
                        "Ed1Title varchar(100), Ed1Link varchar(100), Ed1Degree varchar(100), Ed1Date1 date, Ed1Date2 date, Ed1Desc varchar(1000), " & vbLf &
                        "Ed2Title varchar(100), Ed2Link varchar(100), Ed2Degree varchar(100), Ed2Date1 date, Ed2Date2 date, Ed2Desc varchar(1000), " & vbLf &
                        "Ed3Title varchar(100), Ed3Link varchar(100), Ed3Degree varchar(100), Ed3Date1 date, Ed3Date2 date, Ed3Desc varchar(1000), " & vbLf &
                        "Ed4Title varchar(100), Ed4Link varchar(100), Ed4Degree varchar(100), Ed4Date1 date, Ed4Date2 date, Ed4Desc varchar(1000), " & vbLf &
                        "Ed5Title varchar(100), Ed5Link varchar(100), Ed5Degree varchar(100), Ed5Date1 date, Ed5Date2 date, Ed5Desc varchar(1000), " & vbLf &
                        "Lang1 Varchar(100), Lang2 Varchar(100), Lang3 Varchar(100), Lang4 Varchar(100), Lang5 Varchar(100), " & vbLf &
                        "VolunPosition1 Varchar(250), VolunPosition2 Varchar(250), VolunPosition3 Varchar(250), VolunPosition4 Varchar(250), VolunPosition5 Varchar(250), " & vbLf &
                        "VolunPlace1 varchar(250), VolunPlace2 varchar(250), VolunPlace3 varchar(250), VolunPlace4 varchar(250), VolunPlace5 varchar(250), " & vbLf &
                        "VolunDate1 Varchar(100), VolunDate2 Varchar(100), VolunDate3 Varchar(100), VolunDate4 Varchar(100), VolunDate5 Varchar(100), " & vbLf &
                        "VolunCause1 varchar(250), VolunCause2 varchar(250), VolunCause3 varchar(250), VolunCause4 varchar(250), VolunCause5 varchar(250), " & vbLf &
                        "School1 varchar(100), School2 varchar(100), School3 varchar(100), School4 varchar(100), School5 varchar(100), " & vbLf &
                        "Sch1Course1 varchar(100), Sch1Course2 varchar(100), Sch1Course3 varchar(100), Sch1Course4 varchar(100), Sch1Course5 varchar(100), " & vbLf &
                        "Sch2Course1 varchar(100), Sch2Course2 varchar(100), Sch2Course3 varchar(100), Sch2Course4 varchar(100), Sch2Course5 varchar(100), " & vbLf &
                        "Sch3Course1 varchar(100), Sch3Course2 varchar(100), Sch3Course3 varchar(100), Sch3Course4 varchar(100), Sch3Course5 varchar(100), " & vbLf &
                        "Sch4Course1 varchar(100), Sch4Course2 varchar(100), Sch4Course3 varchar(100), Sch4Course4 varchar(100), Sch4Course5 varchar(100), " & vbLf &
                        "Sch5Course1 varchar(100), Sch5Course2 varchar(100), Sch5Course3 varchar(100), Sch5Course4 varchar(100), Sch5Course5 varchar(100), " & vbLf &
                        "Award1 varchar(100), Award1Subtitle varchar(100), Award1Date date, Award1Desc varchar(1000), Award2 varchar(100), Award2Subtitle varchar(100), Award2Date date, Award2Desc varchar(1000), Award3 varchar(100), " & vbLf &
                        "Award3Subtitle varchar(100), Award3Date date, Award3Desc varchar(1000), Award4 varchar(100), Award4Subtitle varchar(100), Award4Date date, Award4Desc varchar(1000), Award5 varchar(100), Award5Subtitle varchar(100), Award5Date date, Award5Desc varchar(1000), " & vbLf &
                        "Exam1 varchar(100), Exam2 varchar(100), Exam3 varchar(100), Exam4 varchar(100), Exam5 varchar(100), " & vbLf &
                        "Score1 varchar(25), Score2 varchar(25), Score3 varchar(25), Score4 varchar(25), Score5 varchar(25), " & vbLf &
                        "ExamTime1 date, ExamTime2 date, ExamTime3 date, ExamTime4 date, ExamTime5 date, " & vbLf &
                        "ExamDesc1 varchar(500), ExamDesc2 varchar(500), ExamDesc3 varchar(500), ExamDesc4 varchar(500), ExamDesc5 varchar(500), " & vbLf &
                        "Group1 varchar(100), Group1Link varchar(250), Group2 varchar(100), Group2Link varchar(250), Group3 varchar(100), " & vbLf &
                        "Group3Link varchar(250), Group4 varchar(100), Group4Link varchar(250), Group5 varchar(250), Group5Link varchar(100), " & vbLf &
                        "Org1 varchar(100), Org2 varchar(100), Org3 varchar(100), Org4 varchar(100), Org5 varchar(100), " & vbLf &
                        "Org1Position varchar(100), Org2Position varchar(100), Org3Position varchar(100), Org4Position varchar(100), Org5Position varchar(100), " & vbLf &
                        "Org1Date1 date, Org1Date2 date, Org2Date1 date, Org2Date2 date, Org3Date1 date, Org3Date2 date, Org4Date1 date, Org4Date2 date, Org5Date1 date, Org5Date2 date, " & vbLf &
                        "Org1Desc varchar(5000), Org2Desc varchar(5000), Org3Desc varchar(5000), Org4Desc varchar(5000), Org5Desc varchar(5000), " & vbLf &
                        "SuggLink1 varchar(100), SuggLink2 varchar(100), SuggLink3 varchar(100), SuggLink4 varchar(100), SuggLink5 varchar(100), " & vbLf &
                        "SuggLink6 varchar(100), SuggLink7 varchar(100), SuggLink8 varchar(100), SuggLink9 varchar(100), SuggLink10 varchar(100), " & vbLf &
                        "Skills1 varchar(100), Skills2 varchar(100), Skills3 varchar(100), Skills4 varchar(100), Skills5 varchar(100), " & vbLf &
                        "Skills6 varchar(100), Skills7 varchar(100), Skills8 varchar(100), Skills9 varchar(100), Skills10 varchar(100), " & vbLf &
                        "Skills11 varchar(100), Skills12 varchar(100), Skills13 varchar(100), Skills14 varchar(100), Skills15 varchar(100), " & vbLf &
                        "Skills16 varchar(100), Skills17 varchar(100), Skills18 varchar(100), Skills19 varchar(100), Skills20 varchar(100), LastUpdated datetime)"
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    'the table is already there ''do nothing
                End Try
                'Check if its already there
                cmd.CommandText = "Select * From LinkedInUsers where Link = '" & Link & "'"
                If cmd.ExecuteScalar Is Nothing Then

                    cmd.CommandText = "Insert into LinkedInUsers " &
                        "(Link, Name, PictureLink, ConnectionsCount, ProfHeader, Location, CurrentPosition, CurrentPositionLink, PreviousPosition, PreviousPositionLink, EdInstitution, EdLink, SummaryText, "
                    If IsExperience = True Then
                        For i = 1 To expcount
                            cmd.CommandText = cmd.CommandText & "Exp" & i.ToString & "Title, Exp" & i.ToString & "Link, Exp" & i.ToString & "Subtitle, Exp" & i.ToString & "Logo, Exp" & i.ToString & "Date1, Exp" & i.ToString & "Date2, Exp" & i.ToString & "Location, Exp" & i.ToString & "Desc, " & vbLf
                        Next
                    End If
                    If IsEducation = True Then
                        For i = 1 To Edcount
                            cmd.CommandText = cmd.CommandText & "Ed" & i.ToString & "Title, Ed" & i.ToString & "Link, Ed" & i.ToString & "Degree, Ed" & i.ToString & "Date1, Ed" & i.ToString & "Date2, Ed" & i.ToString & "Desc, " & vbLf
                        Next
                    End If
                    If IsLanguages = True Then
                        For i = 1 To LangCount
                            cmd.CommandText = cmd.CommandText & "Lang" & i.ToString & ", " & vbLf
                        Next
                    End If
                    If IsVolunteering Then
                        For i = 1 To VolunteerCount
                            cmd.CommandText = cmd.CommandText & "VolunPosition" & i.ToString & ", VolunPlace" & i.ToString & ", VolunDate" & i.ToString & ", VolunCause" & i.ToString & ", " & vbLf
                        Next
                    End If
                    If IsCourses Then
                        For i = 1 To SchoolCount
                            cmd.CommandText = cmd.CommandText & "School" & i.ToString & ", "
                            For ii = 1 To CourseCount
                                cmd.CommandText = cmd.CommandText & "Sch" & i.ToString & "Course" & ii.ToString & ", " & vbLf
                            Next
                        Next
                    End If
                    If IsAwards Then
                        For i = 1 To AwardCount
                            cmd.CommandText = cmd.CommandText & "Award" & i.ToString & ", Award" & i.ToString & "Subtitle, Award" & i.ToString & "Date, Award" & i.ToString & "Desc, " & vbLf
                        Next
                    End If
                    If IsScoring Then
                        For i = 1 To ScoreCount
                            cmd.CommandText = cmd.CommandText & "Exam" & i.ToString & ", Score" & i.ToString & ", ExamTime" & i.ToString & ", ExamDesc" & i.ToString & ", " & vbLf
                        Next
                    End If
                    If IsGroups Then
                        For i = 1 To GroupCount
                            cmd.CommandText = cmd.CommandText & "Group" & i.ToString & ", Group" & i.ToString & "Link, " & vbLf
                        Next
                    End If
                    If IsOrganizations Then
                        For i = 1 To OrgCount
                            cmd.CommandText = cmd.CommandText & "Org" & i.ToString & ", Org" & i.ToString & "Position, Org" & i.ToString & "Date1, Org" & i.ToString & "Date2, Org" & i.ToString & "Desc, " & vbLf
                        Next
                    End If
                    For i = 1 To 10
                        cmd.CommandText = cmd.CommandText & "SuggLink" & i.ToString & ", " & vbLf
                    Next
                    If IsSkills Then
                        For i = 1 To SkillCount - 1
                            cmd.CommandText = cmd.CommandText & "Skills" & i.ToString & ", " & vbLf
                        Next
                    End If
                    cmd.CommandText = cmd.CommandText & "LastUpdated)" & vbLf

                    'Insert End
                    'Values Start

                    cmd.CommandText = cmd.CommandText & "Values ('" & Link & "', '" & Name & "', '" & Pic & "', '" & Connectionscount & "', '" & ProfHeader & "', '" & Location & "', '" & CurrentPositionName & "', '" & CurrentPositionLink & "', '" & PreviousPositionName & "', '" & PreviousPositionLink & "', '" & EducationName & "', '" & EducationLink & "', '" & SummaryText & "', " & vbLf
                    If IsExperience = True Then
                        For i = 0 To expcount - 1
                            Dim Datey1 As DateTime
                            Dim Datey2 As DateTime
                            If ExperienceContainer(i, 5).Contains("Get") = False Then
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(ExperienceContainer(i, 5))
                            Else
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(Today.Month & "/" & Today.Day & "/" & Today.Year)
                            End If
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(ExperienceContainer(i, 4))

                            cmd.CommandText = cmd.CommandText & "'" & ExperienceContainer(i, 0) & "', '" & ExperienceContainer(i, 1) & "', '" & ExperienceContainer(i, 2) & "', '" & ExperienceContainer(i, 3) & "', '" & Datey1 & "', '" & Datey2 & "', '" & ExperienceContainer(i, 6) & "', '" & ExperienceContainer(i, 7) & "', " & vbLf
                        Next
                    End If
                    If IsEducation = True Then
                        For i = 0 To Edcount - 1
                            Dim Datey1 As DateTime
                            Dim Datey2 As DateTime
                            If EducationContainer(i, 4).Contains("Get") = False Then
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(EducationContainer(i, 4))
                            Else
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(Today.Month & "/" & Today.Day & "/" & Today.Year)
                            End If
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(EducationContainer(i, 3))

                            cmd.CommandText = cmd.CommandText & "'" & EducationContainer(i, 0) & "', '" & EducationContainer(i, 1) & "', '" & EducationContainer(i, 2) & "', '" & Datey1 & "', '" & Datey2 & "', '" & EducationContainer(i, 5) & "', " & vbLf
                        Next
                    End If
                    If IsLanguages = True Then
                        For i = 0 To LangCount - 1
                            cmd.CommandText = cmd.CommandText & "'" & LanguagesContainer(i, 0) & "', " & vbLf
                        Next
                    End If
                    If IsVolunteering Then
                        For i = 0 To VolunteerCount - 1
                            Dim Datey1 As DateTime
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(VolunteeringContainer(i, 2))
                            cmd.CommandText = cmd.CommandText & "'" & VolunteeringContainer(i, 0) & "', '" & VolunteeringContainer(i, 1) & "', '" & Datey1 & "', '" & VolunteeringContainer(i, 3) & "', " & vbLf
                        Next
                    End If
                    If IsCourses Then
                        For i = 0 To SchoolCount - 1
                            cmd.CommandText = cmd.CommandText & "'" & CoursesContainer(i, 0) & "', "
                            For ii = 1 To CourseCount
                                cmd.CommandText = cmd.CommandText & "'" & CoursesContainer(i, ii) & "', " & vbLf
                            Next
                        Next
                    End If
                    If IsAwards Then
                        For i = 0 To AwardCount - 1
                            Dim Datey1 As DateTime
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(AwardsContainer(i, 2))
                            cmd.CommandText = cmd.CommandText & "'" & AwardsContainer(i, 0) & "', '" & AwardsContainer(i, 1) & "', '" & Datey1 & "', '" & AwardsContainer(i, 3) & "', " & vbLf
                        Next
                    End If
                    If IsScoring Then
                        For i = 0 To ScoreCount - 1
                            Dim Datey1 As DateTime
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(ScoresContainer(i, 2))
                            cmd.CommandText = cmd.CommandText & "'" & ScoresContainer(i, 0) & "', '" & ScoresContainer(i, 1) & "', '" & Datey1 & "', '" & ScoresContainer(i, 3) & "', " & vbLf
                        Next
                    End If
                    If IsGroups Then
                        For i = 0 To GroupCount - 1
                            cmd.CommandText = cmd.CommandText & "'" & GroupsContainer(i, 0) & "', '" & GroupsContainer(i, 1) & "', " & vbLf
                        Next
                    End If
                    If IsOrganizations Then
                        For i = 0 To OrgCount - 1
                            Dim Datey1 As DateTime
                            Dim Datey2 As DateTime
                            If OrgContainer(i, 3).Contains("Get") = False Then
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(OrgContainer(i, 3))
                            Else
                                Datey2 = SqlTypes.SqlDateTime.op_Implicit(Today.Month & "/" & Today.Day & "/" & Today.Year)
                            End If
                            Datey1 = SqlTypes.SqlDateTime.op_Implicit(OrgContainer(i, 2))

                            cmd.CommandText = cmd.CommandText & "'" & OrgContainer(i, 0) & "', '" & OrgContainer(i, 1) & "', '" & Datey1 & "', '" & Datey2 & "', '" & OrgContainer(i, 4) & "', " & vbLf
                        Next
                    End If
                    For i = 0 To 9
                        cmd.CommandText = cmd.CommandText & "'" & AlsoViewedContainer(i) & "', " & vbLf
                    Next
                    If IsSkills Then
                        For i = 0 To SkillCount - 2
                            cmd.CommandText = cmd.CommandText & "'" & SkillContainer(i) & "', " & vbLf
                        Next
                    End If

                    'Values End

                    cmd.CommandText = cmd.CommandText & "Getdate())"


                    cmd.ExecuteNonQuery()
                Else
                    'Update the values
                    cmd.CommandText = "Update "
                    cmd.ExecuteNonQuery()
                End If
            Catch ex As Exception
                MsgBox("Problem Inserting Profile Into SQL")
            End Try


        End Using

    End Sub
    Public Function LinkedInDateToSQLDate(Datey As String)
        'November 2016  –  Heute (7 Monate)
        If Datey.Contains("Present") Then
            Return "Getdate()"
            Exit Function
        End If
        Dim year
        Dim Datey1 = Split(Datey, " ")
        Try
            year = Datey1(1)
        Catch ex As Exception
            Return "01/01/" & Datey
        End Try
        Dim Month = Datey1(0)
        Dim Monthnum As String
        Dim Convertedstring As String
        Select Case Month
            Case "January"
                Monthnum = "01"
            Case "February"
                Monthnum = "02"
            Case "March"
                Monthnum = "03"
            Case "April"
                Monthnum = "04"
            Case "May"
                Monthnum = "05"
            Case "June"
                Monthnum = "06"
            Case "July"
                Monthnum = "07"
            Case "August"
                Monthnum = "08"
            Case "September"
                Monthnum = "09"
            Case "October"
                Monthnum = "10"
            Case "November"
                Monthnum = "11"
            Case "December"
                Monthnum = "12"
            Case Else
                Monthnum = 0
        End Select
        Convertedstring = Monthnum & "/" & "01" & "/" & year 'Not sure what to do for the day
        Return Convertedstring
    End Function
End Module
