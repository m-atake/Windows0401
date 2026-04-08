Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("追加タスクを入力してください。")
            Return
        End If
        If ListBox1.Items.Contains(TextBox1.Text) Then
            MessageBox.Show("同じタスクが既に存在します。")
            Return
        End If
            ListBox1.Items.Add(TextBox1.Text)
        TextBox1.Text = ""
        System.IO.File.WriteAllLines("todo.txt", ListBox1.Items.Cast(Of String).ToArray())
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If ListBox1.SelectedIndex = -1 Then
            MessageBox.Show("削除するタスクを選択してください。")
            Return
        End If
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        System.IO.File.WriteAllLines("todo.txt", ListBox1.Items.Cast(Of String).ToArray())
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonAdd.PerformClick()
        End If
        System.IO.File.WriteAllLines("todo.txt", ListBox1.Items.Cast(Of String).ToArray())
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If System.IO.File.Exists("todo.txt") Then
            Dim lines As String() = System.IO.File.ReadAllLines("todo.txt")
            ListBox1.Items.AddRange(lines)
        End If
    End Sub
End Class