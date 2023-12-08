Imports System
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports System.Runtime.Serialization

Module Program
    Private _rand As New Random()

    Sub Main(args As String())
        Do
            Console.Clear()
            Console.WriteLine("Select an option:")
            Console.WriteLine("1. Use the array directly (integer numbers)")
            Console.WriteLine("2. Use the method that generates an array (integer numbers)")
            Console.WriteLine("3. Use the array directly (decimal numbers)")
            Console.WriteLine("4. Use the method that generates an array (decimal numbers)")
            Console.WriteLine("0. Exit")
            Dim opc As Integer
            If Not Integer.TryParse(Console.ReadLine(), opc) Then
                Console.WriteLine("Invalid input. Please enter a valid number.")
                Console.ReadKey()
                Continue Do
            End If

            Select Case opc
                Case 1
                    Dim array1() As Integer = {4, 2, 3, 5, 5, 7, 1}
                    Console.WriteLine("Array before sorting: [" & String.Join(", ", array1) & "]")
                    Dim startTime As DateTime = DateTime.Now
                    BucketSort_int(array1)
                    Console.WriteLine(vbLf & "Array after sorting: [" & String.Join(", ", array1) & "]")
                    Console.WriteLine("Time: " & (DateTime.Now - startTime).ToString())
                Case 2
                    Dim array2() As Integer = GenerateIntArray()
                    Console.WriteLine("Array before sorting: [" & String.Join(", ", array2) & "]")
                    Dim startTime_ As DateTime = DateTime.Now
                    BucketSort_int(array2)
                    Console.WriteLine(vbLf & "Array after sorting: [" & String.Join(", ", array2) & "]")
                    Console.WriteLine("Time: " & (DateTime.Now - startTime_).ToString())
                Case 3
                    Dim array3() As Double = {0.42, 0.33, 0.37, 0.57, 0.4}
                    Console.WriteLine("Array before sorting: [" & String.Join(", ", array3) & "]")
                    Dim startTim As DateTime = DateTime.Now
                    BucketSort_Double(array3)
                    Console.WriteLine(vbLf & "Array after sorting: [" & String.Join(", ", array3) & "]")
                    Console.WriteLine("Time: " & (DateTime.Now - startTim).ToString())
                Case 4
                    Dim array4() As Double = GenerateDoubleArray()
                    Console.WriteLine("Array before sorting: [" & String.Join(", ", array4) & "]")
                    Dim s_tartTim As DateTime = DateTime.Now
                    BucketSort_Double(array4)
                    Console.WriteLine(vbLf & "Array after sorting: [" & String.Join(", ", array4) & "]")
                    Console.WriteLine("Time: " & (DateTime.Now - s_tartTim).ToString())
                Case 0
                    Return
                Case Else
                    Console.WriteLine("Invalid option. Please choose an option from 0 to 4.")
            End Select
            Console.ReadKey()
        Loop While True
    End Sub

    Public Function GenerateDoubleArray(Optional Min As Integer = 0, Optional Length As Integer = 10, Optional Values As Integer = 5) As Double()
        Dim list As New List(Of Double)()

        For i As Integer = Min To Length - 1
            If i < Values Then
                Dim newValue As Double = _rand.NextDouble()
                If list.Contains(newValue) Then
                    i -= 1
                    Continue For
                End If
                list.Add(newValue)
            Else
                Exit For
            End If
        Next
        Return list.ToArray()
    End Function

    Public Function GenerateIntArray(Optional Min As Integer = 0, Optional Length As Integer = 10, Optional Values As Integer = 5) As Integer()
        Dim list As New List(Of Integer)()

        For i As Integer = Min To Length - 1
            If i < Values Then
                Dim newValue As Integer = _rand.Next(Min, Length + 1)
                If list.Contains(newValue) Then
                    i -= 1
                    Continue For
                End If
                list.Add(newValue)
            Else
                Exit For
            End If
        Next
        Return list.ToArray()
    End Function

    Sub BucketSort_Double(array As Double())
        ' Create empty buckets
        Dim buckets(array.Length - 1) As List(Of Double)
        For i As Integer = 0 To buckets.Length - 1
            buckets(i) = New List(Of Double)()
        Next

        ' Insert elements into their respective buckets
        For Each element As Double In array
            buckets(CInt(Fix(element * array.Length))).Add(element)
        Next

        ' Print the state of the buckets after insertion
        PrintBucketState(buckets)

        ' Sort the elements in each bucket
        For i As Integer = 0 To array.Length - 1
            buckets(i).Sort()
        Next

        ' Print the state of the buckets after sorting
        PrintBucketState(buckets)

        ' Get the sorted elements
        Dim k As Integer = 0
        For i As Integer = 0 To array.Length - 1
            For Each item As Double In buckets(i)
                array(k) = item
                k += 1
            Next
        Next
    End Sub

    Sub PrintBucketState(buckets As List(Of Integer)())
        Console.WriteLine("Current state of buckets:")
        For i As Integer = 0 To buckets.Length - 1
            Console.Write($"Bucket {i}: ")
            For Each item As Integer In buckets(i)
                Console.Write($"{item} ")
            Next
            Console.WriteLine()
        Next
        Console.WriteLine()
    End Sub

    Sub PrintBucketState(buckets As List(Of Double)())
        Console.WriteLine("Current state of buckets:")
        For i As Integer = 0 To buckets.Length - 1
            Console.Write($"Bucket {i}: ")
            For Each item As Double In buckets(i)
                Console.Write($"{item} ")
            Next
            Console.WriteLine()
        Next
        Console.WriteLine()
    End Sub

    Public Sub BucketSort_int(array As Integer())
        ' Find the maximum value in the array
        Dim maxVal As Integer = array(0)

        For i As Integer = 1 To array.Length - 1
            If array(i) > maxVal Then
                maxVal = array(i)
            End If
        Next
        ' Create a list of empty buckets
        Dim buckets(maxVal) As List(Of Integer)
        For i As Integer = 0 To buckets.Length - 1
            buckets(i) = New List(Of Integer)()
        Next

        ' Distribute elements into the buckets
        For i As Integer = 0 To array.Length - 1
            buckets(array(i)).Add(array(i))
        Next
        PrintBucketState(buckets)

        ' Sort each bucket individually
        For i As Integer = 0 To buckets.Length - 1
            buckets(i).Sort()
        Next
        PrintBucketState(buckets)

        ' Concatenate the sorted elements from each bucket
        Dim index As Integer = 0
        For i As Integer = 0 To buckets.Length - 1
            For Each item As Integer In buckets(i)
                array(index) = item
                index += 1
            Next
        Next
    End Sub
End Module

