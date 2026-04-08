Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim num1 As Integer = Integer.Parse(TextBox2.Text)
        Dim num2 As Integer = Integer.Parse(TextBox3.Text)
        Button1.Text = "＋"
        Label1.Text = (num1 + num2).ToString()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim num1 As Integer = Integer.Parse(TextBox2.Text)
        Dim num2 As Integer = Integer.Parse(TextBox3.Text)
        Button2.Text = "－"
        Label1.Text = (num1 - num2).ToString()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim num1 As Integer = Integer.Parse(TextBox2.Text)
        Dim num2 As Integer = Integer.Parse(TextBox3.Text)
        Button3.Text = "×"
        Label1.Text = (num1 * num2).ToString()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim num1 As Integer = Integer.Parse(TextBox2.Text)
        Dim num2 As Integer = Integer.Parse(TextBox3.Text)
        Button4.Text = "÷"
        Label1.Text = (num1 / num2).ToString()
    End Sub
    Private Sub Symbol1_Change(sender As Object, e As EventArgs) Handles Button1.TextChanged
        If Button1.Text = "＋" Then
            TextBox1.Text = "足し算の計算結果: "
        ElseIf Button2.Text = "－" Then
            TextBox1.Text = "引き算の計算結果: "
        ElseIf Button3.Text = "×" Then
            TextBox1.Text = "掛け算の計算結果: "
        ElseIf Button4.Text = "÷" Then
            TextBox1.Text = "割り算の計算結果: "
        Else
            TextBox1.Text = "計算結果: "
        End If
    End Sub




End Class
