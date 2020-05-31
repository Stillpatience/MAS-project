<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.InitSquare = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.StepButton = New System.Windows.Forms.Button()
        Me.StartSingleThread = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.InitCircle = New System.Windows.Forms.Button()
        Me.InitQuarterCircle = New System.Windows.Forms.Button()
        Me.NonConvexButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'InitSquare
        '
        Me.InitSquare.Location = New System.Drawing.Point(895, 30)
        Me.InitSquare.Name = "InitSquare"
        Me.InitSquare.Size = New System.Drawing.Size(115, 23)
        Me.InitSquare.TabIndex = 0
        Me.InitSquare.Text = "Initialize square"
        Me.InitSquare.UseVisualStyleBackColor = True
        '
        'ClearButton
        '
        Me.ClearButton.Location = New System.Drawing.Point(895, 179)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(75, 23)
        Me.ClearButton.TabIndex = 1
        Me.ClearButton.Text = "Clear"
        Me.ClearButton.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(895, 272)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(115, 23)
        Me.StartButton.TabIndex = 2
        Me.StartButton.Text = "Start Multi thread"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'StepButton
        '
        Me.StepButton.Location = New System.Drawing.Point(895, 228)
        Me.StepButton.Name = "StepButton"
        Me.StepButton.Size = New System.Drawing.Size(75, 23)
        Me.StepButton.TabIndex = 3
        Me.StepButton.Text = "Step"
        Me.StepButton.UseVisualStyleBackColor = True
        '
        'StartSingleThread
        '
        Me.StartSingleThread.Location = New System.Drawing.Point(895, 317)
        Me.StartSingleThread.Name = "StartSingleThread"
        Me.StartSingleThread.Size = New System.Drawing.Size(115, 23)
        Me.StartSingleThread.TabIndex = 4
        Me.StartSingleThread.Text = "Start Single thread"
        Me.StartSingleThread.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1865, 519)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Initialize square"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'InitCircle
        '
        Me.InitCircle.Location = New System.Drawing.Point(895, 88)
        Me.InitCircle.Name = "InitCircle"
        Me.InitCircle.Size = New System.Drawing.Size(115, 23)
        Me.InitCircle.TabIndex = 6
        Me.InitCircle.Text = "Initialize circle"
        Me.InitCircle.UseVisualStyleBackColor = True
        '
        'InitQuarterCircle
        '
        Me.InitQuarterCircle.Location = New System.Drawing.Point(895, 59)
        Me.InitQuarterCircle.Name = "InitQuarterCircle"
        Me.InitQuarterCircle.Size = New System.Drawing.Size(161, 23)
        Me.InitQuarterCircle.TabIndex = 7
        Me.InitQuarterCircle.Text = "Initialize quarter circle"
        Me.InitQuarterCircle.UseVisualStyleBackColor = True
        '
        'NonConvexButton
        '
        Me.NonConvexButton.Location = New System.Drawing.Point(895, 117)
        Me.NonConvexButton.Name = "NonConvexButton"
        Me.NonConvexButton.Size = New System.Drawing.Size(153, 23)
        Me.NonConvexButton.TabIndex = 8
        Me.NonConvexButton.Text = "Initialize non-convex"
        Me.NonConvexButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(3844, 1061)
        Me.Controls.Add(Me.NonConvexButton)
        Me.Controls.Add(Me.InitQuarterCircle)
        Me.Controls.Add(Me.InitCircle)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.StartSingleThread)
        Me.Controls.Add(Me.StepButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.InitSquare)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents InitSquare As Button
    Friend WithEvents ClearButton As Button
    Friend WithEvents StartButton As Button
    Friend WithEvents StepButton As Button
    Friend WithEvents StartSingleThread As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents InitCircle As Button
    Friend WithEvents InitQuarterCircle As Button
    Friend WithEvents NonConvexButton As Button
End Class
