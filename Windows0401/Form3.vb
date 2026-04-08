Public Class Form3

    Dim numberList As New List(Of String)
    Dim nameList As New List(Of String)
    Dim dateList As New List(Of Date)
    Dim kakeibo As New Dictionary(Of Date, List(Of (Integer, String)))
    Dim totalValue As Integer

    '表示更新用
    Private Sub LoadData()

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Dim d As Date = DateTimePicker1.Value.Date

        If kakeibo.ContainsKey(d) Then

            For Each item In kakeibo(d)

                ListBox1.Items.Add(item.Item1.ToString())
                ListBox2.Items.Add(item.Item2)

            Next

        End If

    End Sub

    '追加
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim d As Date = DateTimePicker1.Value.Date
        'Dim amount As Integer = Integer.Parse(TextBox1.Text)
        'Dim content As String = TextBox2.Text
        Dim amount As Integer = Integer.Parse(TextBox1.Text)
        Dim content As String = TextBox2.Text



        'If amount = "" Then
        '    MessageBox.Show("金額を入力してください")
        '    Return
        'End If

        If Not kakeibo.ContainsKey(d) Then
            kakeibo(d) = New List(Of (Integer, String))
        End If



        'Dictionaryに追加
        kakeibo(d).Add((amount, content))

        'ListBox表示
        ListBox1.Items.Add(amount)
        ListBox2.Items.Add(content)

        'TextBox初期化
        TextBox1.Text = ""
        TextBox2.Text = ""

        'ファイル保存
        SaveData()

    End Sub

    '削除
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If ListBox1.SelectedIndex = -1 Then
            MessageBox.Show("削除する内容を選択してください")
            Return
        End If

        Dim d As Date = DateTimePicker1.Value.Date
        Dim index As Integer = ListBox1.SelectedIndex

        'Dictionaryから削除
        kakeibo(d).RemoveAt(index)

        'ListBox更新
        ListBox1.Items.RemoveAt(index)
        ListBox2.Items.RemoveAt(index)

        'TextBoxクリア
        TextBox1.Text = ""
        TextBox2.Text = ""

        SaveData()

    End Sub

    '更新
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If ListBox1.SelectedIndex = -1 Then
            MessageBox.Show("更新するデータを選択してください")
            Return
        End If

        Dim d As Date = DateTimePicker1.Value.Date
        Dim index As Integer = ListBox1.SelectedIndex

        Dim amount As Integer = Integer.Parse(TextBox1.Text)
        Dim content As String = TextBox2.Text


        'Dictionary更新
        kakeibo(d)(index) = (amount, content)

        'ListBox更新
        ListBox1.Items(index) = amount.ToString()
        ListBox2.Items(index) = content

        SaveData()

        'TextBoxクリア
        TextBox1.Text = ""
        TextBox2.Text = ""

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not System.IO.File.Exists("memo.txt") Then Return

        Dim lines() As String = System.IO.File.ReadAllLines("memo.txt")

        For Each line In lines

            Dim parts() As String = line.Split(",")

            Dim d As Date = Date.Parse(parts(0))
            Dim amount As Integer = Integer.Parse(parts(1))
            Dim content As String = parts(2)

            If Not kakeibo.ContainsKey(d) Then
                kakeibo(d) = New List(Of (Integer, String))
            End If

            kakeibo(d).Add((amount, content))

        Next

        '最初の日付のデータ表示
        DateTimePicker1_ValueChanged(Nothing, Nothing)

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Dim d As Date = DateTimePicker1.Value.Date

        If kakeibo.ContainsKey(d) Then

            For Each item In kakeibo(d)

                ListBox1.Items.Add(item.Item1.ToString())
                ListBox2.Items.Add(item.Item2)

            Next

        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        If ListBox1.SelectedIndex < 0 Then Return

        Dim index As Integer = ListBox1.SelectedIndex

        ListBox2.SelectedIndex = index

        TextBox1.Text = ListBox1.Items(index).ToString()
        TextBox2.Text = ListBox2.Items(index).ToString()

    End Sub


    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

        If ListBox2.SelectedIndex >= 0 Then
            ListBox1.SelectedIndex = ListBox2.SelectedIndex
        End If

    End Sub

    Private Sub SaveData()

        Dim lines As New List(Of String)

        For Each d In kakeibo.Keys

            For Each item In kakeibo(d)

                lines.Add(d.ToString("yyyy/MM/dd") & "," &
                      item.Item1 & "," &
                      item.Item2)

            Next

        Next

        System.IO.File.WriteAllLines("memo.txt", lines)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim d As Date = DateTimePicker1.Value.Date
        Dim total As Integer = 0

        If kakeibo.ContainsKey(d) Then

            For Each item In kakeibo(d)
                total += item.Item1
            Next

        End If

        Label4.Text = total & "円"

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim d As Date = DateTimePicker1.Value.Date
        Dim total As Integer = 0

        '週の開始（日曜始まり）
        Dim startWeek As Date = d.AddDays(-CInt(d.DayOfWeek))

        '週の終わり
        Dim endWeek As Date = startWeek.AddDays(6)

        For Each key In kakeibo.Keys

            If key >= startWeek And key <= endWeek Then

                For Each item In kakeibo(key)
                    total += item.Item1
                Next

            End If

        Next

        Label4.Text = total & "円"

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim d As Date = DateTimePicker1.Value.Date
        Dim total As Integer = 0

        For Each key In kakeibo.Keys

            If key.Month = d.Month And key.Year = d.Year Then

                For Each item In kakeibo(key)
                    total += item.Item1
                Next

            End If

        Next

        Label4.Text = total & "円"
    End Sub
End Class