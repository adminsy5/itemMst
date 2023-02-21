Imports System.Data.SqlClient
Public Class Form1
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim ad As SqlDataAdapter
    Dim dt As DataSet

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Connection()
        createTable()
        insertData()
        ButtonClear_Click(sender, e)
        showdata()
    End Sub

    Private Sub Connection()
        con = New SqlConnection("Data Source=AMDSUR\SQLEXPRESS;Initial catalog=Admin574;Integrated Security=True;")
        Try
            con.Open()
            If con.State = ConnectionState.Open Then
                'MsgBox("Connected")
            End If
        Catch ec As Exception
            'MsgBox(ec.Message)
        End Try
    End Sub

    Private Sub createTable()
        cmd = New SqlCommand("create table itemMst(itemno int primary key,name varchar(20),cost int,quantity int)", connection:=con)
        Try
            cmd.ExecuteNonQuery()
            'MsgBox("Create Table ")
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub insertData()
        Dim itemno As Integer = Convert.ToInt32(TextBoxItemNo.Text)
        Dim name As String = TextBoxName.Text
        Dim cost As Integer = Convert.ToInt32(TextBoxCost.Text)
        Dim quantity As Integer = Convert.ToInt32(TextBoxQuantity.Text)

        cmd = New SqlCommand("insert into itemMst values(@itemno,@name,@cost,@quantity)", connection:=con)
        cmd.Parameters.AddWithValue("@itemno", itemno)
        cmd.Parameters.AddWithValue("@name", name)
        cmd.Parameters.AddWithValue("@cost", cost)
        cmd.Parameters.AddWithValue("@quantity", quantity)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("insert!")
        Catch ec As Exception
            MsgBox(ec.Message)
        End Try
    End Sub

    Private Sub showdata()
        ad = New SqlDataAdapter("select * from itemMst", selectConnection:=con)
        dt = New DataSet
        ad.Fill(dt, "itemMst")
        DataGridViewReader.DataSource = dt.Tables(0).DefaultView
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Connection()
        If DataGridViewReader.RowCount = 0 Then
            MsgBox("Unable to deleted !")
        End If

        If MsgBox("Deleted Record ?", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Cancel Then Return

        Try
            If DataGridViewReader.AreAllCellsSelected(0) = True Then
                cmd = New SqlCommand("delete from itemMst", connection:=con)
                cmd.ExecuteNonQuery()
                ButtonRefresh_Click(sender, e)
                MsgBox("Deleted All")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            For Each row As DataGridViewRow In DataGridViewReader.SelectedRows
                If row.Selected Then
                    cmd = New SqlCommand("delete from itemMst where itemno = '" & row.DataBoundItem(0).ToString() & "'", connection:=con)
                    cmd.ExecuteNonQuery()
                    ButtonRefresh_Click(sender, e)
                    MsgBox("Deleted")
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        Connection()
        ad = New SqlDataAdapter("select * from itemMst", selectConnection:=con)
        dt = New DataSet
        ad.Fill(dt, "itemMst")
        DataGridViewReader.DataSource = dt.Tables(0).DefaultView
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        TextBoxCost.Clear()
        TextBoxItemNo.Clear()
        TextBoxName.Clear()
        TextBoxQuantity.Clear()
        TextBoxSrch.Clear()
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Connection()
        Dim itemno As Integer = Convert.ToInt32(TextBoxItemNo.Text)
        Dim name As String = TextBoxName.Text
        Dim cost As Integer = Convert.ToInt32(TextBoxCost.Text)
        Dim quantity As Integer = Convert.ToInt32(TextBoxQuantity.Text)

        cmd = New SqlCommand("Update itemMst set name=@name,cost=@cost,quantity=@quantity where itemno=@itemno", connection:=con)
        cmd.Parameters.AddWithValue("@itemno", itemno)
        cmd.Parameters.AddWithValue("@name", name)
        cmd.Parameters.AddWithValue("@cost", cost)
        cmd.Parameters.AddWithValue("@quantity", quantity)
        Try
            cmd.ExecuteNonQuery()
            ButtonRefresh_Click(sender, e)
            MsgBox("Updated")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Connection()
        Dim name As String = TextBoxSrch.Text

        ad = New SqlDataAdapter("select * from itemMst where name LIKE '" & TextBoxSrch.Text & "'", selectConnection:=con)
        dt = New DataSet
        ad.Fill(dt, "itemMst")
        Try
            DataGridViewReader.DataSource = dt.Tables(0).DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        ButtonDelete_Click(sender, e)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MsgBox("Admin Piyush !")
        Dim str As String = "https://mpiyush3510.carrd.co/"
        Diagnostics.Process.Start("C:\Program Files\Google\Chrome\Application\chrome.exe", str)
    End Sub
End Class
