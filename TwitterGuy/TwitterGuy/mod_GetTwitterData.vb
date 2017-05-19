
Imports System.Net
Imports Newtonsoft
Module mod_GetTwitterData
    Public Sub GetTweetData(User1)
        Dim User As String = User1
        Dim Address As String = "https://www.twitter.com/" & User
        Dim HtmlDoc = New HtmlAgilityPack.HtmlDocument()
        Using WebClient = New CookieAwareWebClient
            Dim stream = WebClient.OpenRead(Address)
            HtmlDoc.Load(stream)

            ' for future reference
            'Take Private users into account when looking directly at users
            'https://twitter.com/i/activity/retweeted_popup?id= & itemID will return a json of who retweeted a tweet
            'https://twitter.com/i/activity/favorited_popup?id= & itemid will return a json of who favorited 

            'Getting UserData By Converting to string
            'Should convert to using json format

            Dim DocAsString As String = HtmlDoc.DocumentNode.InnerHtml.ToString
            Dim x1
            Dim website As String = ""
            x1 = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""page-container""]/div[2]/div/div/div[1]/div/div/div/div[1]/div[2]/span[2]/a")
            If x1 IsNot Nothing Then
                Website = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""page-container""]/div[2]/div/div/div[1]/div/div/div/div[1]/div[2]/span[2]/a").Attributes("href").Value
            End If
            x1 = Nothing
            Dim MediaCountstring As String = ""
            x1 = HtmlDoc.DocumentNode.SelectSingleNode("//*[@id=""page-container""]/div[2]/div/div/div[1]/div/div/div/div[2]/div[1]/span[2]/a[1]")
            If x1 IsNot Nothing Then
                MediaCountstring = x1.innertext
            End If



            ''Gets the json data for user
            Dim g As String = Split(Split(DocAsString, "quot;OPPO_emojiv4\/OPPO_emojiv4.png&quot;},")(1), """>")(0)
            Dim ID = JSonGetter(g, "id")
            Dim Name = JSonGetter(g, "name")
            User = JSonGetter(g, "screen_name")
            Dim ProfilePic = JSonGetter(g, "profile_image_url_https")
            Dim FollowingCount As Integer = Integer.Parse(JSonGetter(g, "friends_count"))
            Dim FollowerCount As Integer = Integer.Parse(JSonGetter(g, "followers_count"))
            Dim ListedCount As Integer = Integer.Parse(JSonGetter(g, "listed_count"))
            Dim WhenCreated As DateTime = JSonGetter(g, "created_at", True)
            Dim LikeCount As Integer = Integer.Parse(JSonGetter(g, "favourites_count"))
            Dim TweetCount As Integer = Integer.Parse(JSonGetter(g, "statuses_count"))
            Dim Bio As String = Split(Split(g, "description&quot;:&quot;")(1), "&quot;,&quot;protected")(0) ' fix this fix all of these
            Dim Location As String = JSonGetter(g, "location")
            Dim BackGroundPic As String = JSonGetter(g, "profile_background_image_url_https")
            Dim TimeZone As String = JSonGetter(g, "time_zone")
            Dim Verified As String = JSonGetter(g, "verified")
            Dim GeoEnabled As String = JSonGetter(g, "geo_enabled")
            If Verified = "true" Then
                Verified = "1"
            Else
                Verified = "0"
            End If
            If GeoEnabled = True Then
                GeoEnabled = "1"
            Else
                GeoEnabled = "0"
            End If

            Dim constring As String = "Data Source=LUCAS-PC\SQLEXPRESS;Initial Catalog=TwitterGuy;Integrated Security=True"
            Using con1 As New SqlClient.SqlConnection(constring)
                con1.Open()
                Dim cmd As New SqlClient.SqlCommand
                cmd.Connection = con1
                Try
                    Try
                        cmd.CommandText = "Create Table TwitterUsers
                    (Name Varchar(100), Handle varchar(100), TwitterID Varchar(29) Primary Key, DateCreated Varchar(50), Bio Varchar(255), ProfilePic Varchar(255), Website varchar(255), TweetCount int, FaveCount int, 
                       FollowerCount int, FollowingCount int, Location Varchar(100), BackgroundPic varchar(255), TimeZone Varchar(30), GeoEnabled bit, Verified bit, Updated Datetime)"
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        'the table is already there ''do nothing
                    End Try
                    cmd.CommandText = "select * From Twitterusers where TwitterID = " & ID
                    If cmd.ExecuteScalar Is Nothing Then
                        cmd.CommandText = "insert into TwitterUsers (Name, Handle, TwitterID, DateCreated, Bio, ProfilePic, Website, TweetCount, FaveCount, FollowerCount, FollowingCount, Location, BackgroundPic, Timezone, GeoEnabled, Verified, Updated) 
                Values ('" & Name & "', '" & User & "', '" & ID & "', '" & WhenCreated & "', '" & Bio & "', '" & ProfilePic & "', '" & website & "', " & TweetCount & ", " & LikeCount & ", " & FollowerCount & ", " & FollowingCount & ", '" & Location & "',
                         '" & BackGroundPic & "', '" & TimeZone & "', " & GeoEnabled & ", " & Verified & ", Getdate())"
                        cmd.ExecuteNonQuery()
                    Else
                        'Update the values
                        cmd.CommandText = "Update Twitterusers set Name = '" & Name & "', Handle = '" & User & "', TwitterID = '" & ID & "', DateCreated = '" & WhenCreated & "', Bio = '" & Bio & "', ProfilePic = '" & ProfilePic & "', TweetCount = " & TweetCount & ", FaveCount = " & LikeCount & ", FollowerCount = " & FollowerCount & ", FollowingCount = " & FollowingCount & ", Location = '" & Location & "'," &
                        "BackgroundPic = '" & BackGroundPic & "', TimeZone = '" & TimeZone & "', GeoEnabled = " & GeoEnabled & ", Verified = " & Verified & ", Website = '" & website & "', Updated = Getdate() where TwitterID = '" & ID & "'"
                        cmd.ExecuteNonQuery()
                    End If
                Catch ex As Exception
                    MsgBox("Problem Inserting Profile Into SQL")
                End Try


                Dim stoppy As Boolean = False
                Dim tweetstring As String = ""
                Dim cntr As Integer = 0
                Dim position As String = ""
                Dim maxy As Int64
                Do Until stoppy = True
                    Dim container1 = DownloadTweetToString(Address, User, position, maxy, cntr)
                    tweetstring = container1(0)
                    position = container1(1)
                    maxy = container1(2)

                    If tweetstring.Length > 600 Then
                        Try
                            PullTweetData(tweetstring)
                        Catch ex As Exception
                            If MsgBox("WTF" & vbLf & ex.ToString & vbLf & "Exit Download?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Exit Sub
                        End Try
                        'Update the progressbar

                        'Dim t As Integer = Int((maxy - Int64.Parse(position)) / maxy) * 100
                        'Threading.Thread.Sleep(60000)


                        cntr = cntr + 1
                    Else
                        MsgBox("Scrape done")
                        Exit Sub 'why are there errors here?
                    End If
                Loop
            End Using
        End Using

    End Sub
    Public Function DownloadTweetToString(address, user, position, maxy, cntr)

        address = "https://twitter.com/i/search/timeline?vertical=default&q=from%3A" & user & "&src=typd&include_available_features=1&include_entities=1&max_position=TWEET-" & position & "-" & maxy.ToString & "-&oldest_unread_id=0&reset_error_state=false"
        Using webclient = New System.Net.WebClient
            Try
                webclient.DownloadFile(address, "C:\Users\Lucas\Desktop\Tweet.txt")

            Catch ex As Exception
                MsgBox("faiiiiilll")
                Exit Function
            End Try
        End Using

        Dim Txtreader = My.Computer.FileSystem.OpenTextFileReader("C:\Users\Lucas\Desktop\Tweet.txt")
        Dim TweetString As String = Txtreader.ReadToEnd
        Txtreader.Close()

        If cntr = 0 Then
            Dim lowerPosition As Int64 = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(0))
            maxy = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(1))

            position = (maxy - (maxy - lowerPosition)).ToString
        Else
            Dim TempPosition As Int64 = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(0))
            position = (Int64.Parse(position) - (Int64.Parse(position) - TempPosition)).ToString

        End If

        Dim container1(2)
        container1(0) = TweetString
        container1(1) = position
        container1(2) = maxy

        Return container1
    End Function
    Function FindInteger(ByVal str As String)
        Dim i As Int64 = 1
        Dim fail As Boolean = False
        Do Until fail = True
            Try
                Dim tester = Left(str, i)
                Int64.Parse(tester)
                i += 1
            Catch ex As Exception
                fail = True
                i -= 1
            End Try
        Loop
        Return Int64.Parse(Left(str, i))
    End Function
    'New function for converting date
    Function RemoveChar(ByVal Text As String, ByVal ThingToRemove As String)

        Dim CleanedLink As String
        CleanedLink = Text.Replace(ThingToRemove, "")

        Return CleanedLink
    End Function
    Public Sub PullTweetData(ByVal UnparsedTweet As String)
        UnparsedTweet = Replace(UnparsedTweet, "\n", vbLf)
        UnparsedTweet = Replace(UnparsedTweet, "\u003c", "<")
        UnparsedTweet = Replace(UnparsedTweet, "\u003e", ">")
        UnparsedTweet = Replace(UnparsedTweet, "\""", """")
        UnparsedTweet = Replace(UnparsedTweet, "\/", "/")
        UnparsedTweet = Replace(UnparsedTweet, "\u007c", "{")
        UnparsedTweet = Replace(UnparsedTweet, "\u007d", "}")

        'it is in html format now

        Dim htmldoc = New HtmlAgilityPack.HtmlDocument
        htmldoc.LoadHtml(UnparsedTweet)
        Dim docasstring = htmldoc.DocumentNode.InnerHtml.ToString
        Dim tweetsnodes = htmldoc.DocumentNode.ChildNodes
        Dim i As Integer = 0
        Dim constring As String = "Data Source=LUCAS-PC\SQLEXPRESS;Initial Catalog=TwitterGuy;Integrated Security=True"
        Dim StopAt As Integer = ((tweetsnodes.Count - 1) / 2)
        Using con1 As New SqlClient.SqlConnection(constring)
            con1.Open()
            Dim cmd As New SqlClient.SqlCommand
            cmd.Connection = con1
            tweetsnodes.Item(tweetsnodes.Count - 1).Remove()

            Do
                If tweetsnodes.Item(i).Name.Contains("text") Then
                    tweetsnodes.Item(i).Remove()
                    i = i - 1
                Else
                    Dim Mentions1 = ""
                    Dim Mentions2 = ""
                    Dim Mentions3 = ""
                    Dim Mentions4 = ""
                    Dim Mentions5 = ""
                    Dim TweetID As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-tweet-id").Value
                    Dim ItemID As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-item-id").Value
                    Dim ItemLink As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-permalink-path").Value
                    Dim ConversationID As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-Conversation-id").Value
                    Dim UserID As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-user-id").Value
                    'absolute trash. fix when possible
                    'Dim jj As Json.Linq.JObject = Json.JsonConvert.DeserializeObject(Replace(Replace(Replace((Replace(Replace((tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-reply-to-users-json").Value), "&quot;", """"), "\/", "/")), "\u007b", "{"), ":", ": "), ",", ", ").TrimEnd("]}}").TrimStart("[{{"))
                    'Dim ISReply As Integer = 0
                    'If jj.Count > 4 Then ISReply = 1
                    Dim ComponentContext As String = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-component-context").Value
                    Try
                        Dim Mentions = tweetsnodes.Item(i).ChildNodes(1).Attributes.Item("data-mentions").Value
                        Mentions1 = Split(Mentions, " ")(0)
                        Mentions2 = Split(Mentions, " ")(1)
                        Mentions3 = Split(Mentions, " ")(2)
                        Mentions4 = Split(Mentions, " ")(3)
                        Mentions5 = Split(Mentions, " ")(4)
                    Catch ex As Exception
                        'No more mentions!
                    End Try

                    Dim TweetText = htmldoc.DocumentNode.SelectNodes("//div[@class=""js-tweet-text-container""]")(i).ChildNodes(1).InnerHtml 'things to deal with are mentions, videos, pictures, links, emojis, hashtags, polls?, 
                    TweetText = Replace(Replace(TweetText, "<img class=""Emoji Emoji--forText"" src=""https://abs.twimg.com/emoji/", ""), "class=""twitter-atreply pretty-link js-nav"" dir=""ltr"" data-mentioned-user-id=", "")
                    Dim timeclass As String = (htmldoc.DocumentNode.SelectNodes("//small[@class=""time""]")(i).ChildNodes(1).Attributes("title").Value)
                    Dim dates = Split(timeclass, " ")
                    Dim DateTweeted As DateTime = translatedate(dates(4)) & "/" & dates(3) & "/" & dates(5) & " " & dates(0) & dates(1)
                    DateTweeted = SqlTypes.SqlDateTime.op_Implicit(DateTweeted)
                    Dim statcontainer
                    Dim Replies As Int64
                    Dim Retweets As Int64
                    Dim Likes As Int64
                    statcontainer = htmldoc.DocumentNode.SelectNodes("//div[@class=""stream-item-footer""]")(i).ChildNodes(1) 'each has 3, reply retweet and like
                    Replies = statcontainer.ChildNodes(1).ChildNodes(1).Attributes("data-tweet-stat-count").Value
                    Retweets = statcontainer.ChildNodes(3).ChildNodes(1).Attributes("data-tweet-stat-count").Value
                    Likes = statcontainer.ChildNodes(5).ChildNodes(1).Attributes("data-tweet-stat-count").Value
                    'If has a quote
                    Dim QuotedTweeterID As String = ""
                    Dim QuotedTweetID As String = ""
                    Try
                        Dim x = tweetsnodes.Item(i).SelectNodes("//div[@class=""QuoteTweet-container""]")

                        Dim yy As Integer = 0
                        For yy = 0 To x.Count - 1
                            'back to my old terrible ways
                            Dim wherea As String = Split(Split(x(yy).XPath, "[")(1), "]")(0)
                            If wherea = i.ToString Then
                                QuotedTweetID = x.Item(yy).ChildNodes(3).Attributes(1).Value
                                QuotedTweeterID = x.Item(yy).ChildNodes(3).Attributes(4).Value
                            End If
                        Next
                    Catch ex As Exception
                        'No quotes
                    End Try

                    Try
                        Try
                            cmd.CommandText = "Create Table Tweets
                        (TweetID Varchar(29), ItemID varchar(29) Primary Key, ItemLink Varchar(255), DateTweeted DateTime, ConversationID Varchar(29), TweeterID Varchar(29), 
                           ComponentContext Varchar(100), Tweettext Varchar(5000), Replies Int, Retweets Int, Likes Int, Mention1 Varchar(100), Mention2 Varchar(100), Mention3 varchar(100), Mention4 Varchar(100), Mention5 Varchar(100), QuotedTweeterID Varchar(29), QuotedTweetID Varchar(29), Updated Datetime)"
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            'the table is already there ''do nothing
                        End Try
                        cmd.CommandText = "select * From Tweets where ItemID = '" & ItemID.ToString & "'"
                        If cmd.ExecuteScalar Is Nothing Then
                            'What to do with mentions...
                            cmd.CommandText = "insert into Tweets (TweetID, ItemID, ItemLink, DateTweeted, ConversationID, TweeterID, ComponentContext, TweetText, Replies, Retweets, Likes, Mention1, Mention2, Mention3, Mention4, Mention5, QuotedTweeterID, QuotedTweetID, Updated) 
                    Values ('" & TweetID & "', '" & ItemID & "', '" & ItemLink & "', '" & DateTweeted & "', '" & ConversationID & "', '" & UserID & "', '" & ComponentContext & "', '" & TweetText & "', " & Replies & ", " & Retweets & ", " & Likes & ", '" & Mentions1 & "',
'" & Mentions2 & "', '" & Mentions3 & "', '" & Mentions4 & "', '" & Mentions5 & "', '" & QuotedTweeterID & "', '" & QuotedTweetID & "', Getdate())"
                            cmd.ExecuteNonQuery()
                        Else
                            'Update the values
                            cmd.CommandText = "Update Tweets set TweetID = '" & TweetID & "', ItemID = '" & ItemID & "', ItemLink = '" & ItemLink & "', DateTweeted = '" & DateTweeted & "', ConversationID = '" & ConversationID & "', TweeterID = '" & UserID & "', ComponentContext = '" & ComponentContext & "', TweetText = '" & TweetText & "', Replies = " & Replies & ", Retweets = " & Retweets & ", Likes = " & Likes & ", Mention1 = '" & Mentions1 & "'," &
                            "Mention2 = '" & Mentions2 & "', Mention3 = '" & Mentions3 & "', Mention4 = '" & Mentions4 & "', Mention5 = '" & Mentions5 & "', QuotedTweeterID = '" & QuotedTweeterID & "', QuotedTweetID = '" & QuotedTweetID & "', Updated = Getdate() where ItemID = '" & ItemID & "'"
                            cmd.ExecuteNonQuery()
                        End If
                    Catch ex As Exception
                        MsgBox("Problem Inserting into SQL" & ex.ToString)
                    End Try
                End If
                i = i + 1
            Loop Until i = StopAt
        End Using
    End Sub

    Function JSonGetter(ByVal jsonstring As String, ByVal ValueWanted As String, Optional ByVal Datey As Boolean = False)

        Dim f As String = (Replace(Replace(jsonstring, "&quot;", """"), "\/", "/"))

        Dim value As String = Split(Split(f, ValueWanted & """:")(1), ",")(0)
        If value.Contains("""") Then
            value = Replace(value, """", "")
        End If
        If Datey = True Then
            Dim datey1 As DateTime
            Dim r = Split(value, " ")
            Dim day = r(2)
            Dim month = translatedate(r(1))
            Dim time = r(3)
            Dim year = r(5)
            datey1 = month & "/" & day & "/" & year & " " & time
            Return datey1
            Exit Function
        End If

        Return value
    End Function
    Function translatedate(ByVal baddate As String)
        Dim monthnum As Integer
        Select Case baddate
            Case "Jan"
                monthnum = 1
            Case "Feb"
                monthnum = 2
            Case "Mar"
                monthnum = 3
            Case "Apr"
                monthnum = 4
            Case "May"
                monthnum = 5
            Case "Jun"
                monthnum = 6
            Case "Jul"
                monthnum = 7
            Case "Aug"
                monthnum = 8
            Case "Sep"
                monthnum = 9
            Case "Oct"
                monthnum = 10
            Case "Nov"
                monthnum = 11
            Case "Dec"
                monthnum = 12
            Case Else
                monthnum = 0
        End Select
        Return monthnum
    End Function
    Public Sub Updatestats(user1)

        Dim User As String = user1
        Dim Address As String = "https://www.twitter.com/" & User
        Dim HtmlDoc = New HtmlAgilityPack.HtmlDocument()
        Using WebClient = New CookieAwareWebClient
            Dim stream
            Try
                stream = WebClient.OpenRead(Address)
            Catch ex As Exception
                MsgBox("Page does not exist or internet connection not established. Try again soon")
                Exit Sub
            End Try
            HtmlDoc.Load(stream)
            Dim DocAsString As String = HtmlDoc.DocumentNode.InnerHtml.ToString

            ''Gets the json data for user
            Dim g As String = Split(Split(DocAsString, "quot;OPPO_emojiv4\/OPPO_emojiv4.png&quot;},")(1), """>")(0)
            Dim ID = JSonGetter(g, "id")
            Dim Name = JSonGetter(g, "name")
            User = JSonGetter(g, "screen_name")
            frmMain.tb_Twitter.Text = User
            Dim ProfilePic = JSonGetter(g, "profile_image_url_https")
            Dim FollowingCount As Integer = Integer.Parse(JSonGetter(g, "friends_count"))
            Dim FollowerCount As Integer = Integer.Parse(JSonGetter(g, "followers_count"))
            Dim ListedCount As Integer = Integer.Parse(JSonGetter(g, "listed_count"))
            Dim LikeCount As Integer = Integer.Parse(JSonGetter(g, "favourites_count"))
            Dim TweetCount As Integer = Integer.Parse(JSonGetter(g, "statuses_count"))
            Dim Bio As String = Split(Split(g, "description&quot;:&quot;")(1), "&quot;,&quot;protected")(0) ' fix this fix all of these
            Dim Location As String = JSonGetter(g, "location")
            Dim BackGroundPic As String = JSonGetter(g, "profile_background_image_url_https")
            With frmMain
                .pbProfilePic.Load(ProfilePic)
                .pbProfilePic.BackgroundImage = frmMain.pbProfilePic.Image
                .pbProfilePic.BackgroundImageLayout = ImageLayout.Stretch
                .pbProfilePic.Image = Nothing
                .lblProfilePic.Text = Name
                .lblFollowersCount.Text = FollowerCount.ToString
                .lblLikesCount.Text = LikeCount.ToString
                .lblTweetsCount.Text = TweetCount.ToString
                .lblFollowingCount.Text = FollowingCount.ToString
            End With
        End Using
    End Sub
    Public Sub ConversationInfo(ConversationID)
        'Get the conversation info of a certain tweet, or every tweet that has replies...

    End Sub

End Module
