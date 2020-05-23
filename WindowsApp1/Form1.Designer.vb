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
        Me.InitButton = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.StepButton = New System.Windows.Forms.Button()
        Me.StartSingleThread = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'InitButton
        '
        Me.InitButton.Location = New System.Drawing.Point(895, 141)
        Me.InitButton.Name = "InitButton"
        Me.InitButton.Size = New System.Drawing.Size(75, 23)
        Me.InitButton.TabIndex = 0
        Me.InitButton.Text = "Initialize"
        Me.InitButton.UseVisualStyleBackColor = True
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
        Me.StartButton.Location = New System.Drawing.Point(895, 232)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(75, 23)
        Me.StartButton.TabIndex = 2
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'StepButton
        '
        Me.StepButton.Location = New System.Drawing.Point(895, 276)
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 560)
        Me.Controls.Add(Me.StartSingleThread)
        Me.Controls.Add(Me.StepButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.ClearButton)
        Me.Controls.Add(Me.InitButton)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents InitButton As Button
    Friend WithEvents ClearButton As Button
    Friend WithEvents StartButton As Button
    Friend WithEvents StepButton As Button
    Friend WithEvents StartSingleThread As Button
End Class
