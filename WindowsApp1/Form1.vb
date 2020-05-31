Imports System.Threading
Imports System.Timers
Imports Timer = System.Timers.Timer

Public Class Form1
    Dim graphics As Graphics
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 800
        Me.Width = 1100
        Me.Show()
        graphics = Me.CreateGraphics
    End Sub

    Dim robots As New ArrayList
    Dim robotRadius As Double = 4
    Dim robotColor As Color = Drawing.Color.Red
    Dim x_dist As Double = 5
    Dim y_dist As Double = 5
    Dim seedRobotColor As Color = Drawing.Color.Green
    Dim centerX As Double = 50
    Dim centerY As Double = 50
    Dim amountOfNonSeedRobots As Integer
    Dim amountOfSeeds As Integer = 4
    Dim amountOfRows As Integer
    Dim remainder As Integer
    Dim amountOfColumns As Integer
    Dim globalMap As New ArrayList

    Private Sub InitSquareButton_Click(sender As Object, e As EventArgs) Handles InitSquare.Click
        ClearButton.PerformClick()
        initializeSquareMap(40, 25)
        initializeRobots(amountOfNonSeedRobots)
        initializeSeeds()
        showMap()
    End Sub
    Private Sub initializeSeeds()
        initializeCircle(centerX, centerY, robotRadius, seedRobotColor, 0, True)
        initializeCircle(centerX + 1, centerY, robotRadius, seedRobotColor, 1, True)
        initializeCircle(centerX, centerY + 1, robotRadius, seedRobotColor, 1, True)
        initializeCircle(centerX + 1, centerY + 1, robotRadius, seedRobotColor, 1, True)
    End Sub
    Private Sub initializeRobots(amount As Integer)
        amountOfColumns = Math.Sqrt(amount)
        amountOfRows = Math.Floor(amount / amountOfColumns)
        remainder = amount Mod amountOfColumns
        For i As Integer = 1 To amountOfColumns
            For j As Integer = 1 To amountOfRows
                initializeCircle(i, j, robotRadius, robotColor, -1, False)
            Next
        Next
        For k As Integer = 1 To remainder
            initializeCircle(amountOfColumns + 1, k, robotRadius, robotColor, -1, False)
        Next
        robots.Reverse()
    End Sub

    Private Sub initializeCircleMap(radius As Double)
        Dim totalAmountOfRobots As Integer
        Dim centerOfCircleX As Double = centerX + radius * Math.Sqrt(2) / 2
        Dim centerOfCircleY As Double = centerY + radius * Math.Sqrt(2) / 2
        For i As Integer = centerX - radius To centerOfCircleX + radius
            For j As Integer = centerY - radius To centerOfCircleY + radius
                If distanceBetweenPoints(centerOfCircleX, centerOfCircleY, i, j) < radius Then
                    globalMap.Add({i, j})
                    totalAmountOfRobots += 1
                End If
            Next
        Next
        'showMap()
        amountOfNonSeedRobots = totalAmountOfRobots + 1 - amountOfSeeds
        MsgBox(totalAmountOfRobots)
    End Sub

    Private Sub initializeQuarterCircleMap(width As Integer, height As Integer, radius As Double)
        Dim totalAmountOfRobots As Integer
        For i As Integer = centerX To centerX + width
            For j As Integer = centerY To centerY + height
                If distanceBetweenPoints(centerX, centerY, i, j) < radius Then
                    globalMap.Add({i, j})
                    totalAmountOfRobots += 1
                End If
            Next
        Next

        'showMap()
        amountOfNonSeedRobots = totalAmountOfRobots - amountOfSeeds
        MsgBox(totalAmountOfRobots)
    End Sub

    Private Sub showMap()
        For i As Integer = 0 To globalMap.Count() - 1
            draw_circle(globalMap(i)(0), globalMap(i)(1), 4, robotColor, -1)
        Next
    End Sub

    Private Sub initializeSquareMap(rows As Integer, columns As Integer)
        Dim totalAmountOfRobots As Integer
        For i As Integer = centerX To centerX + rows - 1
            For j As Integer = centerY To centerY + columns - 1
                globalMap.Add({i, j})
                totalAmountOfRobots += 1
            Next
        Next
        amountOfNonSeedRobots = totalAmountOfRobots - amountOfSeeds
        MsgBox(totalAmountOfRobots)
    End Sub

    Private Sub draw_circle(i, j, radius, color, gradient)
        Dim rectangle As New Rectangle
        Dim brush = New SolidBrush(color)
        rectangle.X = i * x_dist
        rectangle.Y = j * y_dist
        rectangle.Width = radius
        rectangle.Height = radius
        Dim done As Boolean = False

        While Not done
            Try
                'graphics.FillEllipse()
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

    Private Sub undraw_circle(i, j, radius, gradient)
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

                Catch ex As Exception

                End Try
            End While
        End If
    End Sub

    Private Sub initializeCircle(i As Double, j As Double, radius As Double, color As Color, gradient As Integer, arrived As Boolean)
        draw_circle(i, j, radius, color, gradient)

        robots.Add({i, j, gradient, arrived})
    End Sub


    Private Sub ClearButtonClick(sender As Object, e As EventArgs) Handles ClearButton.Click
        graphics.Clear(BackColor)
        robots.Clear()
        globalMap.Clear()
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
                If dist < maxDistance And robots(j)(2) < smallestGradient And robots(j)(3) = True Then
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
    Private Sub goToOrigin(i As Integer, stepSize As Double)
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        For j As Double = 0 To x - 1 Step stepSize
            moveRobot(i, x - stepSize, y)
            x = robots(i)(0)
        Next
        For k As Double = 0 To y - 1 Step stepSize
            moveRobot(i, x, y - stepSize)
            y = robots(i)(1)
        Next
    End Sub
    Private Sub goToSeed(i As Integer, stepSize As Double)
        robots(i)(3) = False
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        For j As Double = x To centerX - 2 Step stepSize
            Dim done As Boolean = False
            While Not done
                Try
                    moveRobot(i, x + stepSize, y)
                    done = True
                Catch e As Exception
                End Try
            End While
            x = robots(i)(0)
        Next
        For k As Double = y To centerY - 2 Step stepSize
            Dim done As Boolean = False
            While Not done
                Try
                    moveRobot(i, x, y + stepSize)
                    done = True
                Catch e As Exception
                End Try
            End While
            y = robots(i)(1)
        Next
    End Sub

    Private Sub edgeChase(i As Integer, stepSize As Double)
        Dim newGradient As Double = getClosestRobotGradient(i)
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        Dim gradient As Integer
        Dim steps(2) As Double
        Dim prevSteps(2) As Double
        Dim arrivedOnMap As Boolean = False
        Dim count As Integer = 0

        While Not arrivedAtFinalPosition(i, stepSize, steps)

            prevSteps = steps
            Dim innerCount As Integer = 0
            Do
                innerCount += 1
                If innerCount > 100 Then
                    Exit Do
                End If
                steps = pickRandomStep(i, stepSize, gradient)
            Loop While Math.Abs(prevSteps(0)) = Math.Abs(steps(0)) And Math.Abs(prevSteps(1)) = Math.Abs(steps(1))
            If onMap(x + steps(0), y + steps(1)) Then
                arrivedOnMap = True
            End If
            setSteps(i, steps)
            gradient = robots(i)(2)
            x = robots(i)(0)
            y = robots(i)(1)
            count += 1
            While closeEnough(i, x + steps(0), y + steps(1), gradient) And canMove(x + steps(0), y + steps(1))
                Dim tries As Integer = 0
                If count > 100 Then
                    Do
                        prevSteps = steps
                        steps = pickRandomStep(i, stepSize, gradient)
                        If canMove(x + steps(0), y + steps(1)) Then
                            Exit Do
                        End If
                        tries += 1
                        If tries > 10 Then
                            Exit Do
                        End If
                    Loop While Math.Abs(prevSteps(0)) = Math.Abs(steps(0)) And Math.Abs(prevSteps(1)) = Math.Abs(steps(1))
                End If
                If onMap(x + steps(0), y + steps(1)) Then
                    arrivedOnMap = True
                End If
                If arrivedOnMap And Not onMap(x + steps(0), y + steps(1)) Then
                    Exit While
                End If


                setSteps(i, steps)
                'If arrivedAtFinalPosition(i, stepSize, steps) Then
                'steps = pickRandomStep(i, stepSize, gradient)
                'Exit While
                'End If
                gradient = robots(i)(2)
                x = robots(i)(0)
                y = robots(i)(1)
            End While
        End While
        robots(i)(3) = True
    End Sub

    Private Function isElementOf(list As ArrayList, el1 As Double, el2 As Double)
        For i As Integer = 0 To list.Count - 1
            If el1 = list(i)(0) And el2 = list(i)(1) Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub setSteps(i As Integer, steps As Double())
        Dim done As Boolean = False
        While Not done
            Try
                moveRobot(i, robots(i)(0) + steps(0), robots(i)(1) + steps(1))
                done = True
            Catch ex As Exception

            End Try
        End While
    End Sub
    Private Function getClosestRobotIndex(i As Integer)
        Dim smallestIndex As Integer
        Dim smallestDistance As Integer = Integer.MaxValue
        For j As Integer = 0 To robots.Count - 1
            If i <> j And distance(i, j) < smallestDistance Then
                smallestIndex = i
            End If
        Next
        Return smallestIndex
    End Function
    Private Function maximumGradient(i As Integer, stepSize As Double, steps As Double())
        Dim x_diff As Double
        Dim y_diff As Double
        Dim x As Double = robots(i)(0)
        Dim y As Double = robots(i)(1)
        Dim gradient As Integer = robots(i)(2)
        Dim prevClosestGradient = getClosestRobotGradient(i)
        Dim prevClosestIndex = getClosestRobotIndex(i)
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
                'MsgBox(closeEnough(i, x + x_diff, y + y_diff, gradient))
                'MsgBox(Str(x + x_diff) + " " + Str(x) + " " + Str(y + y_diff) + " " + Str(y))
                robots(i)(0) += x_diff
                robots(i)(1) += y_diff
                If steps(0) <> -x_diff And steps(1) <> -y_diff And getClosestRobotGradient(i) <= prevClosestGradient And onMap(robots(i)(0), robots(i)(1)) Then
                    robots(i)(0) -= x_diff
                    robots(i)(1) -= y_diff
                    'MsgBox("false" + Str(x_diff) + Str(y_diff))
                    'MsgBox(closeEnough(i, x + x_diff, y + y_diff, gradient))
                    ' MsgBox(Str(steps(0)) + " " + Str(steps(1)))
                    Return False
                End If
                robots(i)(0) -= x_diff
                robots(i)(1) -= y_diff
            End If
        Next
        Return True
    End Function
    Private Function onMap(x As Double, y As Double)
        For i As Integer = 0 To globalMap.Count - 1
            If globalMap(i)(0) = x And globalMap(i)(1) = y Then
                Return True
            End If
        Next
        Return False

    End Function

    Private Function arrivedAtFinalPosition(i As Integer, stepSize As Double, steps As Double())
        For j As Integer = 0 To globalMap.Count - 1
            If globalMap(j)(0) = robots(i)(0) And globalMap(j)(1) = robots(i)(1) Then
                If maximumGradient(i, stepSize, steps) Then
                    Return True
                End If
            End If
        Next
        Return False
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

    Private Function triedEverything(tried As ArrayList)

    End Function

    Private Sub moveRobot(i, x, y)
        If canMove(x, y) Then
            Dim newGradient As Double = getClosestRobotGradient(i)
            Dim curr_x As Double = robots(i)(0)
            Dim curr_y As Double = robots(i)(1)
            Dim gradient As Integer = robots(i)(2)
            undraw_circle(curr_x, curr_y, robotRadius, gradient)
            draw_circle(x, y, robotRadius, robotColor, newGradient)
            robots(i)(0) = x
            robots(i)(1) = y
            If newGradient = -1 Then
                robots(i)(2) = -1
            Else
                robots(i)(2) = newGradient + 1
            End If
        Else
            Throw New Exception
        End If
    End Sub

    Private Sub undo(i As Integer, steps As Double())
        Dim new_x As Double = robots(i)(0) - steps(0)
        Dim new_y As Double = robots(i)(1) - steps(1)
        moveRobot(i, new_x, new_y)
    End Sub

    Private Function canMove(x As Double, y As Double)
        For i As Integer = 0 To robots.Count - 1
            If robots(i)(0) = x And robots(i)(1) = y Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function canMoveButBlocked(x As Double, y As Double)
        For i As Integer = 0 To robots.Count - 1
            If robots(i)(0) = x And robots(i)(1) = y And robots(i)(3) = True Then
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
    Private Function pickRandomStep(i As Integer, stepSize As Double, gradient As Integer)
        Dim random As Integer
        Dim x_diff As Double
        Dim y_diff As Double
        Dim new_x As Double
        Dim new_y As Double
        Static Generator As System.Random = New System.Random()
        'Return Generator.Next(Min, Max)

        Do
            random = Generator.Next(1, 5)
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

        'moveRobot(i, new_x, new_y)
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
    Dim lastMoved As Integer = 0
    Private Sub stepRobot(i As Integer)
        goToSeed(i, 1)
        edgeChase(i, 1)
    End Sub
    Private Sub StepButton_Click(sender As Object, e As EventArgs) Handles StepButton.Click
        goToSeed(lastMoved, 1)
        edgeChase(lastMoved, 1)
        lastMoved += 1
    End Sub
    Dim threads As New ArrayList

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        Dim sleepTime As Integer = 500
        For i As Integer = 0 To amountOfNonSeedRobots
            Dim evaluator = New Thread(Sub() Me.stepRobot(i))
            threads.Add(evaluator)
            If i < 5 Then
                evaluator.Start()
                evaluator.Sleep(500)
            Else
                While robots(i - 2)(3) = False
                    'evaluator.Start()
                    'evaluator.Sleep(500)
                End While
                evaluator.Start()
                evaluator.Sleep(500)
            End If

        Next
    End Sub

    Private Sub StartSingleThread_Click(sender As Object, e As EventArgs) Handles StartSingleThread.Click
        For i As Integer = 0 To amountOfNonSeedRobots - 1
            goToSeed(i, 1)
            edgeChase(i, 1)
        Next
    End Sub


    Private Sub InitQuarterCircle_Click(sender As Object, e As EventArgs) Handles InitQuarterCircle.Click
        ClearButton.PerformClick()
        initializeQuarterCircleMap(40, 40, 40)
        initializeRobots(amountOfNonSeedRobots)
        initializeSeeds()
        showMap()
    End Sub

    Private Sub InitCircle_Click(sender As Object, e As EventArgs) Handles InitCircle.Click
        ClearButton.PerformClick()
        initializeCircleMap(18)
        initializeRobots(amountOfNonSeedRobots)
        initializeSeeds()
        showMap()

    End Sub


    Private Sub NonConvexButton_Click(sender As Object, e As EventArgs) Handles NonConvexButton.Click
        ClearButton.PerformClick()
        initializeNonConvexMap(70, 70, 70)
        initializeRobots(amountOfNonSeedRobots)
        initializeSeeds()
        showMap()
    End Sub

    Private Sub initializeNonConvexMap(width As Integer, height As Integer, radius As Double)
        Dim totalAmountOfRobots As Integer
        For i As Integer = centerX To centerX + width
            For j As Integer = centerY To centerY + height
                If distanceBetweenPoints(centerX + width, centerY + height, i, j) > radius Then
                    globalMap.Add({i, j})
                    totalAmountOfRobots += 1
                End If
            Next
        Next

        'showMap()
        amountOfNonSeedRobots = totalAmountOfRobots - amountOfSeeds
        MsgBox(totalAmountOfRobots)
    End Sub
End Class
