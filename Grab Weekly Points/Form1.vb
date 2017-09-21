Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim net As New Net.WebClient()
        Dim src As String
        Dim baseurl As String = "http://rotoguru1.com/cgi-bin/fyday.pl?week=" + cmbWeek.SelectedItem + "&game=dk&scsv=1"
        Dim data As Stream
        Dim reader As StreamReader
        Dim s, sReplace As String
        Dim iCount As Integer
        Dim tet1() As String

        data = net.OpenRead(baseurl)
        reader = New StreamReader(data)
        s = reader.ReadToEnd()
        data.Close()
        reader.Close()

        src = Mid(s, InStr(s, "<pre>"), InStr(s, "</pre") - InStr(s, "<pre"))
        sReplace = src.Replace("<pre>", "").Replace("</pre>", "")

        tet1 = sReplace.Split(ControlChars.CrLf.ToCharArray())

        For iCount = LBound(tet1) To UBound(tet1)
            If tet1(iCount) <> "Week;Year;GID;Name;Pos;Team;h/a;Oppt;DK points;DK salary" And tet1(iCount) <> "" Then
                generateSQL(tet1(iCount))
                TextBox1.Text = TextBox1.Text + tet1(iCount) + vbCrLf
            End If

        Next

        Label1.Text = "Rows Inserted: " + tet1.Count.ToString

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub generateSQL(ByVal sRowData As String)
        Dim sqlStatement As String
        Dim sSplit() As String

        sSplit = Split(sRowData, ";")

        sqlStatement = "insert into weeklypoints (site,season_week,season_year,player_id,player_name,player_position,player_team,home_away,opponent,player_points,player_salary) " &
                    "values ('DraftKings'," &
                    Int(sSplit(0)).ToString + "," &
                    Int(sSplit(1)).ToString + "," &
                    Int(sSplit(2)).ToString + "," &
                    "'" + convertQuotes(sSplit(3)) + "'," &
                    "'" + sSplit(4) + "'," &
                    "'" + sSplit(5) + "'," &
                    "'" + sSplit(6) + "'," &
                    "'" + sSplit(7) + "'," &
                    Int(sSplit(8)).ToString + "," &
                    Int(sSplit(9)).ToString &
                    ")"

        sqlInsertFunction(sqlStatement)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Public Function convertQuotes(ByVal str As String) As String
        convertQuotes = str.Replace("'", "''")
    End Function
End Class
