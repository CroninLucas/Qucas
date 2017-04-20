Module mod_GetData
    Public position As String
    Public TweetList(5000)
    Public Address As String
    Public User As String
    Public cntr As Integer = 0
    Dim maxy As Int64
    Public Sub GetTweetData(User1)
        'User is the @blahblah without the @
        User = User1
        Address = "https://www.twitter.com/" & User
        ' /with-replies if i want replies 'doesnt work with replies
        Dim HtmlDoc = New HtmlAgilityPack.HtmlDocument()
        Using WebClient = New CookieAwareWebClient
            Dim stream = WebClient.OpenRead(Address)
            HtmlDoc.Load(stream)


            'Getting UserData By Converting to string

            Dim DocAsString As String
            DocAsString = HtmlDoc.DocumentNode.InnerHtml.ToString
            Dim temp() = Split(DocAsString, "followers_count&quot;:", 2)
            Dim Name = RemoveChar(Split(Split(DocAsString, "fullname ProfileNameTruncated-link u-textInheritColor js-nav"" href=""/" & User & """ data-aria-label-part="""">" & vbLf)(1), "</a></div><span class=""UserBadges"">")(0), " ")
            Dim ProfilePic As String = RemoveChar(Split(Split(DocAsString, "profile_image_url_https&quot;:&quot;")(1), "&quot;,&quot;profile_banner_url&quot;:&")(0), "\")
            Dim FollowerCount As Integer = FindInteger(temp(1))
            Dim FollowingCount As Integer = FindInteger(Split(temp(1), "friends_count&quot;:")(1))
            Dim ListedCount As Integer = FindInteger(Split(temp(1), "&quot;listed_count&quot;:")(1)) 'not sure what this is or why i added it
            Dim WhenCreated As String = Left(Split(temp(1), "created_at&quot;:&quot;")(1), 30)
            Dim LikeCount As Integer = FindInteger(Split(temp(1), "favourites_count&quot;:")(1))
            Dim TweetCount As Integer = FindInteger(Split(temp(1), "&quot;statuses_count&quot;:")(1))
            Dim ID As Integer = FindInteger(Split(temp(1), "quot;profile_id&quot;:")(1))
            Dim Bio As String = Split(Split(temp(1), "<p class=""ProfileHeaderCard-bio u-dir"" dir=""ltr"">")(1), "</p>")(0)
            Dim Location As String = Split(Split(DocAsString, "location&quot;:&quot;")(1), "&quot;,&quot;url&quot;")(0)
            Dim BackGroundPic As String = RemoveChar(Split(Split(DocAsString, "profile_background_image_url_https&quot;:&quot;")(1), "&quot;,&quot;profile_background_tile&quot")(0), "\")
            Dim TimeZone As String = Split(Split(DocAsString, "time_zone&quot;:")(1), ",&quot;geo_enabled&quot")(0)
            Dim Verified As String = Split(Split(DocAsString, ",&quot;verified&quot;:")(1), ",&quot;statuses_count&quot;")(0)
            Dim GeoEnabled As String = Split(Split(DocAsString, "geo_enabled&quot;:")(1), ",&quot;verified&quot;:")(0)
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

            'Grabbing 20 tweets
            'Dim Tweets As HtmlAgilityPack.HtmlNodeCollection
            'Tweets = HtmlDoc.DocumentNode.SelectNodes(" // div[@Class=""js-tweet-text-container""]")
            'Dim TweetContainer = HtmlDoc.DocumentNode.SelectSingleNode("//div[@Class=""ProfileTimeline ""]")
            'position = Split(Split(TweetContainer.InnerHtml.ToString, "max-position=""")(1), """")(0)


            TweetList(0) = "Not nothign haha"
            Dim stoppy As Boolean = False
            Dim tweetstring As String = ""
            Do Until stoppy = True
                tweetstring = DownloadTweetToString()
                If tweetstring.Length > 600 Then
                    Try
                        ParseData(tweetstring)
                    Catch ex As Exception ' will cause an exception on last download
                        MsgBox("errrroooorr")
                        stoppy = True
                    End Try
                    TweetList(cntr) = tweetstring
                    cntr = cntr + 1
                Else
                    If MsgBox("Scrape done, Start Sql Import?", MsgBoxStyle.YesNo) = (MsgBoxResult.Cancel Or MsgBoxResult.No) Then Exit Sub
                    stoppy = True
                End If
            Loop

            Dim i As Integer
            For i = 0 To UBound(TweetList)
                PullTweetData(TweetList(i))
            Next

            Dim constring As String = "Data Source=LUCAS-PC\SQLEXPRESS;Initial Catalog=TwitterGuy;Integrated Security=True"
            Using con1 As New SqlClient.SqlConnection(constring)
                con1.Open()
                Dim cmd As New SqlClient.SqlCommand
                cmd.Connection = con1
                Try
                    cmd.CommandText = "Create Table " & User &
                    " (Name Varchar(100), Handle varchar(100) Then, TwitterID Varchar(29) Primary Key, DateCreated Varchar(50), Bio Varchar(" & Bio.Length & "), ProfilePic Varchar(255), TweetCount int, FaveCount int, 
                       FollowerCount int, FollowingCount int, Location Varchar(100), BackgroundPic varchar(255), TimeZone Varchar(30), GeoEnabled bit, Verified bit)"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "insert into " & User & " (Name, Handle, TwitterID, DateCreated, Bio, ProfilePic, TweetCount, FaveCount, FollowerCount, FollowingCount, Location, BackgroundPic, Timezone, GeoEnabled, Verified) 
                Values ('" & Name & "', '" & User & "', '" & ID & "', '" & WhenCreated & "', '" & Bio & "', '" & ProfilePic & "', " & TweetCount & ", " & LikeCount & ", " & FollowerCount & ", " & FollowingCount & ", '" & Location & "',
                         '" & BackGroundPic & "', '" & TimeZone & "', " & GeoEnabled & ", " & Verified & ")"
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("ohno")
                End Try



            End Using
            ' I should put from tweetlist into sql


        End Using
    End Sub
    Public Function DownloadTweetToString()

        'Address = "https://twitter.com/i/profiles/show/" & User & "/timeline/tweets?include_available_features=1&include_entities=1&max_position=" & position & "&reset_error_state=false"
        'https://twitter.com/i/search/timeline?vertical=default&q=from%3ADrbluejayTV&src=typd&include_available_features=1&include_entities=1&max_position=TWEET-690000000000111976-854730691275362304-&oldest_unread_id=0&reset_error_state=false
        Address = "https://twitter.com/i/search/timeline?vertical=default&q=from%3A" & User & "&src=typd&include_available_features=1&include_entities=1&max_position=TWEET-" & position & "-" & maxy.ToString & "-&oldest_unread_id=0&reset_error_state=false"
        Using webclient = New CookieAwareWebClient
            Try
                webclient.DownloadFile(Address, "C:\Users\Lucas\Desktop\Tweet.txt")

            Catch ex As Exception
                MsgBox("faiiiiilll")
                Exit Function
            End Try
        End Using

        Dim Txtreader = My.Computer.FileSystem.OpenTextFileReader("C:\Users\Lucas\Desktop\Tweet.txt")
        Dim TweetString As String = Txtreader.ReadLine
        Txtreader.Close()
        Return TweetString
    End Function
    Public Sub ParseData(ByVal TweetString As String)
        'position = Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(0)
        'position = Split(Split(TweetString, "data-item-id=\""")(UBound(Split(TweetString, "data-item-id=\"""))), "\")(0)
        If cntr = 0 Then
            Dim lowerPosition As Int64 = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(0))
            maxy = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(1))

            position = (maxy - (maxy - lowerPosition)).ToString
        Else
            Dim TempPosition As Int64 = Int64.Parse(Split(Split(TweetString, "{""min_position"":""TWEET-")(1), "-")(0))
            position = (Int64.Parse(position) - (Int64.Parse(position) - TempPosition)).ToString

        End If
    End Sub
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
    Function PullTweetData(ByVal UnparsedTweet As String)
        'Make an array with all the stuff in it
        '20 tweets
        'each tweet gets an array spot
        'each tweet includes data of its own
        'Tweet data Includes 
        'Tweet ID- Primary key, Tweeter id - foreign key, Tweeter mentions ID - foreign key, 

        Dim TweetDataArray(50) As List(Of Object)


        Return (TweetDataArray)
    End Function
End Module
