Imports System.Windows.Input

Public Class RelayCommand : Implements ICommand

    Private ReadOnly _execute As Action(Of Object)
    Private ReadOnly _canExecute As Predicate(Of Object)

    Public Sub New(ByVal execute As Action(Of Object))
    End Sub

    Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Predicate(Of Object))
        If execute Is Nothing Then Throw New ArgumentNullException("execute")
        _execute = execute
        _canExecute = canExecute
    End Sub

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
        Return (_canExecute Is Nothing) OrElse _canExecute(parameter)
    End Function

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        AddHandler(ByVal value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            CommandManager.InvalidateRequerySuggested()
        End RaiseEvent
    End Event

    Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
        _execute(parameter)
    End Sub
    
End Class