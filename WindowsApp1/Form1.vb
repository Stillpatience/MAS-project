﻿Imports System.Threading

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim graphics As Graphics = Me.CreateGraphics
        Dim pen As Pen
        pen = New Pen(Drawing.Color.DarkBlue, 5)
        Dim rectangle As New Rectangle
        rectangle.X = 40
        rectangle.Y = 30
        rectangle.Width = 200
        rectangle.Height = 100
        graphics.DrawEllipse(pen, rectangle)
    End Sub
    Dim graphics As Graphics = Me.CreateGraphics
    Dim robots As New ArrayList
    Dim radius As Double = 4
    Dim robot_color As Color = Drawing.Color.Red
    Dim x_dist As Double = 5
    Dim y_dist As Double = 5
    Dim seed_robot_color As Color = Drawing.Color.Green
    Dim center_x As Double = 30
    Dim center_y As Double = 30

    Private Sub InitButton_Click(sender As Object, e As EventArgs) Handles InitButton.Click
        initializeRobots(21)
        initializeSeeds()
        initializeMap()
    End Sub
    Private Sub initializeSeeds()
        init_circle(graphics, center_x, center_y, radius, seed_robot_color, 0)
        init_circle(graphics, center_x + 1, center_y, radius, seed_robot_color, 0)
        init_circle(graphics, center_x, center_y + 1, radius, seed_robot_color, 0)
        init_circle(graphics, center_x + 1, center_y + 1, radius, seed_robot_color, 0)
    End Sub
    Private Sub initializeRobots(amount As Integer)
        Dim amountOfColumns As Integer = Math.Sqrt(amount)
        Dim amountOfRows As Integer = Math.Floor(amount / amountOfColumns)
        Dim remainder As Integer = amount Mod amountOfColumns
        For i As Integer = 1 To amountOfColumns
            For j As Integer = 1 To amountOfRows
                init_circle(graphics, i, j, radius, robot_color, -1)
            Next
        Next
        For k As Integer = 1 To remainder
            init_circle(graphics, amountOfColumns + 1, k, radius, robot_color, -1)
        Next
        'For i As Integer = 1 To amount
        'init_circle(graphics, i, i, radius, robot_color, -1)
        'Next
    End Sub
    Private Sub initializeMap()
        For i As Integer = 30 To 34
            For j As Integer = 30 To 34
                globalMap.Add({i, j})
            Next
        Next
    End Sub

    Private Sub draw_circle(graphics, i, j, radius, color, gradient)
        Dim rectangle As New Rectangle
        Dim brush = New SolidBrush(color)
        rectangle.X = i * x_dist
        rectangle.Y = j * y_dist
        rectangle.Width = radius
        rectangle.Height = radius
        Dim done As Boolean = False

        While Not done
            Try
                graphics.FillEllipse(brush, rectangle)
                done = True
            Catch ex As Exception

            End Try
        End While
    End Sub

    Private Function countRobotsAtPosition(i, j)
        Dim nbOfRobots As Integer = 0
        For k As Integer = 0 To robots.Count - 1
            If robots(k)(0) = i And robots(k)(1) = j Then
                If nbOfRobots = 1 Then
                    Return nbOfRobots + 1
                End If
                nbOfRobots += 1
            End If
        Next
        Return nbOfRobots
    End Function

    Private Sub undraw_circle(graphics, i, j, radius, gradient)
        If countRobotsAtPosition(i, j) < 2 Then
            Dim rectangle As New Rectangle
            Dim brush = New SolidBrush(BackColor)
            rectangle.X = i * x_dist
            rectangle.Y = j * y_dist
            rectangle.Width = radius
            rectangle.Height = radius
            Dim done As Boolean = False
            While Not done
                Try
                    graphics.FillEllipse(brush, rectangle)
                    done = True

                Catch ex As exception

                End Try
            End While
        End If
    End Sub

    Private Sub init_circle(graphics As Graphics, i As Double, j As Double, radius As Double, color As Color, gradient As Integer)
        draw_circle(graphics, i, j, radius, color, gradient)
        robots.Add({i, j, gradient})
    End Sub


    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        graphics.Clear(BackColor)
    End Sub

    Private Function arrived(i As Integer)
        For j As Integer = 0 To robots.Count - 1
            If distance(i, j) < 2 And robots(j)(2) > -1 Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function distance(i As Integer, j As Integer)
        Return Math.Sqrt((robots(i)(0) - robots(j)(0)) ^ 2 + (robots(i)(1) - robots(j)(1)) ^ 2)
    End Function

    Private Function getClosestRobotGradient(i As Integer)
        Dim maxDistance As Double = 2
        Dim smallestGradient As Integer = Integer.MaxValue
        'Dim closestRobot As Integer
        For j As Integer = 0 To robots.Count - 1
            If i <> j Then
                Dim dist As Double
                dist = distance(i, j)
                If dist < maxDistance And robots(j)(2) < smallestGradient Then
                    smallestGradient = robots(j)(2)
                End If
            End If
        Next
        If smallestGradient = Integer.MaxValue Then
            Return -1
        Else
            Return smallestGradient
        End If
    End Function

    Private Sub goToSeed(i As Integer, step_size As Double)
        Dim x As Double
        Dim y As Double
        Dim gradient As Integer = robots(i)(2)
        While Not nextToSeed(i)
            x = robots(i)(0)
            y = robots(i)(1)
            gradient = robots(i)(2)
            moveRobot(i, x + step_size, y + step_size)
        End While
    End Sub

    Private Sub edgeChase(i As Integer, step_size As Double)
        Dim newGradient As Double = getClosestRobotGradient(i)
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        Dim oldGradient As Integer = robots(i)(2)
        Dim gradient As Integer
        Dim steps(2) As Double
        Dim prevSteps(2) As Double
        Dim stepSize As Double = 1
        Dim lastTried As Integer = 0
        'steps = setRandomStep(i, stepSize, gradient)
        While Not arrivedAtFinalPosition(i, stepSize)
            Do
                steps = setRandomStep(i, stepSize, gradient)
            Loop While Math.Abs(prevSteps(0)) = Math.Abs(steps(0)) And Math.Abs(prevSteps(1)) = Math.Abs(steps(1))
            prevSteps = steps
            oldGradient = robots(i)(2)
            x = robots(i)(0)
            y = robots(i)(1)
            While closeEnough(i, x + steps(0), y + steps(1), oldGradient) And canMove(x + steps(0), y + steps(1))
                ' If arrivedAtFinalPosition(i, stepSize) Then
                'Exit While
                'End If
                ' undo(i, steps)
                setSteps(i, steps)
                oldGradient = robots(i)(2)
                x = robots(i)(0)
                y = robots(i)(1)
            End While
            'undo(i, steps)

        End While
        'setSteps(i, steps)
        'newGradient += 1
        'End While
    End Sub

    Private Function setSteps(i As Integer, steps As Double())
        moveRobot(i, robots(i)(0) + steps(0), robots(i)(1) + steps(1))
    End Function

    Private Function maximumGradient(i As Integer, stepSize As Double)
        Dim x_diff As Double
        Dim y_diff As Double
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        Dim gradient As Integer = robots(i)(2)
        Dim prevGradient = getClosestRobotGradient(i)
        'If prevGradient = -1 Then
        ' Return False
        'End If

        For direction As Integer = 1 To 4
            If direction = 1 Then
                x_diff = 0
                y_diff = stepSize
            ElseIf direction = 2 Then
                x_diff = 0
                y_diff = -stepSize
            ElseIf direction = 3 Then
                x_diff = stepSize
                y_diff = 0
            Else
                x_diff = -stepSize
                y_diff = 0
            End If
            If closeEnough(i, x + x_diff, y + y_diff, gradient) And canMove(x + x_diff, y + y_diff) Then
                robots(i)(0) += x_diff
                robots(i)(1) += y_diff
                If getClosestRobotGradient(i) < prevGradient Then
                    robots(i)(0) -= x_diff
                    robots(i)(1) -= y_diff
                    Return False
                End If
                robots(i)(0) -= x_diff
                robots(i)(1) -= y_diff
            End If
        Next
        Return True
    End Function

    Dim globalMap As New ArrayList

    Private Function arrivedAtFinalPosition(i As Integer, stepSize As Double)
        For j As Integer = 0 To globalMap.Count - 1
            If globalMap(j)(0) = robots(i)(0) And globalMap(j)(1) = robots(i)(1) Then
                If maximumGradient(i, stepSize) Then
                    Return True
                End If
            End If
        Next
        Return False
        'Return robots(i)(0) = 52 And robots(i)(1) = 51
    End Function

    Private Function isInArray(tried As Array, steps As Array)
        For i As Integer = 0 To tried.Length - 1
            If tried(i) IsNot Nothing Then
                If tried(i)(0) = steps(0) And tried(i)(1) = steps(1) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function countPossibleMoves(i As Integer, x As Double, y As Double, stepSize As Double, gradient As Integer)
        Dim totalCount As Integer = 0
        If canMove(x + stepSize, y + stepSize) And closeEnough(i, x + stepSize, y + stepSize, gradient) Then
            totalCount += 1
        End If
        If canMove(x + stepSize, y - stepSize) And closeEnough(i, x + stepSize, y - stepSize, gradient) Then
            totalCount += 1
        End If
        If canMove(x - stepSize, y + stepSize) And closeEnough(i, x - stepSize, y + stepSize, gradient) Then
            totalCount += 1
        End If
        If canMove(x - stepSize, y - stepSize) And closeEnough(i, x - stepSize, y - stepSize, gradient) Then
            totalCount += 1
        End If
        Return totalCount
    End Function

    Private Function triedEverything(i As Integer, tried As Array, x As Double, y As Double, stepSize As Double, gradient As Integer)
        Dim amountOfOptions As Integer
        amountOfOptions = countPossibleMoves(i, x, y, stepSize, gradient)
        Dim amountOfNothing As Integer = 0
        For j As Integer = 0 To tried.Length - 1
            If tried(j) Is Nothing Then
                amountOfNothing += 1
            End If
        Next
        Return 4 - amountOfNothing = amountOfOptions
    End Function

    Private Sub moveRobot(i, x, y)
        Dim newGradient As Double = getClosestRobotGradient(i)
        Dim curr_x As Double = robots(i)(0)
        Dim curr_y As Double = robots(i)(1)
        Dim gradient As Integer = robots(i)(2)
        undraw_circle(graphics, curr_x, curr_y, radius, gradient)
        draw_circle(graphics, x, y, radius, robot_color, newGradient)
        robots(i)(0) = x
        robots(i)(1) = y
        If newGradient = -1 Then
            robots(i)(2) = -1
        Else
            robots(i)(2) = newGradient + 1
        End If
    End Sub

    Private Sub undo(i As Integer, steps As Double())
        Dim new_x As Double = robots(i)(0) - steps(0)
        Dim new_y As Double = robots(i)(1) - steps(1)
        moveRobot(i, new_x, new_y)
    End Sub

    Private Function canMove(x As Double, y As Double)
        For i As Integer = 0 To robots.Count - 1
            If robots(i)(0) = x And robots(i)(1) = y And robots(i)(2) > -1 Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function distanceBetweenPoints(x1, y1, x2, y2)
        Return Math.Sqrt((x1 - x2) ^ 2 + (y1 - y2) ^ 2)
    End Function

    Private Function closeEnough(i As Integer, x1 As Double, y1 As Double, gradient As Integer)
        Dim x2 As Double
        Dim y2 As Double
        For j As Integer = 0 To robots.Count - 1
            x2 = robots(j)(0)
            y2 = robots(j)(1)
            'MsgBox(distanceBetweenPoints(x1, y1, x2, y2) < 2)
            'MsgBox("gradient")
            'MsgBox(robots(j)(2) = gradient - 1)
            'And robots(j)(2) >= gradient - 1
            If i <> j And distanceBetweenPoints(x1, y1, x2, y2) < 2 Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function setRandomStep(i As Integer, stepSize As Double, gradient As Integer)
        Dim random As Integer
        Dim x_diff As Double
        Dim y_diff As Double
        Dim new_x As Double
        Dim new_y As Double

        Do
            random = Int((4 - 1 + 1) * Rnd() + 1)
            If random = 1 Then
                x_diff = 0
                y_diff = stepSize
            ElseIf random = 2 Then
                x_diff = 0
                y_diff = -stepSize
            ElseIf random = 3 Then
                x_diff = stepSize
                y_diff = 0
            Else
                x_diff = -stepSize
                y_diff = 0
            End If
            new_x = robots(i)(0) + x_diff
            new_y = robots(i)(1) + y_diff
        Loop While Not (canMove(new_x, new_y) And closeEnough(i, new_x, new_y, gradient))

        moveRobot(i, new_x, new_y)
        'robots(i)(2) = getClosestRobotGradient(i) + 1

        Return {x_diff, y_diff}
    End Function
    Private Function nextToSeed(i As Integer)
        For j As Integer = 0 To robots.Count - 1
            If robots(j)(2) > -1 And distance(i, j) < 2 And i <> j Then
                Return True
            End If
        Next
        Return False
    End Function
    Dim lastMoved As Integer = 20
    Private Sub clickStepButton(i As Integer)
        'MsgBox(lastMoved)
        goToSeed(i, 1)

        edgeChase(i, 1)
    End Sub
    Private Sub StepButton_Click(sender As Object, e As EventArgs) Handles StepButton.Click
        goToSeed(lastMoved, 1)

        edgeChase(lastMoved, 1)
        lastMoved -= 1

        'Dim t1 As Thread = New Thread(New ThreadStart(AddressOf goToSeed))

    End Sub

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        Dim threads As New ArrayList
        Dim lastMoved As Integer = 20
        For i As Integer = 0 To 19
            'Dim t1 As Thread = New Thread(New ThreadStart(AddressOf clickStepButton))
            'threads.Add(New Thread(New ThreadStart(AddressOf clickStepButton)))
            'threads.Add(New Thread(Me.clickStepButton))
            ' threads.Add(New Thread(Sub() Me.clickStepButton(lastMoved)))
            Dim evaluator = New Thread(Sub() Me.clickStepButton(lastMoved))
            evaluator.Start()
            'evaluator.Sleep(3000)
            'goToSeed(i, 1)
            'edgeChase(i, 1)
            lastMoved -= 1
        Next
        'For i As Integer = 0 To threads.Count - 1
        'threads(i).Start()
        'lastMoved += 1

        'Next
    End Sub

    Private Sub StartSingleThread_Click(sender As Object, e As EventArgs) Handles StartSingleThread.Click
        For i As Integer = 0 To 20
            goToSeed(i, 1)
            edgeChase(i, 1)
        Next
    End Sub
End Class