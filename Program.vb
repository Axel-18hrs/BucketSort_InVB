Imports System
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.JavaScript.JSType

Module Module1
    Private _rand As New Random()

    Sub Main()
        Do
            Console.Clear()
            Console.WriteLine("Seleccione una opción:")
            Console.WriteLine("1. Utilizar el arreglo directamente (números enteros)")
            Console.WriteLine("2. Utilizar el método que genera un vector (números enteros)")
            Console.WriteLine("3. Utilizar el arreglo directamente (números decimales)")
            Console.WriteLine("4. Utilizar el método que genera un vector (números decimales)")
            Console.WriteLine("0. Salir")

            Dim opcion As Integer
            If Not Integer.TryParse(Console.ReadLine(), opcion) Then
                Console.WriteLine("Entrada no válida. Por favor, ingrese un número válido.")
                Console.ReadKey()
                Continue Do
            End If

            Select Case opcion
                Case 1
                    Dim array1() As Integer = {4, 2, 3, 5, 5, 7, 1}
                    Console.WriteLine("Array antes de ordenar:")
                    PrintArray(array1)
                    BucketSort_int(array1)
                    Console.WriteLine(vbCrLf & "Array después de ordenar:")
                    PrintArray(array1)
                    Exit Select

                Case 2
                    Dim array2() As Integer = GenerarVector()
                    Console.WriteLine("Array antes de ordenar:")
                    PrintArray(array2)
                    BucketSort_int(array2)
                    Console.WriteLine(vbCrLf & "Array después de ordenar:")
                    PrintArray(array2)
                    Exit Select

                Case 3
                    Dim array3() As Double = {0.42, 0.33, 0.37, 0.57, 0.4}
                    Console.WriteLine("Array antes de ordenar:")
                    PrintArray(array3)
                    BucketSort_Double(array3)
                    Console.WriteLine(vbCrLf & "Array después de ordenar:")
                    PrintArray(array3)
                    Exit Select

                Case 4
                    Dim array4() As Double = GenerarVectorDouble()
                    Console.WriteLine("Array antes de ordenar:")
                    PrintArray(array4)
                    BucketSort_Double(array4)
                    Console.WriteLine(vbCrLf & "Array después de ordenar:")
                    PrintArray(array4)
                    Exit Select

                Case 0
                    Return

                Case Else
                    Console.WriteLine("Opción no válida. Por favor, elija una opción del 0 al 4.")
            End Select

            Console.ReadKey()
        Loop While True
    End Sub

    Public Function GenerarVectorDouble(Optional ByVal Minon As Integer = 0, Optional ByVal Lenght As Integer = 10, Optional ByVal values As Integer = 5) As Double()
        Dim _List As New List(Of Double)()

        For i As Integer = Minon To Lenght - 1
            If i < values Then
                Dim NewValor As Double = _rand.NextDouble()
                _List.Add(NewValor)
            Else
                Exit For
            End If
        Next
        Return _List.ToArray()
    End Function

    Public Function GenerarVector(Optional ByVal Minon As Integer = 0, Optional ByVal Lenght As Integer = 10, Optional ByVal values As Integer = 5) As Integer()
        Dim _List As New List(Of Integer)()

        For i As Integer = Minon To Lenght - 1
            If i < values Then
                Dim NewValor As Integer = _rand.Next(Minon, Lenght + 1)
                If _List.Contains(NewValor) Then
                    i -= 1
                    Continue For
                End If
                _List.Add(NewValor)
            Else
                Exit For
            End If
        Next
        Return _List.ToArray()
    End Function

    Private Sub PrintArray(ByVal doubles As Double())
        For Each item As Double In doubles
            Console.Write(item & " ")
        Next
        Console.WriteLine()
    End Sub

    Sub BucketSort_Double(ByVal array As Double())
        ' Crear buckets vacíos
        Dim buckets(array.Length - 1) As List(Of Double)
        For i As Integer = 0 To buckets.Length - 1
            buckets(i) = New List(Of Double)()
        Next

        ' Insertar elementos en sus respectivos buckets
        For Each element As Double In array
            Dim bucketIndex As Integer = CInt(Math.Floor(element * array.Length))
            ' Asegurarse de que el índice del bucket esté dentro de los límites del array
            If bucketIndex >= array.Length Then
                bucketIndex = array.Length - 1
            End If
            buckets(bucketIndex).Add(element)
        Next

        ' Imprimir el estado de los buckets después de la inserción
        PrintBucketState(buckets)

        ' Ordenar los elementos de cada cubo
        For i As Integer = 0 To array.Length - 1
            buckets(i).Sort()
        Next

        ' Imprimir el estado de los buckets después de la ordenación
        PrintBucketState(buckets)

        ' Obtener los elementos ordenados
        Dim k As Integer = 0
        For i As Integer = 0 To array.Length - 1
            For Each item As Double In buckets(i)
                array(k) = item
                k += 1
            Next
        Next
    End Sub


    Sub PrintBucketState(ByVal buckets() As List(Of Double))
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

    Sub PrintBucketState(ByVal buckets() As List(Of Integer))
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

    Public Sub BucketSort_int(ByVal array As Integer())
        ' Encuentra el valor máximo en el array
        Dim maxVal As Integer = array(0)

        For i As Integer = 1 To array.Length - 1
            If array(i) > maxVal Then
                maxVal = array(i)
            End If
        Next

        ' Crea una lista de buckets vacíos
        Dim buckets(maxVal) As List(Of Integer)
        For i As Integer = 0 To buckets.Length - 1
            buckets(i) = New List(Of Integer)()
        Next

        ' Distribuye los elementos en los buckets
        For i As Integer = 0 To array.Length - 1
            buckets(array(i)).Add(array(i))
        Next
        PrintBucketState(buckets)

        ' Ordena cada cubo individualmente
        For i As Integer = 0 To buckets.Length - 1
            buckets(i).Sort()
        Next
        PrintBucketState(buckets)

        ' Concatena los elementos ordenados de cada cubo
        Dim index As Integer = 0
        For i As Integer = 0 To buckets.Length - 1
            For Each item As Integer In buckets(i)
                array(index) = item
                index += 1
            Next
        Next
    End Sub

    Sub PrintArray(ByVal arr As Integer())
        For Each item As Integer In arr
            Console.Write(item & " ")
        Next
        Console.WriteLine()
    End Sub
End Module

