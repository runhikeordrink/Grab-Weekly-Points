Imports System.Data.Odbc

Module modSQL_Insert
    Public Sub sqlInsertFunction(ByVal sqlInsert As String)
        Dim StrConn As String = "Dsn=PostgreSQL35W;database=nfldb;server=127.0.0.1;port=5432;uid=nfldb;pwd=nfldb;"
        Dim MyConn As New OdbcConnection(StrConn)
        Dim myInsert As String = sqlInsert
        Dim MyDataAdapter As New OdbcDataAdapter
        Dim myCommand As New OdbcCommand(myInsert, MyConn)
        'Dim retValue As Integer

        MyConn.Open()
        myCommand.ExecuteNonQuery()
        'retValue = myCommand.ExecuteNonQuery.
        MyConn.Close()
    End Sub
End Module
