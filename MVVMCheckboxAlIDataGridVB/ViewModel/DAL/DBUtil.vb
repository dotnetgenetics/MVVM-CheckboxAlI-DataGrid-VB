Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Module DBUtil

    Function GetProduct() As DataTable
        Dim ds As DataSet = New DataSet()
        Dim query As String = "Select * from Products;"
        Try
            Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("products").ConnectionString.ToString())
                Dim cmd As SqlCommand = New SqlCommand(query, conn)
                conn.Open()
                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                da.Fill(ds)
                conn.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return ds.Tables(0)
    End Function

    Function UpdateProductDiscontinue(ByVal value As Boolean, ByVal productID As Integer) As Integer
        Dim result As Integer = 0
        Dim query As String = String.Format("Update Products set discontinue = '{0}' where productID = '{1}' ;", value, productID)
        Try
            Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("products").ConnectionString.ToString())
                Dim cmd As SqlCommand = New SqlCommand(query, conn)
                conn.Open()
                result = cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return result
    End Function
End Module